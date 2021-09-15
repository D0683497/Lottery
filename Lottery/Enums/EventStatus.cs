using System.ComponentModel.DataAnnotations;

namespace Lottery.Enums
{
    /// <summary>
    /// 活動狀態
    /// </summary>
    public enum EventStatus
    {
        /// <summary>
        /// 進行中、隱藏
        /// </summary>
        [Display(Name = "進行中、隱藏")]
        HideIng = 0,
        /// <summary>
        /// 結束、隱藏
        /// </summary>
        [Display(Name = "結束、隱藏")]
        HideEnd = 1,
        /// <summary>
        /// 進行中、顯示
        /// </summary>
        [Display(Name = "進行中、顯示")]
        OpenIng = 2,
        /// <summary>
        /// 結束、顯示
        /// </summary>
        [Display(Name = "結束、顯示")]
        OpenEnd = 3
    }
}