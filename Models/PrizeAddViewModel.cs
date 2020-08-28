using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class PrizeAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [Display(Name = "獎品名稱")]
        public string Name { get; set; }

        [Display(Name = "獎品圖片")]
        public string Image { get; set; }


        [Display(Name = "獎品數量")]
        public int? Number { get; set; }

        [Display(Name = "獎品排序")]
        public int? Order { get; set; }
    }
}