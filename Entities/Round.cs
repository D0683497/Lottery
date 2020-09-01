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
        }
        
        [Key]
        public string RoundId { get; set; }

        /// <summary>
        /// 此輪抽獎名稱
        /// </summary>
        public string RoundName { get; set; }

        /// <summary>
        /// 此輪中獎者
        /// </summary>
        public ICollection<Winner> Winners { get; set; }

        /// <summary>
        /// 此輪學生
        /// </summary>
        public ICollection<Student> Students { get; set; }
        
        /// <summary>
        /// 此輪工作人員
        /// </summary>
        public ICollection<Staff> Staffs { get; set; }
    }
}