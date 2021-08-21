using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Lottery.Entities.Activity
{
    /// <summary>
    /// 活動
    /// </summary>
    [Index(nameof(Url), Name = "UrlIndex", IsUnique = true)]
    public class Event
    {
        /// <summary>
        /// 活動識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = new SequentialGuidValueGenerator().Next(null!).ToString();

        /// <summary>
        /// 活動結束
        /// </summary>
        public bool End { get; set; } = false;

        /// <summary>
        /// 活動標題
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 活動副標題
        /// </summary>
        [MaxLength(300)]
        public string SubTitle { get; set; }

        /// <summary>
        /// 活動網址
        /// </summary>
        [MaxLength(100)]
        public string Url { get; set; }

        /// <summary>
        /// 活動聲明
        /// </summary>
        public ICollection<EventClaim> Claims { get; set; }

        /// <summary>
        /// 活動圖片
        /// </summary>
        public EventImage Image { get; set; }

        /// <summary>
        /// 獎池
        /// </summary>
        public ICollection<Pool> Pools { get; set; }

        /// <summary>
        /// 申請表
        /// </summary>
        public ApplyForm ApplyForm { get; set; }

        /// <summary>
        /// 活動使用者
        /// </summary>
        public ICollection<EventUser> Users { get; set; }
    }
}