using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class WinnerViewModel
    {
        [Display(Name = "得獎名單識別碼")]
        public string Id { get; set; }

        [Display(Name = "得獎者名單")]
        public ICollection<AttendeeViewModel> Attendees { get; set; }

        [Display(Name = "獲得獎品")]
        public PrizeViewModel Prize { get; set; }
    }
}