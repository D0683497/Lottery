using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Lottery.Models.Participant;

namespace Lottery.Models.Winner
{
    public class WinnerViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }

        [JsonPropertyName("Participant")]
        [Display(Name = "參與者")]
        public ParticipantViewModel Participant { get; set; }
    }
}