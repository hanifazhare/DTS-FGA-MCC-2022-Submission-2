using InventoryManagementWebAPI.Models;
using InventoryManagementWebAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagementWebAPI.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItem();

        Task<Item> GetItem(int id);
        
        Task<Item> GetItem(string code);
        
        Task<int> PutItem(ItemViewModel item);
        
        Task<int> PostItem(ItemViewModel item);
        
        Task<int> DeleteItem(int id);

    }
}
