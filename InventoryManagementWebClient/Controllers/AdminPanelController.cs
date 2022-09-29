using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementWebClient.Controllers
{
    public class AdminPanelController : Controller
    {
        public ActionResult Index()
        {
            var role = HttpContext.Session.GetString("role");

            if (role == "Admin")
            {
                return View();
            }

            return RedirectToAction("Unauthorized", "ErrorPage");
        }
    }
}
