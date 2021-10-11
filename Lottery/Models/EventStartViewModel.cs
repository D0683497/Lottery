using System.Collections.Generic;
using Lottery.Models.Event;
using Lottery.Models.Pool;
using Lottery.Models.Prize;

namespace Lottery.Models
{
    public class EventStartViewModel : EventViewModel
    {
        public List<PoolStartViewModel> Pools { get; set; }
    }
}