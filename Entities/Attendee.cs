using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class Attendee
    {
        public Attendee()
        {
            AttendeeId = Guid.NewGuid().ToString();
            AttendeeIsAwarded = false;
        }

        [Key]
        public string AttendeeId { get; set; }
        
        /// <summary>
        /// 參與者學號
        /// </summary>
        public string AttendeeNID { get; set; }
        
        /// <summary>
        /// 參與者姓名
        /// </summary>
        public string AttendeeName { get; set; }

        /// <summary>
        /// 參與者系所
        /// </summary>
        public string AttendeeDepartment { get; set; }

        /// <summary>
        /// 參與者是否得獎
        /// </summary>
        public bool AttendeeIsAwarded { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; }

        public Winner Winner { get; set; }
    }
}