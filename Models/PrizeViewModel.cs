using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class PrizeViewModel
    {
        [Display(Name = "獎品識別碼")]
        public string Id { get; set; }

        [Display(Name = "獎品名稱")]
        public string Name { get; set; }

        [Display(Name = "獎品圖片")]
        public string Image { get; set; }

        [Display(Name = "獎品數量")]
        public int Number { get; set; }

        [Display(Name = "獎品是否抽獎完成")]
        public bool Complete { get; set; }

        [Display(Name = "獎品排序")]
        public int Order { get; set; }
    }
}