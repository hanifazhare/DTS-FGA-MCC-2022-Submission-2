using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementWebClient.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}
