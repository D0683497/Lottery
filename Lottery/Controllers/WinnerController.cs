using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Lottery.Data;
using Lottery.Helpers;
using Lottery.Models.Winner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeTypes;

namespace Lottery.Controllers
{
    [AuthAuthorize]
    [Route("manage/event/{eventId}/pool/{poolId}/prize/{prizeId}/winner")]
    public class WinnerController : Controller
    {
        private readonly ILogger<WinnerController> _logger;
        private readonly LotteryDbContext _dbContext;
        private readonly IMapper _mapper;

        public WinnerController(ILogger<WinnerController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 管理得獎者頁面
        /// </summary>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<WinnerViewModel>>> Index([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entities = await _dbContext.ParticipantPrizes
                .AsNoTracking()
                .Include(x => x.Participant)
                .ThenInclude(x => x.Claims.OrderBy(y => y.EventClaimId))
                .ThenInclude(x => x.Field)
                .Where(x => x.PrizeId == prizeId)
                .ToListAsync();
            var models = _mapper.Map<List<WinnerViewModel>>(entities);
            return View(models);
        }
        
        /// <summary>
        /// 得獎者詳情頁面
        /// </summary>
        [HttpGet("{winnerId}")]
        public async Task<ActionResult<WinnerViewModel>> Detail([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId, [FromRoute] string winnerId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.ParticipantPrizes
                .AsNoTracking()
                .Include(x => x.Participant)
                .ThenInclude(x => x.Claims.OrderBy(y => y.EventClaimId))
                .ThenInclude(x => x.Field)
                .Where(x => x.PrizeId == prizeId)
                .SingleOrDefaultAsync(x => x.ParticipantPrizeId == winnerId);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<WinnerViewModel>(entity);
            return View(model);
        }
        
        /// <summary>
        /// 刪除得獎者
        /// </summary>
        [HttpPost("{winnerId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId, [FromRoute] string winnerId)
        {
            if (!await _dbContext.Pools.AnyAsync(x => x.EventId == eventId && x.Id == poolId))
            {
                return NotFound();
            }
            var entity = await _dbContext.ParticipantPrizes
                .Where(x => x.PrizeId == prizeId)
                .SingleOrDefaultAsync(x => x.ParticipantPrizeId == winnerId);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.ParticipantPrizes.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Winner", new{ eventId, poolId, prizeId });
        }

        /// <summary>
        /// 匯出得獎者
        /// </summary>
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromRoute] string eventId, [FromRoute] string poolId, [FromRoute] string prizeId)
        {
            var pool = await _dbContext.Pools
                .AsNoTracking()
                .Include(x => x.Event)
                .ThenInclude(x => x.Fields)
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == poolId);
            if (pool == null)
            {
                return NotFound();
            }
            var entity = await _dbContext.Prizes
                .AsNoTracking()
                .Include(x => x.ParticipantPrizes)
                .ThenInclude(x => x.Participant)
                .ThenInclude(x => x.Claims.OrderBy(y => y.EventClaimId))
                .Where(x => x.PoolId == poolId)
                .SingleOrDefaultAsync(x => x.Id == prizeId);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"{pool.Name}");

                foreach (var field in pool.Event.Fields.Select((value, index) => new { value, index }))
                {
                    if (field.index == 0)
                    {
                        worksheet.Cell(1, field.index+1).Value = entity.Name;
                    }
                    else
                    {
                        worksheet.Range(worksheet.Cell(1, field.index), worksheet.Cell(1, field.index + 1)).Merge();
                    }
                    worksheet.Cell(2, field.index+1).Value = field.value.Value;
                }
                foreach (var item in entity.ParticipantPrizes.Select((value, index) => new { value, index }))
                {
                    foreach (var participant in item.value.Participant.Claims.Select((value, index) => new { value, index }))
                    {
                        worksheet.Cell(item.index + 3, participant.index + 1).Value = participant.value.Value;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, MimeTypeMap.GetMimeType("xlsx"), $"{pool.Event.Title}.xlsx");
                }
            }
        }
    }
}