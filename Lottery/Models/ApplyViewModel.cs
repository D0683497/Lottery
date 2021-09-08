using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models
{
    public class ApplyViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(50, ErrorMessage = "{0}最多{1}位")]
        [DataType(DataType.Text)]
        [JsonPropertyName("Name")]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(256, ErrorMessage = "{0}最多{1}位")]
        [EmailAddress(ErrorMessage = "{0}格式錯誤")]
        [DataType(DataType.EmailAddress)]
        [JsonPropertyName("Email")]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(500, ErrorMessage = "{0}最多{1}位")]
        [DataType(DataType.MultilineText)]
        [JsonPropertyName("Purpose")]
        [Display(Name = "用途")]
        public string Purpose { get; set; }
    }
}