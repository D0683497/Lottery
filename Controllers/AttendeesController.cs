using System.Threading.Tasks;
using AutoMapper;
using Lottery.Entities;
using Lottery.Models;
using Lottery.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/rounds/{roundId}/[controller]")]
    public class AttendeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IRoundRepository _roundRepository;

        public AttendeesController(IMapper mapper, IAttendeeRepository attendeeRepository, IRoundRepository roundRepository)
        {
            _mapper = mapper;
            _attendeeRepository = attendeeRepository;
            _roundRepository = roundRepository;
        }

        [HttpGet("{attendeeId}", Name = nameof(GetAttendeeForRound))]
        public async Task<IActionResult> GetAttendeeForRound(string roundId, string attendeeId)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = await _attendeeRepository.GetPrizeByIdAsync(attendeeId);
            if (entity != null)
            {
                var model = _mapper.Map<AttendeeViewModel>(entity);
                return Ok(model);
            }

            return NotFound();
        }

        [HttpPost(Name = nameof(CreateAttendeeForRound))]
        public async Task<IActionResult> CreateAttendeeForRound(string roundId, AttendeeAddViewModel model)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<Attendee>(model);

            _attendeeRepository.AddPrize(roundId, entity);
            var result = await _attendeeRepository.SaveAsync();

            if (result)
            {
                var returnModel = _mapper.Map<AttendeeViewModel>(entity);
                return CreatedAtRoute(nameof(GetAttendeeForRound), new { roundId = roundId, attendeeId = returnModel.Id },
                    returnModel);
            }

            return BadRequest();
        }
    }
}