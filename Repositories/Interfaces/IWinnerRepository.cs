using System.Threading.Tasks;

namespace Lottery.Repositories.Interfaces
{
    public interface IWinnerRepository
    {
        Task<bool> SaveAsync();
    }
}