using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories
{
    public interface IStaffRepository
    {
        Task<Staff> GetStaffByIdAsync(string staffId);

        Task<Staff> GetStaffForRound(string roundId, string staffId);

        Task<IEnumerable<Staff>> GetAllStaffsForRoundAsync(string roundId);

        void AddStaffForRound(string roundId, Staff staff);

        void DeleteStaff(Staff staff);

        Task<bool> StaffExistsAsync(string staffId);

        Task<Staff> GetRandomStaffForRound(string roundId);

        Task<bool> SaveAsync();
    }
}