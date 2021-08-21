using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 活動圖片
    /// </summary>
    public class EventImage : Document
    {
        /// <summary>
        /// 活動圖片識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
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
    }
}