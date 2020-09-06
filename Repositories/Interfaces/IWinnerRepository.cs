using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories.Interfaces
{
    public interface IWinnerRepository
    {
        /// <summary>
        /// 獲取清單中的得獎者
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="skipNumber"></param>
        /// <param name="takeNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<Attendee>> GetWinnersForItemIdAsync(string itemId, int skipNumber, int takeNumber);

        /// <summary>
        /// 建立獲獎者
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="attendeeId"></param>
        /// <param name="winner"></param>
        void CreateWinnerForItemIdAttendeeId(string itemId, string attendeeId, Winner winner);

        /// <summary>
        /// 獲取清單中的獲獎者數量
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<int> GetAllWinnersLengthForItemIdAsync(string itemId);

        Task<bool> SaveAsync();
    }
}