using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Prize
{
    public class PrizeSearchViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }

        [JsonPropertyName("PrizeId")]
        [Display(Name = "獎品識別碼")]
        public string PrizeId { get; set; }

        [JsonPropertyName("ParticipantId")]
        [Display(Name = "參加與者識別碼")]
        public string ParticipantId { get; set; }
    }
}
