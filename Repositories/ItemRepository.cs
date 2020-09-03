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
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _applicationDbContext.Items.ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(int skipNumber, int takeNumber)
        {
            return await _applicationDbContext.Items
                .Skip(skipNumber)
                .Take(takeNumber)
                .ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(string itemId)
        {
            if (itemId == null)
            {
                throw new ArgumentNullException(nameof(itemId));
            }

            return await _applicationDbContext.Items
                .FirstOrDefaultAsync(x => x.ItemId == itemId);
        }

        public void CreateItem(Item item)
        {
            _applicationDbContext.Add(item);
        }
        
        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() >= 0;
        }
    }
}