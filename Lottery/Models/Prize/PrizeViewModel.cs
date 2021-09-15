using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Prize
{
    public class PrizeViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }
        
        [JsonPropertyName("Name")]
        [Display(Name = "名稱")]
        public string Name { get; set; }
      
        [JsonPropertyName("Last")]
        [Display(Name = "剩餘數量")]
        public int Last { get; set; }
        
        [JsonPropertyName("Total")]
        [Display(Name = "數量")]
        public int Total { get; set; }
    }
}