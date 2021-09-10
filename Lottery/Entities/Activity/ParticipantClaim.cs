using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 參與者聲明
    /// </summary>
    public class ParticipantClaim
    {
        /// <summary>
        /// 參與者聲明識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// 參與者識別碼
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string ParticipantId { get; set; }
        
        /// <summary>
        /// 參與者
        /// </summary>
        public Participant Participant { get; set; }

        /// <summary>
        /// 活動聲明識別碼
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string EventClaimId { get; set; }
        
        /// <summary>
        /// 活動聲明
        /// </summary>
        public EventClaim EventClaim { get; set; }
    }
}