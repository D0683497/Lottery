using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 獎品圖片
    /// </summary>
    public class PrizeImage : Document
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
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
    }
}