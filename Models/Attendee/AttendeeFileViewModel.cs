using System.ComponentModel.DataAnnotations;

namespace Lottery.Models.Attendee
{
    public class AttendeeFileViewModel
    {
        [Display(Name = "參與者學號")]
        public string NID { get; set; }
        
        [Display(Name = "參與者姓名")]
        public string Name { get; set; }

        [Display(Name = "參與者系所")]
        public string Department { get; set; }
        
        [Display(Name = "參與者是否得獎")]
        public bool IsAwarded { get; set; }
    }
}