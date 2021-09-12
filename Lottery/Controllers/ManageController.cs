using AutoMapper;
using Lottery.Data;
using Lottery.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lottery.Controllers
{
    [AuthAuthorize]
    [Route("manage")]
    public class ManageController : Controller
    {
        private readonly ILogger<ManageController> _logger;
        private readonly LotteryDbContext _dbContext;
        private readonly IMapper _mapper;

        public ManageController(ILogger<ManageController> logger, LotteryDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 管理頁面
        /// </summary>
        [HttpGet("")]
        public IActionResult Index() => View();
        
        /// <summary>
        /// 網站設定
        /// </summary>
        [HttpGet("setting")]
        public IActionResult Setting() => View();
        
        /// <summary>
        /// 管理申請表頁面
        /// </summary>
        [HttpGet("apply")]
        public IActionResult Apply() => View();
    }
}