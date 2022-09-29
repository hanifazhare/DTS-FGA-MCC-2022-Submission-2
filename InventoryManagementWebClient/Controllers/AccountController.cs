using InventoryManagementWebAPI.ViewModels;
using InventoryManagementWebClient.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementWebClient.Controllers
{
    public class AccountController : Controller
    {
        HttpClient httpClient;
        string address;

        public AccountController()
        {
            this.address = "https://localhost:44376/api/account/";
            
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address, content).Result;
            
            if (result.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseClientViewModel>(await result.Content.ReadAsStringAsync());
                HttpContext.Session.SetString("role", data.data.role);

                return RedirectToAction("Index", "AdminPanel");
            }
            
            return View();
        }
    }
}
