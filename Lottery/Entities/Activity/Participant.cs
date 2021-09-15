using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 參與者
    /// </summary>
    public class Participant
    {
        /// <summary>
        /// 參與者識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// 講池識別碼
        /// </summary>
        [Required]
        [MaxLength(36)]
        public string PoolId { get; set; }

        /// <summary>
        /// 講池
        /// </summary>
        public Pool Pool { get; set; }

        /// <summary>
        /// 獲得獎品
        /// </summary>
        public ICollection<ParticipantPrize> ParticipantPrizes { get; set; }

        /// <summary>
        /// 參與者聲明
        /// </summary>
        public ICollection<ParticipantClaim> Claims { get; set; }
    }
}