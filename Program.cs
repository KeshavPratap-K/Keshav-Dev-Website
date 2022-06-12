using Keshav_Dev.Model;
using Keshav_Dev.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<ClipyClipboardDatabaseSettings>(
    builder.Configuration.GetSection("ClipyDatabase"));
builder.Services.Configure<ClipyUserDatabaseSettings>(
    builder.Configuration.GetSection("ClipyUserDatabase"));
builder.Services.AddSingleton<ClipyClipboardService>();
//builder.Services.AddSingleton<ClipyClipboardCRUD>();
builder.Services.AddSingleton<ClipyClipboardFields>();
builder.Services.AddSingleton<ClipyUserService>();
//builder.Services.AddSingleton<ClipyClipboardCRUD>();
builder.Services.AddSingleton<ClipyUserFields>();
builder.Services.AddSingleton<ClipyClipboardDataId>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
