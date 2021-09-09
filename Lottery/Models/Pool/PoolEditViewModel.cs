﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lottery.Models.Pool
{
    public class PoolEditViewModel
    {
        [Required(ErrorMessage = "{0}是必填的")]
        [MaxLength(50, ErrorMessage = "{0}最多{1}位")]
        [DataType(DataType.Text)]
        [JsonPropertyName("Name")]
        [Display(Name = "名稱")]
        public string Name { get; set; }
    }
}