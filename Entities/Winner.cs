using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class Winner
    {
        public Winner()
        {
            WinnerId = Guid.NewGuid().ToString();
        }
        
        [Key]
        public string WinnerId { get; set; }
        
        /// <summary>
        /// 中獎的人
        /// </summary>
        public string AttendeeId { get; set; }
        public Attendee Attendee { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; }
    }
}