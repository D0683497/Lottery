using System.ComponentModel.DataAnnotations;
using Lottery.Entities.Activity;
using Lottery.Entities.Identity;

namespace Lottery.Entities
{
    /// <summary>
    /// 活動使用者
    /// </summary>
    public class EventUser
    {
        /// <summary>
        /// 管理活動
        /// </summary>
        [Required]
        public bool Manage { get; set; } = false;

        /// <summary>
        /// 管理參與者
        /// </summary>
        [Required]
        public bool Participant { get; set; } = false;

        /// <summary>
        /// 管理抽獎
        /// </summary>
        [Required]
        public bool Lottery { get; set; } = false;
        
        /// <summary>
        /// 活動識別碼
        /// </summary>
        [MaxLength(36)]
        public string EventId { get; set; }

        /// <summary>
        /// 活動
        /// </summary>
        public Event Event { get; set; }
        
        /// <summary>
        /// 使用者識別碼
        /// </summary>
        [MaxLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}