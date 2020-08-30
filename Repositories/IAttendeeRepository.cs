using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories
{
    public interface IAttendeeRepository
    {
        Task<Attendee> GetPrizeByIdAsync(string attendeeId);

        void AddPrize(string roundId, Attendee attendee);

        void DeletePrize(string roundId, Attendee attendee);

        Task<bool> SaveAsync();
    }
}