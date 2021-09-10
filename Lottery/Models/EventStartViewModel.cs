using System.Collections.Generic;
using Lottery.Models.Event;
using Lottery.Models.Pool;
using Lottery.Models.Prize;

namespace Lottery.Models
{
    public class EventStartViewModel
    {
        public EventViewModel Event { get; set; }

        public PoolViewModel Pool { get; set; }

        public IEnumerable<PrizeViewModel> Prizes { get; set; }
    }
}