using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();

        Task<IEnumerable<Item>> GetItemsAsync(int skipNumber, int takeNumber);

        Task<Item> GetItemByIdAsync(string itemId);

        void CreateItem(Item item);
        
        Task<bool> SaveAsync();
    }
}