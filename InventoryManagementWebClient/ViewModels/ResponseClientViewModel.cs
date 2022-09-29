using InventoryManagementWebAPI.ViewModels;

namespace InventoryManagementWebClient.ViewModels
{
    public class ResponseClientViewModel
    {
        public string message { get; set; }

        public int statusCode { get; set; }
        
        public ResponseLoginViewModel data { get; set; }
    }
}
