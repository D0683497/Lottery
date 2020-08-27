using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class Attendee
    {
        public Attendee()
        {
            AttendeeId = Guid.NewGuid().ToString();
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

        public string WinnerId { get; set; }
        public Winner Winner { get; set; }
        
        public string RoundId { get; set; }
        public Round Round { get; set; }
    }
}