using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Pool
{
    public class PoolViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }
        
        [JsonPropertyName("Name")]
        [Display(Name = "名稱")]
        public string Name { get; set; }
    }
}