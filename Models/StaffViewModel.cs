using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class StaffViewModel
    {
        [Display(Name = "學生識別碼")]
        public string Id { get; set; }

        [Display(Name = "學生學號")]
        public string NID { get; set; }
        
        [Display(Name = "學生姓名")]
        public string Name { get; set; }

        [Display(Name = "學生系所")]
        public string Department { get; set; }
    }
}