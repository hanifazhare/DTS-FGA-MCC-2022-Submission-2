using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace InventoryManagementWebClient.Models
{
    public class Item
    {
        [Key]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Code")]
        public string code { get; set; }
        
        [Display(Name = "Name")]
        public string name { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Available Quantity")]
        public decimal available_quantity { get; set; }
        
        [Display(Name = "Note")]
        public string notes { get; set; }
    }
}
