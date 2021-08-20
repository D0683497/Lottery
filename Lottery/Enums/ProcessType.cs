namespace Lottery.Enums
{
    /// <summary>
    /// 進度狀態
    /// </summary>
    public enum ProcessType
    {
        /// <summary>
        /// 待處理
        /// </summary>
        Pending = 0,
        /// <summary>
        /// 處理中
        /// </summary>
        Processing = 1,
        /// <summary>
        /// 通過
        /// </summary>
        Pass = 2,
        /// <summary>
        /// 不通過
        /// </summary>
        Fail = 3
    }
}