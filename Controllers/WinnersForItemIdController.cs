using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Helpers;
using Lottery.Models.Attendee;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}