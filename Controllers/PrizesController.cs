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
    public class PrizesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPrizeRepository _prizeRepository;
        private readonly IRoundRepository _roundRepository;

        public PrizesController(IMapper mapper, IPrizeRepository prizeRepository, IRoundRepository roundRepository)
        {
            _mapper = mapper;
            _prizeRepository = prizeRepository;
            _roundRepository = roundRepository;
        }

        [HttpGet("{prizeId}", Name = nameof(GetPrizeForRound))]
        public async Task<IActionResult> GetPrizeForRound(string roundId, string prizeId)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = await _prizeRepository.GetPrizeByIdAsync(prizeId);
            if (entity != null)
            {
                var model = _mapper.Map<PrizeViewModel>(entity);
                return Ok(model);
            }

            return NotFound();
        }

        [HttpPost(Name = nameof(CreatePrizeForRound))]
        public async Task<IActionResult> CreatePrizeForRound(string roundId, PrizeAddViewModel model)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<Prize>(model);

            _prizeRepository.AddPrize(roundId, entity);
            var result = await _prizeRepository.SaveAsync();

            if (result)
            {
                var returnModel = _mapper.Map<PrizeViewModel>(entity);
                return CreatedAtRoute(nameof(GetPrizeForRound), new {roundId = roundId, prizeId = returnModel.Id},
                    returnModel);
            }

            return BadRequest();
        }
    }
}