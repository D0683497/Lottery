using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Helpers;
using Lottery.Models;
using Lottery.Models.Event;
using Lottery.Models.Field;
using Lottery.Models.Participant;
using Lottery.Models.Pool;
using Lottery.Models.Prize;
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
        /// 新增活動
        /// </summary>
        [HttpPost("event/add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<EventAddViewModel>> EventAdd([FromForm] EventAddViewModel model)
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
                .Include(x => x.Pools)
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
        /// 管理獎池頁面
        /// </summary>
        [HttpGet("event/{eventId}/pool")]
        public async Task<ActionResult<IEnumerable<PoolViewModel>>> Pool([FromRoute] string eventId)
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
        [HttpGet("event/{eventId}/pool/{poolId}")]
        public async Task<ActionResult<PoolViewModel>> PoolDetail([FromRoute] string eventId, [FromRoute] string poolId)
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
        [HttpGet("event/{eventId}/pool/add")]
        public async Task<IActionResult> PoolAdd([FromRoute] string eventId)
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
        [HttpPost("event/{eventId}/pool/add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolAddViewModel>> PoolAdd([FromRoute] string eventId, [FromForm] PoolAddViewModel model)
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
                return RedirectToAction("PoolDetail", "Manage", new{ eventId, poolId = entity.Id });
            }
            return View(model);
        }
        
        /// <summary>
        /// 編輯獎池頁面
        /// </summary>
        [HttpGet("event/{eventId}/pool/{poolId}/edit")]
        public async Task<ActionResult<PoolEditViewModel>> PoolEdit([FromRoute] string eventId, [FromRoute] string poolId)
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
        [HttpPost("event/{eventId}/pool/{poolId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolEditViewModel>> PoolEdit([FromRoute] string eventId, [FromRoute] string poolId, [FromForm] PoolEditViewModel model)
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
                return RedirectToAction("PoolDetail", "Manage", new{ eventId, poolId });
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除獎池
        /// </summary>
        [HttpPost("event/{eventId}/pool/{poolId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PoolDelete([FromRoute] string eventId, [FromRoute] string poolId)
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
            return RedirectToAction("Pool", "Manage", new{ eventId });
        }
        
        /// <summary>
        /// 管理獎品頁面
        /// </summary>
        [HttpGet("event/{eventId}/pool/{poolId}/prize")]
        public async Task<ActionResult<IEnumerable<PrizeViewModel>>> Prize([FromRoute] string eventId, [FromRoute] string poolId)
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
        [HttpGet("event/{eventId}/pool/{poolId}/prize/{prizeId}")]
        public async Task<ActionResult<PrizeViewModel>> PrizeDetail([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
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
        [HttpGet("event/{eventId}/pool/{poolId}/prize/add")]
        public async Task<IActionResult> PrizeAdd([FromRoute] string eventId, [FromRoute] string poolId)
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
        [HttpPost("event/{eventId}/pool/{poolId}/prize/add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PrizeAddViewModel>> PrizeAdd([FromRoute] string eventId, [FromRoute] string poolId, [FromForm] PrizeAddViewModel model)
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
                return RedirectToAction("PrizeDetail", "Manage", new{ eventId, poolId, prizeId = entity.Id });
            }
            return View(model);
        }
        
        /// <summary>
        /// 編輯獎池頁面
        /// </summary>
        [HttpGet("event/{eventId}/pool/{poolId}/prize/{prizeId}/edit")]
        public async Task<ActionResult<PrizeEditViewModel>> PrizeEdit([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
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
        [HttpPost("event/{eventId}/pool/{poolId}/prize/{prizeId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PrizeEditViewModel>> PrizeEdit([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId, [FromForm] PrizeEditViewModel model)
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
                return RedirectToAction("PrizeDetail", "Manage", new{ eventId, poolId, prizeId });
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除獎池
        /// </summary>
        [HttpPost("event/{eventId}/pool/{poolId}/prize/{prizeId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrizeDelete([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
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
            return RedirectToAction("Prize", "Manage", new{ eventId, poolId });
        }

        /// <summary>
        /// 管理參與者頁面
        /// </summary>
        [HttpGet("event/{eventId}/pool/{poolId}/participant")]
        public async Task<ActionResult<PaginatedList<ParticipantViewModel>>> Participant([FromRoute] string eventId, [FromRoute] string poolId, [FromQuery] int? page)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var query = _dbContext.Participants
                .AsNoTracking()
                .Include(x => x.Claims)
                .ThenInclude(x => x.EventClaim)
                .Where(x => x.PoolId == poolId);
            var entities = await query
                .Skip((page ?? 1 - 1) * 50)
                .Take(50)
                .ToListAsync();
            var count = await query.CountAsync();
            var models = _mapper.Map<List<ParticipantViewModel>>(entities);
            var paginatedModels = new PaginatedList<ParticipantViewModel>(models, count, page ?? 1, 50);
            return View(paginatedModels);
        }
        
        /// <summary>
        /// 參與者詳情頁面
        /// </summary>
        [HttpGet("event/{eventId}/pool/{poolId}/participant/{participantId}")]
        public async Task<ActionResult<ParticipantViewModel>> ParticipantDetail([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string participantId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Participants
                .AsNoTracking()
                .Include(x => x.Claims)
                .ThenInclude(x => x.EventClaim)
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == participantId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<ParticipantViewModel>(entity);
            return View(model);
        }

        /// <summary>
        /// 新增參與者頁面
        /// </summary>
        [HttpGet("event/{eventId}/pool/{poolId}/participant/add")]
        public async Task<ActionResult<List<ParticipantAddViewModel>>> ParticipantAdd([FromRoute] string eventId, [FromRoute] string poolId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entities = await _dbContext.EventClaims
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .ToListAsync();
            var models = _mapper.Map<List<ParticipantAddViewModel>>(entities);
            return View(models);
        }
        
        /// <summary>
        /// 新增參與者
        /// </summary>
        [HttpPost("event/{eventId}/pool/{poolId}/participant/add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<List<ParticipantAddViewModel>>> ParticipantAdd([FromRoute] string eventId, [FromRoute] string poolId, [FromForm] List<ParticipantAddViewModel> models)
        {
            var pool = await _dbContext.Pools
                .Include(x => x.Participants)
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (pool == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    var field = await _dbContext.EventClaims
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Id == models[i].Field.Id);
                    if (field == null)
                    {
                        return BadRequest();
                    }
                    if (field.Key && string.IsNullOrEmpty(models[i].Value))
                    {
                        ModelState.AddModelError($"[{i}].Value", $"{models[i].Field.Value}是必填的");
                    }
                }
                if (ModelState.IsValid)
                {
                    var entity = _mapper.Map<Participant>(models);
                    pool.Participants.Add(entity);
                    _dbContext.Pools.Update(pool);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("ParticipantDetail", "Manage", new{ eventId, poolId, participantId = entity.Id });
                }
            }
            return View(models);
        }

        /// <summary>
        /// 管理欄位頁面
        /// </summary>
        [HttpGet("event/{eventId}/field")]
        public async Task<ActionResult<IEnumerable<FieldViewModel>>> Field([FromRoute] string eventId)
        {
            if (!await _dbContext.Events.AnyAsync(x => x.Id == eventId))
            {
                return NotFound();
            }
            var entities = await _dbContext.EventClaims
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .ToListAsync();
            var models = _mapper.Map<IEnumerable<FieldViewModel>>(entities);
            return View(models);
        }
        
        /// <summary>
        /// 欄位詳情頁面
        /// </summary>
        [HttpGet("event/{eventId}/field/{fieldId}")]
        public async Task<ActionResult<FieldViewModel>> FieldDetail([FromRoute] string eventId, [FromRoute] string fieldId)
        {
            var entity = await _dbContext.EventClaims
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == fieldId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<FieldViewModel>(entity);
            return View(model);
        }

        /// <summary>
        /// 新增欄位頁面
        /// </summary>
        [HttpGet("event/{eventId}/field/add")]
        public async Task<IActionResult> FieldAdd([FromRoute] string eventId)
        {
            if (!await _dbContext.Events.AnyAsync(x => x.Id == eventId))
            {
                return NotFound();
            }
            return View();
        }
        
        /// <summary>
        /// 新增欄位
        /// </summary>
        [HttpPost("event/{eventId}/field/add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolAddViewModel>> FieldAdd([FromRoute] string eventId, [FromForm] FieldAddViewModel model)
        {
            var act = await _dbContext.Events
                .Include(x => x.Claims)
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (act == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<EventClaim>(model);
                act.Claims.Add(entity);
                _dbContext.Events.Update(act);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("FieldDetail", "Manage", new{ eventId, fieldId = entity.Id });
            }
            return View(model);
        }
        
        /// <summary>
        /// 編輯欄位頁面
        /// </summary>
        [HttpGet("event/{eventId}/field/{fieldId}/edit")]
        public async Task<ActionResult<FieldEditViewModel>> FieldEdit([FromRoute] string eventId, [FromRoute] string fieldId)
        {
            var entity = await _dbContext.EventClaims
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == fieldId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<FieldEditViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 編輯欄位
        /// </summary>
        [HttpPost("event/{eventId}/field/{fieldId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolEditViewModel>> FieldEdit([FromRoute] string eventId, [FromRoute] string fieldId, [FromForm] FieldEditViewModel model)
        {
            var entity = await _dbContext.EventClaims
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == fieldId);
            if (entity == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var updateEntity = _mapper.Map(model, entity);
                _dbContext.EventClaims.Update(updateEntity);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("FieldDetail", "Manage", new{ eventId, fieldId });
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除欄位
        /// </summary>
        [HttpPost("event/{eventId}/field/{fieldId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FieldDelete([FromRoute] string eventId, [FromRoute] string fieldId)
        {
            var entity = await _dbContext.EventClaims
                .Include(x => x.ParticipantClaims)
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == fieldId);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.EventClaims.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Pool", "Manage", new{ eventId });
        }
        
        /// <summary>
        /// 管理申請表頁面
        /// </summary>
        [HttpGet("apply")]
        public IActionResult Apply() => View();
    }
}