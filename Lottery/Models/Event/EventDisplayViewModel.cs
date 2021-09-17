using System.Collections.Generic;
using Lottery.Models.Field;
using Lottery.Models.Pool;

namespace Lottery.Models.Event
{
    public class EventDisplayViewModel : EventViewModel
    {
        public IEnumerable<PoolViewModel> Pools { get; set; }

        public List<FieldViewModel> Fields { get; set; }
    }
}