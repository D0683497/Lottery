using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Event
{
    public class EventViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }

        [JsonPropertyName("End")]
        [Display(Name = "活動狀態")]
        public bool End { get; set; }

        [JsonPropertyName("Title")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [JsonPropertyName("SubTitle")]
        [Display(Name = "副標題")]
        public string SubTitle { get; set; }

        [JsonPropertyName("Url")]
        [Display(Name = "網址")]
        public string Url { get; set; }
    }
}