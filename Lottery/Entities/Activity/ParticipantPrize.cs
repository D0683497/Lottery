using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 獲得獎品
    /// </summary>
    public class ParticipantPrize
    {
        /// <summary>
        /// 獲得獎品識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string ParticipantPrizeId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 獎品識別碼
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string PrizeId { get; set; }
        
        /// <summary>
        /// 獎品
        /// </summary>
        public Prize Prize { get; set; }

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
    }
}