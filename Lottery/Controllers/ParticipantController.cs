using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Helpers;
using Lottery.Models;
using Lottery.Models.Field;
using Lottery.Models.Participant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [AuthAuthorize]
    [Route("manage/event/{eventId}/pool/{poolId}/participant")]
    public class ParticipantController : Controller
    {
        private readonly ILogger<ParticipantController> _logger;
        private readonly LotteryDbContext _dbContext;
        private readonly IMapper _mapper;

        public ParticipantController(ILogger<ParticipantController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 管理參與者頁面
        /// </summary>
        [HttpGet("")]
        public async Task<ActionResult<PaginatedList<ParticipantViewModel>>> Index([FromRoute] string eventId, [FromRoute] string poolId, [FromQuery] int? page, [FromQuery] string search)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(search))
            {
                var query = _dbContext.Participants
                    .AsNoTracking()
                    .Include(x => x.Claims)
                    .ThenInclude(x => x.Field)
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
            else
            {
                var ids = await _dbContext.ParticipantClaims
                    .AsNoTracking()
                    .Where(x => x.Value.Contains(search))
                    .Include(x => x.Participant)
                    .Select(x => x.Participant.Id)
                    .ToListAsync();
                var entities = await _dbContext.Participants
                    .AsNoTracking()
                    .Include(x => x.Claims)
                    .ThenInclude(x => x.Field)
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();
                var models = _mapper.Map<List<ParticipantViewModel>>(entities);
                var paginatedModels = new PaginatedList<ParticipantViewModel>(models, models.Count, 1, models.Count);
                return View(paginatedModels);
            }
        }
        
        /// <summary>
        /// 參與者詳情頁面
        /// </summary>
        [HttpGet("{participantId}")]
        public async Task<ActionResult<ParticipantViewModel>> Detail([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string participantId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Participants
                .AsNoTracking()
                .Include(x => x.Claims)
                .ThenInclude(x => x.Field)
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
        [HttpGet("add")]
        public async Task<ActionResult<List<ParticipantAddViewModel>>> Add([FromRoute] string eventId, [FromRoute] string poolId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entities = await _dbContext.Fields
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(x => x.EventId == eventId)
                .ToListAsync();
            var models = _mapper.Map<List<ParticipantAddViewModel>>(entities);
            return View(models);
        }
        
        /// <summary>
        /// 新增參與者
        /// </summary>
        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<List<ParticipantAddViewModel>>> Add([FromRoute] string eventId, [FromRoute] string poolId, [FromForm] List<ParticipantAddViewModel> models)
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
                    var field = await _dbContext.Fields
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
                    var entity = new Participant
                    {
                        Claims = _mapper.Map<List<ParticipantClaim>>(models)
                    };
                    pool.Participants.Add(entity);
                    _dbContext.Pools.Update(pool);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Detail", "Participant", new{ eventId, poolId, participantId = entity.Id });
                }
            }
            return View(models);
        }

        /// <summary>
        /// 匯入參與者頁面
        /// </summary>
        [HttpGet("import")]
        public async Task<IActionResult> Import([FromRoute] string eventId, [FromRoute] string poolId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entities = await _dbContext.Fields
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(x => x.EventId == eventId)
                .ToListAsync();
            var models = _mapper.Map<List<FieldViewModel>>(entities);
            return View(models);
        }

        /// <summary>
        /// 匯入參與者
        /// </summary>
        [HttpPost("import")]
        public async Task<IActionResult> Import([FromRoute] string eventId, [FromRoute] string poolId, [FromForm] IFormFile file)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var fields = await _dbContext.Fields
                .AsNoTracking()
                .Include(x => x.ParticipantClaims)
                .OrderBy(x => x.Id)
                .Where(x => x.EventId == eventId)
                .ToListAsync();
            using (var wbook = new XLWorkbook(file.OpenReadStream()))
            {
                var worksheet = wbook.Worksheet(1);
                foreach (IXLRow row in worksheet.Rows())
                {
                    var entity = new Participant { PoolId = poolId, Claims = new List<ParticipantClaim>() };
                    for (int i = 1; i <= fields.Count; i++)
                    {
                        var value = row.Cell(i).Value.ToString();
                        if (fields[i-1].Key && string.IsNullOrEmpty(value))
                        {
                            return BadRequest();
                        }
                        entity.Claims.Add(new ParticipantClaim
                        {
                            Value = row.Cell(i).Value.ToString(),
                            EventClaimId = fields[i-1].Id
                        });
                    }
                    _dbContext.Participants.Add(entity);
                }
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Participant", new { eventId, poolId });
        }

        /// <summary>
        /// 編輯參與者頁面
        /// </summary>
        [HttpGet("{participantId}/edit")]
        public async Task<ActionResult<List<ParticipantEditViewModel>>> Edit([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string participantId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Participants
                .AsNoTracking()
                .Include(x => x.Claims)
                .ThenInclude(x => x.Field)
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == participantId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<List<ParticipantEditViewModel>>(entity.Claims);
            return View(model);
        }
        
        /// <summary>
        /// 編輯參與者
        /// </summary>
        [HttpPost("{participantId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<List<ParticipantEditViewModel>>> Edit([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string participantId, [FromForm] List<ParticipantEditViewModel> models)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Participants
                .Include(x => x.Claims)
                .ThenInclude(x => x.Field)
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == participantId);
            if (entity == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    var field = entity.Claims
                        .SingleOrDefault(x => x.EventClaimId ==  models[i].Field.Id);
                    if (field == null)
                    {
                        return BadRequest();
                    }
                    if (field.Field.Key && string.IsNullOrEmpty(models[i].Value))
                    {
                        ModelState.AddModelError($"[{i}].Value", $"{models[i].Field.Value}是必填的");
                    }
                }
                if (ModelState.IsValid)
                {
                    var updateEntity = _mapper.Map(models, entity);
                    _dbContext.Participants.Update(updateEntity);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Detail", "Participant", new{ eventId, poolId, participantId });
                }
            }
            return View(models);
        }
        
        /// <summary>
        /// 刪除參與者
        /// </summary>
        [HttpPost("{participantId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string participantId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.Participants
                .Include(x => x.Claims)
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == participantId);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Participants.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Participant", new{ eventId, poolId });
        }
    }
}