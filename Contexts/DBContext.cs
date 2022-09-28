using Microsoft.EntityFrameworkCore;
using InventoryManagementWebAPI.Models;

namespace InventoryManagementWebAPI.Contexts
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Item> Items { get; set; }
    }
}
