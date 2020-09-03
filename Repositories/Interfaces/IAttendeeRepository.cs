using System.Threading.Tasks;

namespace Lottery.Repositories.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<bool> SaveAsync();
    }
}