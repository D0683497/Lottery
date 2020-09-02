using System.Collections.Generic;
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
    public class StaffsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoundRepository _roundRepository;
        private readonly IStaffRepository _staffRepository;

        public StaffsController(
            IMapper mapper,
            IRoundRepository roundRepository,
            IStaffRepository staffRepository)
        {
            _mapper = mapper;
            _roundRepository = roundRepository;
            _staffRepository = staffRepository;
        }

        [HttpGet(Name = nameof(GetAllStaffsForRound))]
        public async Task<IActionResult> GetAllStaffsForRound(string roundId)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entities = await _staffRepository.GetAllStaffsForRoundAsync(roundId);

            var models = _mapper.Map<IEnumerable<StaffViewModel>>(entities);

            return Ok(models);
        }

        [HttpGet("{staffId}", Name = nameof(GetStaffForRound))]
        public async Task<IActionResult> GetStaffForRound(string roundId, string staffId)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = await _staffRepository.GetStaffForRound(roundId, staffId);
            if (entity == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<StaffViewModel>(entity);

            return Ok(model);
        }

        [HttpPost(Name = nameof(CreateStaff))]
        public async Task<IActionResult> CreateStaff(string roundId, StaffAddViewModel model)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<Staff>(model);

            _staffRepository.AddStaffForRound(roundId, entity);
            var result = await _staffRepository.SaveAsync();
            
            if (result)
            {
                var returnModel = _mapper.Map<StaffViewModel>(entity);
                return CreatedAtRoute(nameof(GetStaffForRound), new { roundId, staffId = returnModel.Id }, returnModel);
            }

            return BadRequest();
        }

        [HttpGet("random", Name = nameof(GetRandomStaff))]
        public async Task<IActionResult> GetRandomStaff(string roundId)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = await _staffRepository.GetRandomStaffForRound(roundId);

            var model = _mapper.Map<StaffViewModel>(entity);

            return Ok(model);
        }
    }
}