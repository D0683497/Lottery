using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 活動欄位
    /// </summary>
    [Index(nameof(Value), Name = "FieldValueIndex", IsUnique = true)]
    public class Field
    {
        /// <summary>
        /// 活動欄位識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = new SequentialGuidValueGenerator().Next(null!).ToString();

        /// <summary>
        /// 搜尋
        /// </summary>
        [Required]
        public bool Key { get; set; } = false;

        /// <summary>
        /// 顯示
        /// </summary>
        [Required]
        public bool Show { get; set; } = false;

        /// <summary>
        /// 機密
        /// </summary>
        [Required]
        public bool Security { get; set; } = false;
        
        /// <summary>
        /// 值
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Value { get; set; }
        
        /// <summary>
        /// 活動識別碼
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string EventId { get; set; }

        /// <summary>
        /// 活動
        /// </summary>
        public Event Event { get; set; }

        /// <summary>
        /// 參與者聲明
        /// </summary>
        public ICollection<ParticipantClaim> ParticipantClaims { get; set; }
    }
}