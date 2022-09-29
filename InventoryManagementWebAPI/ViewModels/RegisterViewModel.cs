using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace InventoryManagementWebAPI.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Full Name")]
        public string fullName { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
