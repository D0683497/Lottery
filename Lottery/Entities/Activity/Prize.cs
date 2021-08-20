using System;
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
        /// 獎品數量
        /// </summary>
        [Required]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// 獎品圖片
        /// </summary>
        public PrizeImage Image { get; set; }
        
        /// <summary>
        /// 得獎者識別碼
        /// </summary>
        [MaxLength(36)]
        public string ParticipantId { get; set; }
        
        /// <summary>
        /// 得獎者
        /// </summary>
        public Participant Participant { get; set; }

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