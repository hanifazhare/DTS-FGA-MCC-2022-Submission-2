using InventoryManagementWebClient.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementWebClient.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Item> Items { get; set; }
    }
}
