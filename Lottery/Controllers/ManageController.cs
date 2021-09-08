using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Helpers;
using Lottery.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [AuthAuthorize]
    [Route("manage")]
    public class ManageController : Controller
    {
        private readonly ILogger<ManageController> _logger;
        private readonly LotteryDbContext _dbContext;
        private readonly IMapper _mapper;

        public ManageController(ILogger<ManageController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 管理頁面
        /// </summary>
        [HttpGet("")]
        public IActionResult Index() => View();
        
        /// <summary>
        /// 網站設定
        /// </summary>
        [HttpGet("setting")]
        public IActionResult Setting() => View();
        
        /// <summary>
        /// 管理活動頁面
        /// </summary>
        [HttpGet("event")]
        public IActionResult Event() => View();
        
        /// <summary>
        /// 活動詳情頁面
        /// </summary>
        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventViewModel>> EventDetail([FromRoute] string eventId)
        {
            var entity = await _dbContext.Events
                .AsNoTracking()
                .Include(x => x.Pools)
                .SingleOrDefaultAsync(x => x.Id == eventId);
            var model = _mapper.Map<EventViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 新增活動頁面
        /// </summary>
        [HttpGet("event/add")]
        public IActionResult EventAdd() => View();

        /// <summary>
        /// 新增活動頁面
        /// </summary>
        [HttpPost("event/add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventAdd([FromForm] EventAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _dbContext.Events.AnyAsync(x => x.Url == model.Url))
                {
                    ModelState.AddModelError("Url", "網址已經被使用");
                }
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<Event>(model);
                    await _dbContext.Events.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Event", "Manage", new{ eventId = entity.Id });
                }
            }
            return View(model);
        }
        
        /// <summary>
        /// 管理申請表頁面
        /// </summary>
        [HttpGet("apply")]
        public IActionResult Apply() => View();
    }
}