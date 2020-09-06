using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Lottery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Repositories
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public WinnerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Attendee>> GetAllWinnersForItemIdAsync(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .Where(x => x.AttendeeIsAwarded == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendee>> GetWinnersForItemIdAsync(string itemId, int skipNumber, int takeNumber)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .Where(x => x.AttendeeIsAwarded == true)
                .Skip(skipNumber)
                .Take(takeNumber)
                .ToListAsync();
        }

        public void CreateWinnerForItemIdAttendeeId(string itemId, string attendeeId, Winner winner)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }
            if (attendeeId == null)
            {
                throw new ArgumentNullException(nameof(attendeeId));
            }
            
            winner.ItemId = itemId;
            winner.AttendeeId = attendeeId;

            _applicationDbContext.Winners.Add(winner);
        }

        public async Task<int> GetAllWinnersLengthForItemIdAsync(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _applicationDbContext.Winners
                .Where(x => x.ItemId == itemId)
                .CountAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}