using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Repositories
{
    public class RoundRepository : IRoundRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoundRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> GetLengthRoundsAsync()
        {
            return await _applicationDbContext.Rounds.CountAsync();
        }

        public async Task<IEnumerable<Round>> GetRoundsAsync(int skipNumber, int takeNumber)
        {
            return await _applicationDbContext.Rounds
                .Skip(skipNumber)
                .Take(takeNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Round>> GetAllRoundAsync()
        {
            return await _applicationDbContext.Rounds.ToListAsync();
        }

        public async Task<Round> GetRoundByIdAsync(string roundId)
        {
            return await _applicationDbContext.Rounds.FirstOrDefaultAsync(x => x.RoundId == roundId);
        }

        public void AddRound(Round round)
        {
            if (round == null)
            {
                throw new ArgumentException(nameof(round));
            }

            _applicationDbContext.Rounds.Add(round);
        }

        public void DeleteRound(Round round)
        {
            if (round == null)
            {
                throw new ArgumentException(nameof(round));
            }

            _applicationDbContext.Rounds.Remove(round);
        }

        public async Task<bool> RoundExistsAsync(string roundId)
        {
            if (roundId == null)
            {
                throw new ArgumentException(nameof(roundId));
            }

            return await _applicationDbContext.Rounds.AnyAsync(x => x.RoundId == roundId);
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}