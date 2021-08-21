using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 講池圖片
    /// </summary>
    public class PoolImage : Document
    {
        /// <summary>
        /// 講池圖片識別碼
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
    }
}