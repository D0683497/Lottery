using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class StaffAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "工作人員學號")]
        public string NID { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "工作人員姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "工作人員系所")]
        public string Department { get; set; }
    }
}