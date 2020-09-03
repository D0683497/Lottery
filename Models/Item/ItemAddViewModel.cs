using System.ComponentModel.DataAnnotations;

namespace Lottery.Models.Item
{
    public class ItemAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "清單名稱")]
        public string Name { get; set; }
    }
}