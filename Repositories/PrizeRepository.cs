using System.Linq;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Repositories
{
    public class PrizeRepository : IPrizeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PrizeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Prize> GetPrizeByIdAsync(string prizeId)
        {
            return await _applicationDbContext.Prizes.FirstOrDefaultAsync(x => x.PrizeId == prizeId);
        }

        public void AddPrize(string roundId, Prize prize)
        {
            prize.RoundId = roundId;
            _applicationDbContext.Prizes.Add(prize);
        }

        public void DeletePrize(string roundId, Prize prize)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}