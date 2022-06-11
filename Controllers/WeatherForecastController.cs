using Keshav_Dev.Model;
using KloudReach.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keshav_Dev.Controllers
{
    public class WeatherForecastController : ControllerBase
    {
        private readonly ClipyClipboardService _clipyClipboardService;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ClipyClipboardService clipyClipboardService)
        {
            _logger = logger;
            _clipyClipboardService = clipyClipboardService;
        }

        //[HttpGet]
        //public async Task<IEnumerable<clipyClipboardData>> GetAsync()
        //{
        //    var clipyClipboardFields = await _clipyClipboardService.GetAsync("5e44746f-e6b6-4914-82b5-c374e03cf5ba");
        //    return Enumerable.Range(1, 1).Select(index => new clipyClipboardData
        //    {
        //        clipboardData = clipyClipboardFields.ClipyHistory,
        //    })
        //    .ToArray();
        //}
    }
}