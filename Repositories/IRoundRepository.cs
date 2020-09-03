using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories
{
    public interface IRoundRepository
    {
        Task<int> GetLengthRoundsAsync();

        Task<IEnumerable<Round>> GetRoundsAsync(int skipNumber, int takeNumber);

        Task<IEnumerable<Round>> GetAllRoundAsync();

        Task<Round> GetRoundByIdAsync(string roundId);

        void AddRound(Round round);

        void DeleteRound(Round round);

        Task<bool> RoundExistsAsync(string roundId);

        Task<bool> SaveAsync();
    }
}