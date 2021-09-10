using System.Collections.Generic;
using Lottery.Models.Event;
using Lottery.Models.Pool;

namespace Lottery.Models
{
    public class EventDisplayViewModel : EventViewModel
    {
        public IEnumerable<PoolViewModel> Pools { get; set; }
    }
}