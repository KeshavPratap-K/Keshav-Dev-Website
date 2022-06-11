using Keshav_Dev.Model;
using KloudReach.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ClipyClipboardDatabaseSettings>(
    builder.Configuration.GetSection("ClipyDatabase"));
builder.Services.AddSingleton<ClipyClipboardService>();
//builder.Services.AddSingleton<ClipyClipboardCRUD>();
builder.Services.AddSingleton<ClipyClipboardFields>();


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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
