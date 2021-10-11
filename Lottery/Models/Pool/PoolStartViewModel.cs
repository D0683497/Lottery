using System.Collections.Generic;
using Lottery.Models.Prize;

namespace Lottery.Models.Pool
{
    public class PoolStartViewModel : PoolViewModel
    {
        public List<PrizeViewModel> Prizes { get; set; }
    }
}