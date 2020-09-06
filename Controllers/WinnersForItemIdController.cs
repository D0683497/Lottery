using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Lottery.Data;
using Lottery.Helpers;
using Lottery.Models.Attendee;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/items/{itemId}/winners")]
    public class WinnersForItemIdController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWinnerRepository _winnerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IAttendeeRepository _attendeeRepository;

        public WinnersForItemIdController(
            IMapper mapper, 
            IWinnerRepository winnerRepository, 
            IItemRepository itemRepository, 
            IAttendeeRepository attendeeRepository)
        {
            _mapper = mapper;
            _winnerRepository = winnerRepository;
            _itemRepository = itemRepository;
            _attendeeRepository = attendeeRepository;
        }
        
        [HttpGet("length", Name = nameof(GetAllWinnersLengthForItemId))]
        public async Task<ActionResult<int>> GetAllWinnersLengthForItemId(string itemId)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }
            
            var model = await _winnerRepository.GetAllWinnersLengthForItemIdAsync(itemId);

            return Ok(model);
        }

        [HttpGet(Name = nameof(GetWinnersForItemId))]
        public async Task<ActionResult<IEnumerable<AttendeeViewModel>>> GetWinnersForItemId(string itemId, [FromQuery] WinnerResourceParameters parameters)
        {
            if (!await _itemRepository.ExistItemByIdAsync(itemId))
            {
                return NotFound();
            }
            
            var skipNumber = parameters.PageSize * (parameters.PageNumber - 1);
            var takeNumber = parameters.PageSize;

            var entities = await _winnerRepository.GetWinnersForItemIdAsync(itemId, skipNumber, takeNumber);
            var models = _mapper.Map<IEnumerable<AttendeeViewModel>>(entities);

            return Ok(models);
        }
        
        [HttpGet("file/xlsx", Name = nameof(GetWinnersXlsxForItemId))]
        public async Task<IActionResult> GetWinnersXlsxForItemId(string itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var entity = await _winnerRepository.GetAllWinnersForItemIdAsync(itemId);

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
                    for (int i = 1; i <= models.Count; i++)
                    {
                        worksheet.Cell(i + 1, 1).Value = models[i - 1].NID;
                        worksheet.Cell(i + 1, 2).Value = models[i - 1].Name;
                        worksheet.Cell(i + 1, 3).Value = models[i - 1].Department;
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
        
        [HttpGet("file/csv", Name = nameof(GetWinnersCsvForItemId))]
        public async Task<IActionResult> GetWinnersCsvForItemId(string itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var entity = await _winnerRepository.GetAllWinnersForItemIdAsync(itemId);

            var models = _mapper.Map<List<AttendeeFileViewModel>>(entity);
            
            var fileName = $"{item.ItemName}.csv";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("學號, 姓名, 系所");
            foreach (var m in models)
            {
                stringBuilder.AppendLine($"{m.NID}, {m.Name}, {m.Department}");
            }

            var content = Encoding.UTF8.GetBytes(stringBuilder.ToString());

            return File(content, ContentType.Csv, fileName);
        }
        
        [HttpGet("file/json", Name = nameof(GetWinnersJsonForItemId))]
        public async Task<IActionResult> GetWinnersJsonForItemId(string itemId)
        {
            var item = await _itemRepository.GetItemByIdAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            var entity = await _winnerRepository.GetAllWinnersForItemIdAsync(itemId);

            // 移除 IsAwarded
            var models = _mapper.Map<List<AttendeeFileViewModel>>(entity)
                .Select(e => new {e.NID, e.Name, e.Department})
                .ToList();

            var fileName = $"{item.ItemName}.json";

            var content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(models));

            return File(content, ContentType.Json, fileName);
        }
    }
}