using System.Collections.Generic;
using System.Threading.Tasks;
using Lottery.Entities;

namespace Lottery.Repositories.Interfaces
{
    public interface IItemRepository
    {
        /// <summary>
        /// 獲取所有清單
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetAllItemsAsync();

        /// <summary>
        /// 獲取清單
        /// </summary>
        /// <param name="skipNumber"></param>
        /// <param name="takeNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<Item>> GetItemsAsync(int skipNumber, int takeNumber);

        /// <summary>
        /// 獲取清單
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<Item> GetItemByIdAsync(string itemId);

        /// <summary>
        /// 建立清單
        /// </summary>
        /// <param name="item"></param>
        void CreateItem(Item item);

        /// <summary>
        /// 查看清單是否存在
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<bool> ExistItemByIdAsync(string itemId);
        
        Task<bool> SaveAsync();
    }
}