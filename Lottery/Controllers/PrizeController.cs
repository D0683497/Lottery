using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Helpers;
using Lottery.Models.Prize;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [AuthAuthorize]
    [Route("manage/event/{eventId}/pool/{poolId}/prize")]
    public class PrizeController : Controller
    {
        private readonly ILogger<PrizeController> _logger;
        private readonly LotteryDbContext _dbContext;
        private readonly IMapper _mapper;

        public PrizeController(ILogger<PrizeController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 管理獎品頁面
        /// </summary>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PrizeViewModel>>> Index([FromRoute] string eventId, [FromRoute] string poolId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entities = await _dbContext.Prizes
                .AsNoTracking()
                .Where(x => x.PoolId == poolId)
                .ToListAsync();
            var models = _mapper.Map<IEnumerable<PrizeViewModel>>(entities);
            return View(models);
        }
        
        /// <summary>
        /// 獎品詳情頁面
        /// </summary>
        [HttpGet("{prizeId}")]
        public async Task<ActionResult<PrizeViewModel>> Detail([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Prizes
                .AsNoTracking()
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == prizeId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<PrizeViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 新增獎品頁面
        /// </summary>
        [HttpGet("add")]
        public async Task<IActionResult> Add([FromRoute] string eventId, [FromRoute] string poolId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            return View();
        }
        
        /// <summary>
        /// 新增獎品
        /// </summary>
        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PrizeAddViewModel>> Add([FromRoute] string eventId, [FromRoute] string poolId, [FromForm] PrizeAddViewModel model)
        {
            var pool = await _dbContext.Pools
                .Include(x => x.Prizes)
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (pool == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Prize>(model);
                pool.Prizes.Add(entity);
                _dbContext.Pools.Update(pool);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Detail", "Prize", new{ eventId, poolId, prizeId = entity.Id });
            }
            return View(model);
        }
        
        /// <summary>
        /// 編輯獎池頁面
        /// </summary>
        [HttpGet("{prizeId}/edit")]
        public async Task<ActionResult<PrizeEditViewModel>> Edit([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Prizes
                .AsNoTracking()
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == prizeId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<PrizeEditViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 編輯獎池
        /// </summary>
        [HttpPost("{prizeId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PrizeEditViewModel>> Edit([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId, [FromForm] PrizeEditViewModel model)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Prizes
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == prizeId);
            if (entity == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var updateEntity = _mapper.Map(model, entity);
                _dbContext.Prizes.Update(updateEntity);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Detail", "Prize", new{ eventId, poolId, prizeId });
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除獎池
        /// </summary>
        [HttpPost("{prizeId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Prizes
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == prizeId);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Prizes.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Prize", new{ eventId, poolId });
        }
    }
}