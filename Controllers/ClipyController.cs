using Keshav_Dev.Model;
using Keshav_Dev.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//using Keshav_Dev.Views.Home;

namespace Keshav_Dev.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ClipyController : ControllerBase
{
    private readonly ClipyClipboardService _clipyClipboardService;
   private readonly ClipyClipboardFields _clipboardFields;

    public ClipyController(ClipyClipboardService clipyClipboardService, ClipyClipboardFields clipboardFields)
    {
        
        _clipyClipboardService = clipyClipboardService;
        _clipboardFields = clipboardFields;
    }



    //[BindProperty]
    //public InputModel Input { get; set; }

    //public class InputModel
    //{
    //    public List<string> ClipyHistory { get; set; }
    //}

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<ClipyClipboardData>> GetAsync()
    {
        var clipyClipboardFields = await _clipyClipboardService.GetAsync("62a5e26605362bc67b0a52a8");
        return Enumerable.Range(1, 1).Select(index => new ClipyClipboardData
        {
            clipboardData = clipyClipboardFields.ClipyHistory,
        })
        .ToArray();
    }

    [HttpPost]
    public async Task<IActionResult> putAsync(IEnumerable<ClipyClipboardData> clipyClipboard)
    {
        if (clipyClipboard != null)
        {
            ClipyClipboardFields updatedClipyClipboardFields = new ClipyClipboardFields();
            updatedClipyClipboardFields.IdShared = "62a5e26605362bc67b0a52a8";
            updatedClipyClipboardFields.ClipyHistory = clipyClipboard.First().clipboardData;

            await _clipyClipboardService.UpdateAsync("62a5e26605362bc67b0a52a8", updatedClipyClipboardFields);
            return this.Ok($"True");
        }
        return this.BadRequest($"Failed");
        
    }

    //[HttpGet]
    //public async Task<IActionResult> Index()
    //{
    //    if (User != null && User.Identity.IsAuthenticated)
    //    {
    //        //ClipyClipboardFields clipyClipboardFields = new ClipyClipboardFields();
    //        ClipyClipboardFields clipyClipboard = new ClipyClipboardFields();
    //        var user = await _userManager.GetUserAsync(User);

    //        string userId = await _userManager.GetUserIdAsync(user);

    //        var clipyClipboardData = await ClipyClipboardGet(userId);
    //        if (clipyClipboardData == null)
    //        {
    //            return RedirectToAction("Error", "Home");
    //        }
    //        if (clipyClipboardData.Value == null)
    //        {
    //            return RedirectToAction("Error", "Home");
    //        }
    //        _clipboardFields.ClipyHistory = clipyClipboardData.Value.ClipyHistory;
    //        return View();
    //    }
    //    else
    //    {
    //        return View();
    //    }
    //}

    //public class ClipyHistoryClass
    //{
    //    public List<string> ClipyHistory { get; set; }
    //}

    //[HttpPost]
    //public async Task<IActionResult> AddStudent([FromBody] List<string> ClipyHistory)
    //{
    //    //Insert code;
    //    var user = await _userManager.GetUserAsync(User);

    //    string userId = await _userManager.GetUserIdAsync(user);
    //    //var n = listkey;
    //    var clipyClipboardFields = await _clipyClipboardService.GetAsync(userId);

    //    if (clipyClipboardFields is null)
    //    {
    //        return RedirectToAction("Error", "Home");
    //    }
    //    if (ClipyHistory.Count <= 6 && ClipyHistory.Count > 0)
    //    {
    //        ClipyClipboardFields updatedClipyClipboardFields = new ClipyClipboardFields();
    //        updatedClipyClipboardFields.IdShared = userId;
    //        updatedClipyClipboardFields.ClipyHistory = ClipyHistory;

    //        await _clipyClipboardService.UpdateAsync(userId, updatedClipyClipboardFields);
    //        TempData["UserMessage"] = "success";

    //        return this.Ok($"True");
    //    }
    //    else
    //    {
    //        return RedirectToAction("Error", "Home");
    //    }
    //}


    //[Authorize]
    //[HttpPost]
    //public async Task<IActionResult> ClipyUpdateAsync([FromBody] List<string> ClipyHistory)
    //{
    //    var user = await _userManager.GetUserAsync(User);

    //    string userId = await _userManager.GetUserIdAsync(user);
    //    //var n = listkey;
    //    var clipyClipboardFields = await _clipyClipboardService.GetAsync(userId);

    //    if (clipyClipboardFields is null)
    //    {
    //        return RedirectToAction("Error", "Home");
    //    }
    //    if (ClipyHistory.Count <= 6 && ClipyHistory.Count > 0)
    //    {
    //        ClipyClipboardFields updatedClipyClipboardFields = new ClipyClipboardFields();
    //        updatedClipyClipboardFields.IdShared = userId;
    //        updatedClipyClipboardFields.ClipyHistory = ClipyHistory;

    //        await _clipyClipboardService.UpdateAsync(userId, updatedClipyClipboardFields);
    //        TempData["UserMessage"] = "success";

    //        return this.Ok($"Form Data received!");
    //    }
    //    else
    //    {
    //        return RedirectToAction("Error", "Home");
    //    }
    //}


    //public async Task<List<ClipyClipboardFields>> ClipyClipboardGet() =>
    //    await _clipyClipboardService.GetAsync();

    //public async Task<ActionResult<ClipyClipboardFields>> ClipyClipboardGet(string id)
    //{
    //    var clipyClipboard = await _clipyClipboardService.GetAsync(id);

    //    if (clipyClipboard is null)
    //    {
    //        return NotFound();
    //    }

    //    return clipyClipboard;
    //}


    //public async Task<IActionResult> ClipyClipboardCreate(ClipyClipboardFields newClipyClipboardFields)
    //{
    //    await _clipyClipboardService.CreateAsync(newClipyClipboardFields);

    //    return CreatedAtAction(nameof(ClipyClipboardGet), new { id = newClipyClipboardFields.IdShared }, newClipyClipboardFields);
    //}


    //public async Task<IActionResult> ClipyClipboardUpdate(string id, ClipyClipboardFields updatedClipyClipboardFields)
    //{
    //    var clipyClipboardFields = await _clipyClipboardService.GetAsync(id);

    //    if (clipyClipboardFields is null)
    //    {
    //        return NotFound();
    //    }

    //    updatedClipyClipboardFields.IdShared = updatedClipyClipboardFields.IdShared;

    //    await _clipyClipboardService.UpdateAsync(id, updatedClipyClipboardFields);

    //    return NoContent();
    //}


    //public async Task<IActionResult> ClipyClipboardDelete(string id)
    //{
    //    var clipyClipboardFields = await _clipyClipboardService.GetAsync(id);

    //    if (clipyClipboardFields is null)
    //    {
    //        return NotFound();
    //    }

    //    await _clipyClipboardService.RemoveAsync(id);

    //    return NoContent();
    //}
}

