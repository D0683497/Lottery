using System.ComponentModel.DataAnnotations;

namespace Lottery.Models.Item
{
    public class ItemViewModel
    {
        [Display(Name = "清單識別碼")]
        public string Id { get; set; }
        
        [Display(Name = "清單名稱")]
        public string Name { get; set; }
    }
}