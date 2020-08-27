using System;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class Prize
    {
        public Prize()
        {
            PrizeId = Guid.NewGuid().ToString();
            PrizeNumber = 1;
            PrizeComplete = false;
            PrizeOrder = 1;
        }
        
        [Key]
        public string PrizeId { get; set; }
        
        /// <summary>
        /// 獎品名稱
        /// </summary>
        public string PrizeName { get; set; }

        /// <summary>
        /// 獎品圖片
        /// </summary>
        public string PrizeImage { get; set; }

        /// <summary>
        /// 獎品數量
        /// </summary>
        public int PrizeNumber { get; set; }

        /// <summary>
        /// 獎品是否抽獎完成
        /// </summary>
        public bool PrizeComplete { get; set; }

        /// <summary>
        /// 獎品排序
        /// </summary>
        public int PrizeOrder { get; set; }

        public string RoundId { get; set; }
        public Round Round { get; set; }

        public string WinnerId { get; set; }
        public Winner Winner { get; set; }
    }
}