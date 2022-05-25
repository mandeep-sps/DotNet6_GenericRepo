using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SyncLib.Model.Common;
using SyncLib.WebApp.Models;
using System.Diagnostics;

namespace SyncLib.WebApp.Controllers
{

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOptions<ApplicationSettingsModel> options) : base(options)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}