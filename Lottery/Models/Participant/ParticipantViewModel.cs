using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Participant
{
    public class ParticipantViewModel
    {
        [JsonPropertyName("Id")]
        [Display(Name = "識別碼")]
        public string Id { get; set; }
        
        [JsonPropertyName("Claims")]
        [Display(Name = "聲明")]
        public List<ParticipantClaimViewModel> Claims { get; set; }
    }
}