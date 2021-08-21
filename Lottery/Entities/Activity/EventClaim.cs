using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 活動聲明
    /// </summary>
    [Index(nameof(Value), Name = "EventClaimValueIndex", IsUnique = true)]
    public class EventClaim
    {
        /// <summary>
        /// 活動聲明識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// 是否可搜尋
        /// </summary>
        [Required]
        public bool Key { get; set; } = false;

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