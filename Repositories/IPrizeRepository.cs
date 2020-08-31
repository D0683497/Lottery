using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories
{
    public interface IPrizeRepository
    {
        Task<IEnumerable<Prize>> GetPrizesByIdAsync(string roundId);

        Task<Prize> GetPrizeByIdAsync(string prizeId);

        void AddPrize(string roundId, Prize prize);

        void DeletePrize(string roundId, Prize prize);

        Task<bool> SaveAsync();
    }
}