using InventoryManagementWebAPI.Models;
using InventoryManagementWebAPI.Repositories.Datas;
using InventoryManagementWebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        // POST api/<AccountController>
        [HttpPost]
        public ActionResult Post(LoginViewModel login)
        {
            var user = accountRepository.login(login);
            if (user != null)
                return Ok(new { statusCode = 200, data = user });

            return BadRequest(new { statusCode = 400, data = "null" });
        }
    }
}
