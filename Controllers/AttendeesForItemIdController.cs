using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Entities;
using Lottery.Helpers;
using Lottery.Models.Attendee;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/items/{itemId}/attendees")]
    public class AttendeesForItemIdController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IItemRepository _itemRepository;

        public AttendeesForItemIdController(IMapper mapper, IAttendeeRepository attendeeRepository, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _attendeeRepository = attendeeRepository;
            _itemRepository = itemRepository;
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
    }
}