using AutoMapper;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WinnersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWinnerRepository _winnerRepository;

        public WinnersController(IMapper mapper, IWinnerRepository winnerRepository)
        {
            _mapper = mapper;
            _winnerRepository = winnerRepository;
        }
    }
}