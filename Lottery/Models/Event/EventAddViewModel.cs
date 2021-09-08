using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Event
{
    public class EventAddViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(50, ErrorMessage = "{0}最多{1}位")]
        [DataType(DataType.Text)]
        [JsonPropertyName("Title")]
        [Display(Name = "標題")]
        public string Title { get; set; }
        
        [MaxLength(300, ErrorMessage = "{0}最多{1}位")]
        [DataType(DataType.MultilineText)]
        [JsonPropertyName("SubTitle")]
        [Display(Name = "副標題")]
        public string SubTitle { get; set; }
        
        [MaxLength(100, ErrorMessage = "{0}最多{1}位")]
        [RegularExpression(@"^[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_]*$", ErrorMessage = "{0}只能是英文、數字、底線、連字號")]
        [DataType(DataType.Text)]
        [JsonPropertyName("Url")]
        [Display(Name = "網址")]
        public string Url { get; set; }
    }
}