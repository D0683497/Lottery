using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class Student
    {
        public Student()
        {
            StudentId = Guid.NewGuid().ToString();
        }
        
        [Key]
        public string StudentId { get; set; }

        /// <summary>
        /// 學生學號
        /// </summary>
        public string StudentNID { get; set; }
        
        /// <summary>
        /// 學生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 學生系所
        /// </summary>
        public string StudentDepartment { get; set; }
        
        public string WinnerId { get; set; }
        public Winner Winner { get; set; }
        
        public string RoundId { get; set; }
        public Round Round { get; set; }
    }
}