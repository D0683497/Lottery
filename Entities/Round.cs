using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    /// <summary>
    /// 一輪抽獎
    /// </summary>
    public class Round
    {
        public Round()
        {
            RoundId = Guid.NewGuid().ToString();
            RoundComplete = false;
        }
        
        [Key]
        public string RoundId { get; set; }

        /// <summary>
        /// 此輪抽獎名稱
        /// </summary>
        public string RoundName { get; set; }

        /// <summary>
        /// 此輪抽獎是否完成
        /// </summary>
        public bool RoundComplete { get; set; }

        /// <summary>
        /// 此輪獎品
        /// </summary>
        public ICollection<Prize> Prizes { get; set; }

        /// <summary>
        /// 此輪中獎者
        /// </summary>
        public ICollection<Winner> Winners { get; set; }

        /// <summary>
        /// 此輪參與者
        /// </summary>
        public ICollection<Attendee> Attendees { get; set; }
    }
}