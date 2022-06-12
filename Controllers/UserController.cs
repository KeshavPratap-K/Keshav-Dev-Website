using Keshav_Dev.Model;
using Keshav_Dev.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Keshav_Dev.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly ClipyUserService _clipyUserService;
    private readonly ClipyClipboardService _clipyClipboardService;
    public UserController(ClipyUserService clipyUserService, ClipyClipboardService clipyClipboardService)
    {
        _clipyUserService = clipyUserService;
        _clipyClipboardService = clipyClipboardService;
    }
    // GET: UserController

    [HttpGet]
    [Route("login")]
    [AllowAnonymous]
    public async Task<LoginUser> Login()
    {
        return new LoginUser
        {
            UserName = "test",
            Password = "test"
        };
    }
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUser userLogin)
    {
        //{ "userName":"test","password":"test"}
        var user = await AuthenticateUser(userLogin.UserName, userLogin.Password);
        if (user == null)
        {
            return BadRequest();
        }

        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userLogin.UserName),
                    new Claim("FullName", userLogin.UserName),
                    new Claim(ClaimTypes.Role, "User"),
                };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,

            IsPersistent = true,
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
        return this.Ok($"True");
    }

    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> register(RegisterUser userRegister)
    {
        //{ "userName":"test","password":"test"}
        var user = await RegisterUser(userRegister);
        if (user == false)
        {
           return BadRequest();
        }

        return this.Ok($"True");
    }


    private async Task<LoginUser?> AuthenticateUser(string email, string password)
    {
        // For demonstration purposes, authenticate a user
        // with a static email address. Ignore the password.
        // Assume that checking the database takes 500ms

        try
        {

            ClipyUserFields userExists = await _clipyUserService.GetAsync(email);
            if (userExists != null && userExists.Password == password)
            {
                return new LoginUser()
                {
                    UserName = userExists.UserName,
                    Password = password
                };
            }
            else
            {
                return null;
            }
        }

        catch(Exception e)
        {
            return null;
        }
    }

    private async Task<bool> RegisterUser(RegisterUser registerUser)
    {
        try
        {
            if (registerUser == null)
                return false;
            ClipyUserFields clipyUserFields = new ClipyUserFields
            {
                Email = registerUser.email,
                UserName = registerUser.UserName,
                Password = registerUser.Password,
            };
            ClipyUserFields userExists = await _clipyUserService.GetAsync(clipyUserFields.Email);
            if (userExists == null)
            {
                await _clipyUserService.CreateAsync(clipyUserFields);
                ClipyUserFields clipyUser = await _clipyUserService.GetAsync(clipyUserFields.Email);
                ClipyClipboardFields clipyClipboardFields = new ClipyClipboardFields
                {
                    IdShared = clipyUser._id,
                    ClipyHistory = new List<string>()
                };
                await _clipyClipboardService.CreateAsync(clipyClipboardFields);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

}
