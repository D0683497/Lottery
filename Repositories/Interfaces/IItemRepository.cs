using System.Threading.Tasks;

namespace Lottery.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<bool> SaveAsync();
    }
}