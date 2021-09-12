using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Helpers;
using Lottery.Models.Pool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [AuthAuthorize]
    [Route("manage/event/{eventId}/pool")]
    public class PoolController : Controller
    {
        private readonly ILogger<PoolController> _logger;
        private readonly LotteryDbContext _dbContext;
        private readonly IMapper _mapper;

        public PoolController(ILogger<PoolController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 管理獎池頁面
        /// </summary>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PoolViewModel>>> Index([FromRoute] string eventId)
        {
            if (!await _dbContext.Events.AnyAsync(x => x.Id == eventId))
            {
                return NotFound();
            }
            var entities = await _dbContext.Pools
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .ToListAsync();
            var models = _mapper.Map<IEnumerable<PoolViewModel>>(entities);
            return View(models);
        }
        
        /// <summary>
        /// 獎池詳情頁面
        /// </summary>
        [HttpGet("{poolId}")]
        public async Task<ActionResult<PoolViewModel>> Detail([FromRoute] string eventId, [FromRoute] string poolId)
        {
            var entity = await _dbContext.Pools
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<PoolViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 新增獎池頁面
        /// </summary>
        [HttpGet("add")]
        public async Task<IActionResult> Add([FromRoute] string eventId)
        {
            if (!await _dbContext.Events.AnyAsync(x => x.Id == eventId))
            {
                return NotFound();
            }
            return View();
        }

        /// <summary>
        /// 新增獎池
        /// </summary>
        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolAddViewModel>> Add([FromRoute] string eventId, [FromForm] PoolAddViewModel model)
        {
            var act = await _dbContext.Events
                .Include(x => x.Pools)
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (act == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Pool>(model);
                act.Pools.Add(entity);
                _dbContext.Events.Update(act);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Detail", "Pool", new{ eventId, poolId = entity.Id });
            }
            return View(model);
        }
        
        /// <summary>
        /// 編輯獎池頁面
        /// </summary>
        [HttpGet("{poolId}/edit")]
        public async Task<ActionResult<PoolEditViewModel>> Edit([FromRoute] string eventId, [FromRoute] string poolId)
        {
            var entity = await _dbContext.Pools
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<PoolEditViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 編輯獎池
        /// </summary>
        [HttpPost("{poolId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolEditViewModel>> Edit([FromRoute] string eventId, [FromRoute] string poolId, [FromForm] PoolEditViewModel model)
        {
            var entity = await _dbContext.Pools
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (entity == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var updateEntity = _mapper.Map(model, entity);
                _dbContext.Pools.Update(updateEntity);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Detail", "Pool", new{ eventId, poolId });
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除獎池
        /// </summary>
        [HttpPost("{poolId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string eventId, [FromRoute] string poolId)
        {
            var entity = await _dbContext.Pools
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Pools.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Pool", new{ eventId });
        }
    }
}