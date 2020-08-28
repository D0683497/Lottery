using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Models
{
    public class RoundViewModel
    {
        [Display(Name = "抽獎活動識別碼")]
        public string Id { get; set; }

        [Display(Name = "抽獎活動名稱")]
        public string Name { get; set; }

        [Display(Name = "抽獎活動是否完成")]
        public bool Complete { get; set; }

        public ICollection<PrizeViewModel> Prizes { get; set; }
    }
}