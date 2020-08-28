﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class RoundAddAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "抽獎活動名稱")]
        public string Name { get; set; }

        public ICollection<PrizeAddViewModel> Prizes { get; set; }

        public ICollection<AttendeeAddViewModel> Attendees { get; set; }
    }
}