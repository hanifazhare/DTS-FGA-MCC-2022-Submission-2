using InventoryManagementWebAPI.Contexts;
using InventoryManagementWebAPI.Models;
using InventoryManagementWebAPI.Repositories.Interfaces;
using InventoryManagementWebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementWebAPI.Repositories.Datas
{
    public class ItemRepository : IItemRepository
    {
        private readonly DBContext _dbContext;

        public ItemRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Item>> GetItem()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            var itemData = await _dbContext.Items.FindAsync(id);
            return itemData;
        }

        public async Task<int> PutItem(ItemViewModel item)
        {
            int result = 0;
            var itemData = await GetItem();
            var getItemById = itemData.Find(i => i.id == item.id);
            var getItemByCode = itemData.Find(i => i.code == item.code);

            if (getItemById == null)
                return result + 3;

            if (getItemByCode != null)
                return result + 2;

            getItemById.code = item.code;
            getItemById.name = item.name;
            getItemById.available_quantity = item.available_quantity;
            getItemById.notes = item.notes;

            _dbContext.Items.Update(getItemById);
            result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> PostItem(ItemViewModel item)
        {
            int result = 0;
            var itemData = await GetItem();
            var getItemByCode = itemData.Find(i => i.code == item.code);

            if (getItemByCode != null)
                return result + 2;

            _dbContext.Items.Add(new Item
            {
                code = item.code,
                name = item.name,
                available_quantity = item.available_quantity,
                notes = item.notes
            });
            result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteItem(int id)
        {
            int result = 0;
            var itemData = await GetItem(id);

            if (itemData == null)
                return result + 2;

            _dbContext.Items.Remove(itemData);
            result = await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
