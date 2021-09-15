using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Data;
using Lottery.Entities.Activity;
using Lottery.Helpers;
using Lottery.Models.Field;
using Lottery.Models.Pool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [AuthAuthorize]
    [Route("manage/event/{eventId}/field")]
    public class FieldController : Controller
    {
        private readonly ILogger<FieldController> _logger;
        private readonly LotteryDbContext _dbContext;
        private readonly IMapper _mapper;

        public FieldController(ILogger<FieldController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 管理欄位頁面
        /// </summary>
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<FieldViewModel>>> Index([FromRoute] string eventId)
        {
            if (!await _dbContext.Events.AnyAsync(x => x.Id == eventId))
            {
                return NotFound();
            }
            var entities = await _dbContext.Fields
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Where(x => x.EventId == eventId)
                .ToListAsync();
            var models = _mapper.Map<IEnumerable<FieldViewModel>>(entities);
            return View(models);
        }
        
        /// <summary>
        /// 欄位詳情頁面
        /// </summary>
        [HttpGet("{fieldId}")]
        public async Task<ActionResult<FieldViewModel>> Detail([FromRoute] string eventId, [FromRoute] string fieldId)
        {
            var entity = await _dbContext.Fields
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
        /// 新增欄位
        /// </summary>
        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolAddViewModel>> Add([FromRoute] string eventId, [FromForm] FieldAddViewModel model)
        {
            var act = await _dbContext.Events
                .Include(x => x.Fields)
                .SingleOrDefaultAsync(x => x.Id == eventId);
            if (act == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Field>(model);
                act.Fields.Add(entity);
                _dbContext.Events.Update(act);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Detail", "Field", new{ eventId, fieldId = entity.Id });
            }
            return View(model);
        }
        
        /// <summary>
        /// 編輯欄位頁面
        /// </summary>
        [HttpGet("{fieldId}/edit")]
        public async Task<ActionResult<FieldEditViewModel>> Edit([FromRoute] string eventId, [FromRoute] string fieldId)
        {
            var entity = await _dbContext.Fields
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
        [HttpPost("{fieldId}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<PoolEditViewModel>> Edit([FromRoute] string eventId, [FromRoute] string fieldId, [FromForm] FieldEditViewModel model)
        {
            var entity = await _dbContext.Fields
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == fieldId);
            if (entity == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var updateEntity = _mapper.Map(model, entity);
                _dbContext.Fields.Update(updateEntity);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Detail", "Field", new{ eventId, fieldId });
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除欄位
        /// </summary>
        [HttpPost("{fieldId}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string eventId, [FromRoute] string fieldId)
        {
            var entity = await _dbContext.Fields
                .Include(x => x.ParticipantClaims)
                .Where(x => x.EventId == eventId)
                .SingleOrDefaultAsync(x => x.Id == fieldId);
            if (entity == null)
            {
                return NotFound();
            }
            _dbContext.Fields.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Field", new{ eventId });
        }
    }
}