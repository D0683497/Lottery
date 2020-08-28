using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class AttendeeAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "參與者學號")]
        public string NID { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "參與者姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "參與者系所")]
        public string Department { get; set; }
    }
}