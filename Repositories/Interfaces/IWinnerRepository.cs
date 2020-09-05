using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories.Interfaces
{
    public interface IWinnerRepository
    {
        void CreateWinnerForItemIdAttendeeId(string itemId, string attendeeId, Winner winner);
        
        Task<bool> ExistWinnerByItemIAttendeeIddAsync(string itemId, string attendeeId);
        
        Task<bool> SaveAsync();
    }
}