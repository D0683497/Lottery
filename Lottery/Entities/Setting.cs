using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    /// <summary>
    /// 網站設定
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 設定識別碼
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 設定名稱
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 設定值
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}