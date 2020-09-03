using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories.Interfaces
{
    public interface IAttendeeRepository
    {
        /// <summary>
        /// 獲取清單中的所有參與者
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<IEnumerable<Attendee>> GetAllAttendeesForItemIdAsync(string itemId);

        /// <summary>
        /// 獲取清單中的參與者
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="skipNumber"></param>
        /// <param name="takeNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<Attendee>> GetAttendeesForItemIdAsync(string itemId, int skipNumber, int takeNumber);

        /// <summary>
        /// 獲取清單中的參與者
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        Task<Attendee> GetAttendeeByIdForItemIdAsync(string itemId, string attendeeId);

        /// <summary>
        /// 建立參與者
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="attendee"></param>
        void CreateAttendeeForItemId(string itemId, Attendee attendee);

        /// <summary>
        /// 獲取清單中的參與者數量
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<int> GetAllAttendeesLengthForItemIdAsync(string itemId);

        /// <summary>
        /// 查看清單中的參與者是否存在
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        Task<bool> ExistAttendeeByIdForItemIdAsync(string itemId, string attendeeId);
        
        Task<bool> SaveAsync();
    }
}