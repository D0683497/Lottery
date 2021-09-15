using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Prize
{
    public class PrizeAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(50, ErrorMessage = "{0}最多{1}位")]
        [DataType(DataType.Text)]
        [JsonPropertyName("Name")]
        [Display(Name = "名稱")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [Range(1, int.MaxValue, ErrorMessage = "{0}必須大於{1}")]
        [DataType(DataType.Text)]
        [JsonPropertyName("Total")]
        [Display(Name = "數量")]
        public int Total { get; set; }
    }
}