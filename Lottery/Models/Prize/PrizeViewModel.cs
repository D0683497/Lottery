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
        
        [JsonPropertyName("Quantity")]
        [Display(Name = "數量")]
        public int Quantity { get; set; }
    }
}