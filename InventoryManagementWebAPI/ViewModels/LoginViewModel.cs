using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace InventoryManagementWebAPI.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
