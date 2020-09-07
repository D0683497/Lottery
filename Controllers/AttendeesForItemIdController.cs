using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Lottery.Data;
using Lottery.Entities;
using Lottery.Helpers;
using Lottery.Models.Attendee;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/items/{itemId}/attendees")]
    public class AttendeesForItemIdController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IWinnerRepository _winnerRepository;

        public AttendeesForItemIdController(
            IMapper mapper, 
            IAttendeeRepository attendeeRepository, 
            IItemRepository itemRepository, 
            IWinnerRepository winnerRepository)
        {
            _mapper = mapper;
            _attendeeRepository = attendeeRepository;
            _itemRepository = itemRepository;
            _winnerRepository = winnerRepository;
        }
        
        [HttpGet("all", Name = nameof(GetAllAttendeesForItemId))]
        public async Task<ActionResult<IEnumerable<AttendeeViewModel>>> GetAllAttendeesForItemId(string itemId)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }

            var entities = await _attendeeRepository.GetAllAttendeesForItemIdAsync(itemId);

            var models = _mapper.Map<IEnumerable<AttendeeViewModel>>(entities);

            return Ok(models);
        }

        [HttpGet("length", Name = nameof(GetAllAttendeesLengthForItemId))]
        public async Task<ActionResult<int>> GetAllAttendeesLengthForItemId(string itemId)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }
            
            var model = await _attendeeRepository.GetAllAttendeesLengthForItemIdAsync(itemId);

            return Ok(model);
        }

        [HttpGet(Name = nameof(GetAttendeesForItemId))]
        public async Task<ActionResult<IEnumerable<AttendeeViewModel>>> GetAttendeesForItemId(string itemId, [FromQuery] AttendeeResourceParameters parameters)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }
            
            var skipNumber = parameters.PageSize * (parameters.PageNumber - 1);
            var takeNumber = parameters.PageSize;

            var entities = await _attendeeRepository.GetAttendeesForItemIdAsync(itemId, skipNumber, takeNumber);

            var models = _mapper.Map<IEnumerable<AttendeeViewModel>>(entities);

            return Ok(models);
        }

        [HttpGet("{attendeeId}", Name = nameof(GetAttendeeByIdForItemId))]
        public async Task<ActionResult<AttendeeViewModel>> GetAttendeeByIdForItemId(string itemId, string attendeeId)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }

            var entity = await _attendeeRepository.GetAttendeeByIdForItemIdAsync(itemId, attendeeId);

            var model = _mapper.Map<AttendeeViewModel>(entity);

            return Ok(model);
        }

        [HttpPost(Name = nameof(CreateAttendeeForItemId))]
        public async Task<IActionResult> CreateAttendeeForItemId(string itemId, AttendeeAddViewModel model)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<Attendee>(model);
            
            _attendeeRepository.CreateAttendeeForItemId(itemId, entity);
            
            var result = await _attendeeRepository.SaveAsync();
            if (!result) return BadRequest();

            var returnModel = _mapper.Map<AttendeeViewModel>(entity);
            
            return CreatedAtRoute(nameof(GetAttendeeByIdForItemId), new { itemId, attendeeId = returnModel.Id }, returnModel);
        }

        [HttpPost("collections", Name = nameof(CreateAttendeesForItemId))]
        public async Task<IActionResult> CreateAttendeesForItemId(string itemId, IEnumerable<AttendeeAddViewModel> models)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }

            var entities = _mapper.Map<IEnumerable<Attendee>>(models);
            
            _attendeeRepository.CreateAttendeesForItemId(itemId, entities);
            
            var result = await _attendeeRepository.SaveAsync();
            if (!result) return BadRequest();
            
            // var returnModel = _mapper.Map<IEnumerable<AttendeeViewModel>>(entities);

            return Ok();
        }
        
        [HttpGet("file/xlsx", Name = nameof(GetAttendeesXlsxForItemId))]
        public async Task<IActionResult> GetAttendeesXlsxForItemId(string itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var entity = await _attendeeRepository.GetAllAttendeesForItemIdAsync(itemId);

            var models = _mapper.Map<List<AttendeeFileViewModel>>(entity);
            
            var fileName = $"{item.ItemName}.xlsx";

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add($"{item.ItemName}列表");
                    worksheet.Cell(1, 1).Value = "學號";
                    worksheet.Cell(1, 2).Value = "姓名";
                    worksheet.Cell(1, 3).Value = "系所";
                    worksheet.Cell(1, 4).Value = "是否得獎";
                    for (int i = 1; i <= models.Count; i++)
                    {
                        worksheet.Cell(i + 1, 1).Value = models[i - 1].NID;
                        worksheet.Cell(i + 1, 2).Value = models[i - 1].Name;
                        worksheet.Cell(i + 1, 3).Value = models[i - 1].Department;
                        worksheet.Cell(i + 1, 4).Value = models[i - 1].IsAwarded;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, ContentType.Xlsx, fileName);
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("file/csv", Name = nameof(GetAttendeesCsvForItemId))]
        public async Task<IActionResult> GetAttendeesCsvForItemId(string itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var entity = await _attendeeRepository.GetAllAttendeesForItemIdAsync(itemId);

            var models = _mapper.Map<List<AttendeeFileViewModel>>(entity);
            
            var fileName = $"{item.ItemName}.csv";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("學號, 姓名, 系所, 是否得獎");
            foreach (var m in models)
            {
                stringBuilder.AppendLine($"{m.NID}, {m.Name}, {m.Department}, {m.IsAwarded}");
            }

            var content = Encoding.UTF8.GetBytes(stringBuilder.ToString());

            return File(content, ContentType.Csv, fileName);
        }
        
        [HttpGet("file/json", Name = nameof(GetAttendeesJsonForItemId))]
        public async Task<IActionResult> GetAttendeesJsonForItemId(string itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var entity = await _attendeeRepository.GetAllAttendeesForItemIdAsync(itemId);

            var models = _mapper.Map<List<AttendeeFileViewModel>>(entity);
            
            var fileName = $"{item.ItemName}.json";

            var content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(models));

            return File(content, ContentType.Json, fileName);
        }

        [HttpGet("random", Name = nameof(GetAttendeeRandomForItemId))]
        public async Task<ActionResult<AttendeeViewModel>> GetAttendeeRandomForItemId(string itemId)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }
            
            var entity =  _attendeeRepository.GetAttendeeRandomForItemId(itemId);
            if (entity == null) // 清單中的參與者都獲獎了
            {
                return NoContent();
            }

            entity.AttendeeIsAwarded = true;
            _attendeeRepository.UpdateAttendee(entity);
            _winnerRepository.CreateWinnerForItemIdAttendeeId(itemId, entity.AttendeeId, new Winner());
            var result = await _winnerRepository.SaveAsync();
            if (!result) return BadRequest();
            
            var model = _mapper.Map<AttendeeViewModel>(entity);

            return Ok(model);
        }
    }
}