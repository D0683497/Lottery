using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 講池
    /// </summary>
    public class Pool
    {
        /// <summary>
        /// 講池識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// 講池名稱
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 重複中獎
        /// </summary>
        [Required]
        public bool Duplicate { get; set; } = false;

        /// <summary>
        /// 獎品
        /// </summary>
        public ICollection<Prize> Prizes { get; set; }

        /// <summary>
        /// 參與者
        /// </summary>
        public ICollection<Participant> Participants { get; set; }

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