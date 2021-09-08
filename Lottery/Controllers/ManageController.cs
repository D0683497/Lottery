using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Helpers;
using Lottery.Models;
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
        public async Task<ActionResult<PaginatedList<EventViewModel>>> Event([FromQuery] int? page, [FromQuery] string search)
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
        
        /// <summary>
        /// 活動詳情頁面
        /// </summary>
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<EventViewModel>> EventDetail([FromRoute] string eventId)
        {
            var entity = await _dbContext.Events
                .AsNoTracking()
                .Include(x => x.Pools)
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (entity == null)
            {
                return NotFound();
            }
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
                    return RedirectToAction("EventDetail", "Manage", new{ eventId = entity.Id });
                }
            }
            return View(model);
        }

        /// <summary>
        /// 編輯活動頁面
        /// </summary>
        [HttpGet("event/{eventId}/edit")]
        public async Task<ActionResult<EventEditViewModel>> EventEdit([FromRoute] string eventId)
        {
            var entity = await _dbContext.Events
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EventEditViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 編輯活動
        /// </summary>
        [HttpPost("event/{eventId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<EventEditViewModel>> EventEdit([FromRoute] string eventId, [FromForm] EventEditViewModel model)
        {
            var entity = await _dbContext.Events
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (entity == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (entity.Url != model.Url)
                {
                    if (await _dbContext.Events.AnyAsync(x => x.Url == model.Url))
                    {
                        ModelState.AddModelError("Url", "網址已經被使用");
                    }
                }
                if (ModelState.IsValid)
                {
                    var updateEntity = _mapper.Map(model, entity);
                    _dbContext.Events.Update(updateEntity);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("EventDetail", "Manage", new{ eventId });
                }
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除活動
        /// </summary>
        [HttpPost("event/{eventId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventDelete([FromRoute] string eventId)
        {
            var entity = await _dbContext.Events
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Events.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Event", "Manage");
        }
        
        /// <summary>
        /// 管理申請表頁面
        /// </summary>
        [HttpGet("apply")]
        public IActionResult Apply() => View();
    }
}