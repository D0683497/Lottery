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
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AttendeeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Attendee>> GetAllAttendeesForItemIdAsync(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Attendee>> GetAttendeesForItemIdAsync(string itemId, int skipNumber, int takeNumber)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .Skip(skipNumber)
                .Take(takeNumber)
                .ToListAsync();
        }

        public async Task<Attendee> GetAttendeeByIdForItemIdAsync(string itemId, string attendeeId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }
            if (attendeeId == null)
            {
                throw new ArgumentNullException(nameof(attendeeId));
            }

            return await _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .FirstOrDefaultAsync(x => x.AttendeeId == attendeeId);
        }

        public void CreateAttendeeForItemId(string itemId, Attendee attendee)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            attendee.ItemId = itemId;

            _applicationDbContext.Attendees.Add(attendee);
        }

        public async Task<int> GetAllAttendeesLengthForItemIdAsync(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .CountAsync();
        }

        public Attendee GetAttendeeRandomForItemId(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .AsEnumerable()
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefault();
        }

        public async Task<bool> ExistAttendeeByIdForItemIdAsync(string itemId, string attendeeId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }
            if (attendeeId == null)
            {
                throw new ArgumentNullException(nameof(attendeeId));
            }

            return await _applicationDbContext.Attendees
                .Where(x => x.ItemId == itemId)
                .AnyAsync(x => x.AttendeeId == attendeeId);
        }
        
        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}