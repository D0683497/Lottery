using System.Threading.Tasks;
using AutoMapper;
using Lottery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemController(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }
    
        [HttpGet("all", Name = nameof(GetAllItems))]
        public async Task<IActionResult> GetAllItems()
        {
            
        }

        [HttpGet(Name = nameof(GetItems))]
        public async Task<IActionResult> GetItems()
        {
            
        }

        [HttpGet("{itemId}", Name = nameof(GetItemById))]
        public async Task<IActionResult> GetItemById()
        {
            
        }
    }
}