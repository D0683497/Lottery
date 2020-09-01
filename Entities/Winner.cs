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
        /// 中獎學生名單
        /// </summary>
        public ICollection<Student> Students { get; set; }
        
        /// <summary>
        /// 中獎工作人員名單
        /// </summary>
        public ICollection<Staff> Staffs { get; set; }

        public string RoundId { get; set; }
        public Round Round { get; set; }
    }
}