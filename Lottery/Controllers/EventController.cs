using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [Route("event")]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("list")]
        public IActionResult List()
        {
            return View();
        }
        
        [HttpGet("participant")]
        public IActionResult Participant()
        {
            return View();
        }
        
        [HttpGet("background")]
        public IActionResult Background()
        {
            return File(System.IO.File.OpenRead("wwwroot/images/home.jpg"), "image/jpeg", $"home.jpg");
        }
    }
}