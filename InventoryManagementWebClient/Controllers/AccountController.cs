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
        private readonly HttpClient httpClient;
        private readonly string address;

        public AccountController()
        {
            address = "https://localhost:44376/api/account/";
            
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModel), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address + "login", content).Result;
            
            if (result.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseClientViewModel>(await result.Content.ReadAsStringAsync());
                HttpContext.Session.SetString("role", data.data.role);

                return RedirectToAction("Index", "AdminPanel");
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerViewModel), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address + "register", content).Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(changePasswordViewModel), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address + "change-password", content).Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(RegisterViewModel registerViewModel)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerViewModel), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address + "forgot-password", content).Result;

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}
