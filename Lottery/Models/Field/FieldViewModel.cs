using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Field
{
    public class FieldViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }
        
        [JsonPropertyName("Key")]
        [Display(Name = "搜尋")]
        public bool Key { get; set; }
        
        [JsonPropertyName("Show")]
        [Display(Name = "顯示")]
        public bool Show { get; set; }
        
        [JsonPropertyName("Security")]
        [Display(Name = "機密")]
        public bool Security { get; set; }
        
        [JsonPropertyName("Value")]
        [Display(Name = "值")]
        public string Value { get; set; }
    }
}