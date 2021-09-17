using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Lottery.Models.Prize;

namespace Lottery.Models.Participant
{
    public class ParticipantSearchViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }

        [JsonPropertyName("Prizes")]
        [Display(Name = "獎品")]
        public List<PrizeSearchViewModel> Prizes { get; set; }
    }
}
