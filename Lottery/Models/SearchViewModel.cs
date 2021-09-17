using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Lottery.Models.Pool;

namespace Lottery.Models
{
    public class SearchViewModel
    {
        [JsonPropertyName("Pools")]
        [Display(Name = "講池")]
        public List<PoolSearchViewModel> Pools { get; set; }
    }
}
