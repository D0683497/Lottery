using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ItemController(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        
    }
}