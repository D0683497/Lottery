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
        /// 中獎名單
        /// </summary>
        public ICollection<Attendee> Attendees { get; set; }
        
        /// <summary>
        /// 獲得的獎品
        /// </summary>
        public Prize Prize { get; set; }
        
        public string RoundId { get; set; }
        public Round Round { get; set; }
    }
}