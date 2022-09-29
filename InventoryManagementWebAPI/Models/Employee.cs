using System.ComponentModel.DataAnnotations;

namespace InventoryManagementWebAPI.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
    }
}
