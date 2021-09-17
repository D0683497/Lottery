using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Enums;
using Lottery.Helpers;
using Lottery.Models;
using Lottery.Models.Event;
using Lottery.Models.Participant;
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
        
        /// <summary>
        /// 活動列表頁面
        /// </summary>
        [HttpGet("list")]
        public async Task<ActionResult<PaginatedList<EventViewModel>>> List([FromQuery] int? page, [FromQuery] string search)
        {
            var query = _dbContext.Events.AsNoTracking()
                .Where(x => x.Status == EventStatus.OpenIng);
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
                .Include(x => x.Fields.OrderBy(y => y.Id))
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EventDisplayViewModel>(entity);
            return View(model);
        }

        [AuthAuthorize]
        [HttpGet("{eventId}/start/{poolId}")]
        public async Task<IActionResult> Start([FromRoute] string eventId, [FromRoute] string poolId)
        {
            var entity = await _dbContext.Pools
                .AsNoTracking()
                .Include(x => x.Event)
                .Include(x => x.Prizes)
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EventStartViewModel>(entity);
            return View(model);
        }

        /// <summary>
        /// 抽獎
        /// </summary>
        [AuthAuthorize]
        [HttpGet("{eventId}/start/{poolId}/prize/{prizeId}")]
        public async Task<IActionResult> Lottery([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
        {
            var pool = await _dbContext.Pools
                .AsNoTracking()
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (pool == null)
            {
                return NotFound();
            }
            var prize = await _dbContext.Prizes
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == prizeId);
            if (prize == null)
            {
                return NotFound();
            }
            if (prize.Last == 0)
            {
                return NoContent();
            }
            prize.Last--;
            Participant participant;
            if (pool.Duplicate)
            {
                participant = _dbContext.Participants
                    .AsNoTracking()
                    .Include(x => x.Claims.OrderBy(y => y.EventClaimId))
                    .ThenInclude(x => x.Field)
                    .Where(x => x.PoolId == poolId)
                    .AsEnumerable()
                    .OrderBy(r => Guid.NewGuid())
                    .FirstOrDefault();
            }
            else
            {
                while (true)
                {
                    participant = _dbContext.Participants
                        .AsNoTracking()
                        .Include(x => x.Claims.OrderBy(y => y.EventClaimId))
                        .ThenInclude(x => x.Field)
                        .Where(x => x.PoolId == poolId)
                        .AsEnumerable()
                        .OrderBy(r => Guid.NewGuid())
                        .FirstOrDefault();
                    if (!await _dbContext.ParticipantPrizes.AnyAsync(x => x.ParticipantId == participant.Id))
                    {
                        break;
                    }
                }
            }
            _dbContext.ParticipantPrizes.Add(new ParticipantPrize
            {
                PrizeId = prizeId,
                ParticipantId = participant.Id
            });
            await _dbContext.SaveChangesAsync();
            var model = _mapper.Map<ParticipantViewModel>(participant);
            return Ok(model);
        }

        /// <summary>
        /// 參與者頁面
        /// </summary>
        [HttpGet("{eventId}/participant")]
        public async Task<ActionResult<SearchViewModel>> Participant([FromRoute] string eventId, [FromQuery] string search)
        {
            var act = await _dbContext.Events
                .AsNoTracking()
                .Include(x => x.Fields.OrderBy(y => y.Id))
                .Include(x => x.Pools)
                .ThenInclude(x => x.Prizes)
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (act == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<SearchViewModel>(act);
            foreach(var field in act.Fields)
            {
                if (field.Key)
                {
                    var claims = await _dbContext.ParticipantClaims
                        .AsNoTracking()
                        .Where(x => x.EventClaimId == field.Id)
                        .Where(x => x.Value == search)
                        .ToListAsync();
                    if (claims == null)
                    {
                        return NotFound();
                    }
                    foreach (var claim in claims)
                    {
                        var participant = await _dbContext.Participants
                            .AsNoTracking()
                            .Include(x => x.ParticipantPrizes)
                            .SingleOrDefaultAsync(x => x.Id == claim.ParticipantId);
                        var pool = model.Pools
                            .SingleOrDefault(x => x.Id == participant.PoolId);
                        if (pool.Participant == null)
                        {
                            pool.Participant = _mapper.Map<ParticipantSearchViewModel>(participant);
                        }
                    }
                }
            }
            return View(model);
        }
        
        [HttpGet("background")]
        public IActionResult Background()
        {
            return File(System.IO.File.OpenRead("wwwroot/images/home.jpg"), "image/jpeg", $"home.jpg");
        }
    }
}