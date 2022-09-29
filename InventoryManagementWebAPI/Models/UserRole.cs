using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementWebAPI.Models
{
    public class UserRole
    {
        [Key]
        public int id { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }

        public Role Role { get; set; }

        [ForeignKey("Role")]
        public int roleId { get; set; }
    }
}
