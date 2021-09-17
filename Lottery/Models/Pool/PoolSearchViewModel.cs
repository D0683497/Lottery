using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Lottery.Models.Participant;
using Lottery.Models.Prize;

namespace Lottery.Models.Pool
{
    public class PoolSearchViewModel : PoolViewModel
    {
        [JsonPropertyName("Prizes")]
        [Display(Name = "獎品")]
        public List<PrizeViewModel> Prizes { get; set; }

        [JsonPropertyName("Participant")]
        [Display(Name = "參與者")]
        public ParticipantSearchViewModel Participant { get; set; }
    }
}
