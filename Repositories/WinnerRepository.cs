using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Repositories.Interfaces;

namespace Lottery.Repositories
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WinnerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}