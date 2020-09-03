using AutoMapper;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttendeeRepository _attendeeRepository;

        public AttendeesController(IMapper mapper, IAttendeeRepository attendeeRepository)
        {
            _mapper = mapper;
            _attendeeRepository = attendeeRepository;
        }
    }
}