using System.Diagnostics;
using Lottery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("background")]
        public IActionResult Background()
        {
            return File(System.IO.File.OpenRead("wwwroot/images/home.jpg"), "image/jpeg", $"home.jpg");
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return Redirect("https://www.fcu.edu.tw/privacy/");
        }
        
        [HttpGet("facebook")]
        public IActionResult Facebook()
        {
            return Redirect("https://www.facebook.com/fcussc/");
        }
        
        [HttpGet("instagram")]
        public IActionResult Instagram()
        {
            return Redirect("https://www.instagram.com/fcu.cdc/");
        }
        
        [HttpGet("github")]
        public IActionResult GitHub()
        {
            return Redirect("https://github.com/fcu-ssc/");
        }
        
        [HttpGet("site")]
        public IActionResult Site()
        {
            return Redirect("https://ssc.fcu.edu.tw/");
        }

        [HttpGet("credits")]
        public IActionResult Credits()
        {
            return View();
        }
        
        [HttpGet("apply")]
        public IActionResult Apply()
        {
            return View();
        }

        [HttpGet("errors")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
