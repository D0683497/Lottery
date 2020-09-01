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
    [Route("api/[controller]")]
    public class RoundsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoundRepository _roundRepository;

        public RoundsController(IMapper mapper, IRoundRepository roundRepository)
        {
            _mapper = mapper;
            _roundRepository = roundRepository;
        }

        [HttpGet(Name = nameof(GetAllRound))]
        public async Task<IActionResult> GetAllRound()
        {
            var entities = await _roundRepository.GetAllRoundAsync();

            var models = _mapper.Map<IEnumerable<RoundViewModel>>(entities);

            return Ok(models);
        }

        [HttpGet("{roundId}", Name = nameof(GetRoundById))]
        public async Task<IActionResult> GetRoundById(string roundId)
        {
            var entity = await _roundRepository.GetRoundByIdAsync(roundId);

            var model = _mapper.Map<RoundViewModel>(entity);

            return Ok(model);
        }

        [HttpPost(Name = nameof(CreateRound))]
        public async Task<IActionResult> CreateRound(RoundAddAddViewModel model)
        {
            var entity = _mapper.Map<Round>(model);

            _roundRepository.AddRound(entity);

            var result = await _roundRepository.SaveAsync();

            if (result)
            {
                var returnModel = _mapper.Map<RoundViewModel>(entity);

                return CreatedAtRoute(nameof(GetRoundById), new { roundId = returnModel.Id }, returnModel);
            }

            return BadRequest();
        }

        [HttpDelete("{roundId}", Name = nameof(DeleteRound))]
        public async Task<IActionResult> DeleteRound(string roundId)
        {
            var entity = await _roundRepository.GetRoundByIdAsync(roundId);

            if (entity == null)
            {
                return NotFound();
            }

            _roundRepository.DeleteRound(entity);

            var result = await _roundRepository.SaveAsync();

            if (result)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}