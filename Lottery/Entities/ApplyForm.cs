using System.ComponentModel.DataAnnotations;
using Lottery.Entities.Activity;
using Lottery.Entities.Identity;
using Lottery.Enums;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Lottery.Entities
{
    /// <summary>
    /// 申請表
    /// </summary>
    public class ApplyForm
    {
        /// <summary>
        /// 申請表識別碼
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = new SequentialGuidValueGenerator().Next(null!).ToString();

        /// <summary>
        /// 申請人姓名
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
        
        /// <summary>
        /// 申請人姓名
        /// </summary>
        [MaxLength(256)]
        public string Email { get; set; }

        /// <summary>
        /// 申請狀態
        /// </summary>
        public ProcessType Process { get; set; } = ProcessType.Pending;

        /// <summary>
        /// 申請用途
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Purpose { get; set; }

        /// <summary>
        /// 退回原因
        /// </summary>
        [MaxLength(500)]
        public string Reason { get; set; }
        
        /// <summary>
        /// 使用者識別碼
        /// </summary>
        [MaxLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 使用者
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// 活動
        /// </summary>
        public Event Event { get; set; }
    }
}