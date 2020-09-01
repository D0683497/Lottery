using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class Staff
    {
        public Staff()
        {
            StaffId = Guid.NewGuid().ToString();
        }
        
        [Key]
        public string StaffId { get; set; }

        /// <summary>
        /// 學生學號
        /// </summary>
        public string StaffNID { get; set; }
        
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 學生系所
        /// </summary>
        public string StaffDepartment { get; set; }
        
        public string WinnerId { get; set; }
        public Winner Winner { get; set; }
        
        public string RoundId { get; set; }
        public Round Round { get; set; }
    }
}