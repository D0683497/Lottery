using System.Diagnostics;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LotteryDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, LotteryDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("privacy")]
        public async Task<IActionResult> Privacy()
        {
            var url = await _dbContext.Settings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == "privacy");
            return Redirect(url.Value);
        }
        
        [HttpGet("facebook")]
        public async Task<IActionResult> Facebook()
        {
            var url = await _dbContext.Settings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == "facebook");
            return Redirect(url.Value);
        }
        
        [HttpGet("instagram")]
        public async Task<IActionResult> Instagram()
        {
            var url = await _dbContext.Settings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == "instagram");
            return Redirect(url.Value);
        }
        
        [HttpGet("site")]
        public async Task<IActionResult> Site()
        {
            var url = await _dbContext.Settings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == "website");
            return Redirect(url.Value);
        }
        
        [HttpGet("github")]
        public async Task<IActionResult> GitHub()
        {
            var url = await _dbContext.Settings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == "github");
            return Redirect(url.Value);
        }

        [HttpGet("credits")]
        public async Task<IActionResult> Credits()
        {
            var url = await _dbContext.Settings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == "credits");
            return Redirect(url.Value);
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
