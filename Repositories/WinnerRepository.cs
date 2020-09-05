using System;
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

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}