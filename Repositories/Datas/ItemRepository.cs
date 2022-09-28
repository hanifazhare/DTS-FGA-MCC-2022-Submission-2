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
        private readonly DBContext _context;

        public ItemRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetItem()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItem(int id)
        {
            var itemData = await _context.Items.FindAsync(id);
            return itemData;
        }

        public async Task<Item> GetItem(string code)
        {
            var itemData = await _context.Items.FindAsync(code);
            return itemData;
        }

        public async Task<int> PutItem(ItemViewModel item)
        {
            int result = 0;
            var itemData = await GetItem(item.id);

            if (itemData == null)
            {
                int resultNull = result + 2;
                return resultNull;
            }

            itemData.code = item.code;
            itemData.name = item.name;
            itemData.available_quantity = item.available_quantity;
            itemData.notes = item.notes;

            _context.Items.Update(itemData);
            result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> PostItem(ItemViewModel item)
        {
            int result = 0;
            var itemData = await GetItem(item.code);

            if (itemData == null)
            {
                int resultNull = result + 2;
                return resultNull;
            }

            _context.Items.Add(new Item
            {
                code = item.code,
                name = item.name,
                available_quantity = item.available_quantity,
                notes = item.notes
            });
            result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteItem(int id)
        {
            int result = 0;
            var itemData = await GetItem(id);

            if (itemData == null)
            {
                int resultNull = result + 2;
                return resultNull;
            }

            _context.Items.Remove(itemData);
            result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
