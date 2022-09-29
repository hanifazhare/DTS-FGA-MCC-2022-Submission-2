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
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
