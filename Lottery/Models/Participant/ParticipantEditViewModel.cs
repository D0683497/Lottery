using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Lottery.Models.Field;

namespace Lottery.Models.Participant
{
    public class ParticipantEditViewModel
    {
        [JsonPropertyName("Value")]
        [Display(Name = "值")]
        public string Value { get; set; }
        
        [JsonPropertyName("Field")]
        [Display(Name = "欄位")]
        public FieldViewModel Field { get; set; }
    }
}