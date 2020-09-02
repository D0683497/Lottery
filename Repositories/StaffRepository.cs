using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottery.Data;
using Lottery.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lottery.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StaffRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Staff> GetStaffByIdAsync(string staffId)
        {
            return await _applicationDbContext.Staffs
                .FirstOrDefaultAsync(x => x.StaffId == staffId);
        }

        public async Task<Staff> GetStaffForRound(string roundId, string staffId)
        {
            return await _applicationDbContext.Staffs
                .Where(x => x.RoundId == roundId && x.StaffId == staffId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Staff>> GetAllStaffsForRoundAsync(string roundId)
        {
            return await _applicationDbContext.Staffs
                .Where(x => x.RoundId == roundId)
                .ToListAsync();
        }

        public void AddStaffForRound(string roundId, Staff staff)
        {
            if (roundId == null)
            {
                throw new ArgumentException(nameof(roundId));
            }
            if (staff == null)
            {
                throw new ArgumentException(nameof(staff));
            }

            staff.RoundId = roundId;
            _applicationDbContext.Staffs.Add(staff);
        }

        public void DeleteStaff(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentException(nameof(staff));
            }

            _applicationDbContext.Staffs.Remove(staff);
        }

        public async Task<bool> StaffExistsAsync(string staffId)
        {
            if (staffId == null)
            {
                throw new ArgumentException(nameof(staffId));
            }

            return await _applicationDbContext.Staffs
                .AnyAsync(x => x.StaffId == staffId);
        }

        public Staff GetRandomStaffForRound(string roundId)
        {
            if (roundId == null)
            {
                throw new ArgumentException(nameof(roundId));
            }

            return _applicationDbContext.Staffs
                .Where(x => x.RoundId == roundId)
                .AsEnumerable()
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefault();
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}