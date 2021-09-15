using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 獎品
    /// </summary>
    public class Prize
    {
        /// <summary>
        /// 獎品識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 獎品名稱
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 獎品剩餘數量
        /// </summary>
        [Required]
        public int Last { get; set; } = 1;

        /// <summary>
        /// 獎品數量
        /// </summary>
        [Required]
        public int Total { get; set; } = 1;

        /// <summary>
        /// 獎品圖片
        /// </summary>
        public PrizeImage Image { get; set; }
        
        /// <summary>
        /// 獲得獎品
        /// </summary>
        public ICollection<ParticipantPrize> ParticipantPrizes { get; set; }

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
    }
}