using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class WinnerAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "得獎者名單")]
        public ICollection<AttendeeViewModel> Attendees { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "獲得獎品")]
        public PrizeViewModel Prize { get; set; }
    }
}