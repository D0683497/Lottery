using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Field
{
    public class FieldAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [JsonPropertyName("Key")]
        [Display(Name = "搜尋")]
        public bool Key { get; set; }
        
        [Required(ErrorMessage = "{0}是必填的")]
        [JsonPropertyName("Show")]
        [Display(Name = "顯示")]
        public bool Show { get; set; }
        
        [Required(ErrorMessage = "{0}是必填的")]
        [JsonPropertyName("Security")]
        [Display(Name = "機密")]
        public bool Security { get; set; }
        
        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(10, ErrorMessage = "{0}最多{1}位")]
        [DataType(DataType.Text)]
        [JsonPropertyName("Value")]
        [Display(Name = "值")]
        public string Value { get; set; }
    }
}