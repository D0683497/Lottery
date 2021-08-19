using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}