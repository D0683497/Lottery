using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 首頁
        /// </summary>
        [HttpGet("")]
        public IActionResult Index() => View();

        /// <summary>
        /// 申請使用頁面
        /// </summary>
        [HttpGet("apply")]
        public async Task<IActionResult> Apply()
        {
            var userId = User.Claims
                .SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                ?.Value;
            if (userId == null)
            {
                return View();
            }
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId);
            var model = _mapper.Map<ApplyViewModel>(user);
            return View(model);
        }
        
        [HttpGet("current-activity")]
        public async Task<IActionResult> CurrentActivity()
        {
            var url = await _dbContext.Settings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name == "event");
            return Redirect(url.Value);
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

        [HttpGet("errors")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
