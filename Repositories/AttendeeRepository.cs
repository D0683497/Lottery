using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AttendeeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Attendee> GetPrizeByIdAsync(string attendeeId)
        {
            return await _applicationDbContext.Attendees.FirstOrDefaultAsync(x => x.AttendeeId == attendeeId);
        }

        public void AddPrize(string roundId, Attendee attendee)
        {
            attendee.RoundId = roundId;
            _applicationDbContext.Attendees.Add(attendee);
        }

        public void DeletePrize(string roundId, Attendee attendee)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}