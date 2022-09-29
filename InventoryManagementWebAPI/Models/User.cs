using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementWebAPI.Models
{
    public class User
    {
        public Employee Employee { get; set; }
        
        [Key]
        [ForeignKey("Employee")]
        public int id { get; set; }

        public string password { get; set; }
    }
}
