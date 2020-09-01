using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class StudentAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "學生學號")]
        public string NID { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "學生姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "學生系所")]
        public string Department { get; set; }
    }
}