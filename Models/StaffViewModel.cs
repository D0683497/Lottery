﻿using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class StaffViewModel
    {
        [Display(Name = "工作人員識別碼")]
        public string Id { get; set; }

        [Display(Name = "工作人員學號")]
        public string NID { get; set; }
        
        [Display(Name = "工作人員姓名")]
        public string Name { get; set; }

        [Display(Name = "工作人員系所")]
        public string Department { get; set; }
    }
}