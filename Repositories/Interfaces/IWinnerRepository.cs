using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories.Interfaces
{
    public interface IWinnerRepository
    {
        /// <summary>
        /// 建立獲獎者
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="attendeeId"></param>
        /// <param name="winner"></param>
        void CreateWinnerForItemIdAttendeeId(string itemId, string attendeeId, Winner winner);

        Task<bool> SaveAsync();
    }
}