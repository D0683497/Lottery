using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Repositories.Interfaces;

namespace Lottery.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AttendeeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}