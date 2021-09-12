using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Models;
using Lottery.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [Route("activity")]
    public class ActivityController : Controller
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly LotteryDbContext _dbContext;
        private IMapper _mapper;

        public ActivityController(ILogger<ActivityController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 活動列表頁面
        /// </summary>
        [HttpGet("list")]
        public async Task<ActionResult<PaginatedList<EventViewModel>>> List([FromQuery] int? page, [FromQuery] string search)
        {
            var query = _dbContext.Events.AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Title.Contains(search));
            }
            var entities = await query
                .Skip((page ?? 1 - 1) * 50)
                .Take(50)
                .ToListAsync();
            var count = await query.CountAsync();
            var models = _mapper.Map<List<EventViewModel>>(entities);
            var paginatedModels = new PaginatedList<EventViewModel>(models, count, page ?? 1, 50);
            return View(paginatedModels);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Display([FromRoute] string eventId)
        {
            var entity = await _dbContext.Events
                .AsNoTracking()
                .Include(x => x.Pools)
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EventDisplayViewModel>(entity);
            return View(model);
        }

        [HttpGet("{eventId}/start/{poolId}")]
        public async Task<IActionResult> Start([FromRoute] string eventId, [FromRoute] string poolId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Pools
                .AsNoTracking()
                .Include(x => x.Event)
                .Include(x => x.Prizes)
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            var models = _mapper.Map<EventStartViewModel>(entity);
            return View(models);
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