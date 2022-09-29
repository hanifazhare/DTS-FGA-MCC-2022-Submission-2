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
        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // POST api/<AccountController>
        [HttpPost("login")]
        public ActionResult LoginService(LoginViewModel loginViewModel)
        {
            var data = _accountRepository.login(loginViewModel);
            if (data != null)
                return Ok(new { statusCode = 200, data = data });

            return BadRequest(new { statusCode = 400, data = "null" });
        }

        [HttpPost("register")]
        public ActionResult RegisterService(RegisterViewModel registerViewModel)
        {
            var data = _accountRepository.register(registerViewModel);
            if (data == 1)
                return Ok(new { statusCode = 200, message = "Success register user" });

            return BadRequest(new { statusCode = 400, message = "Failed register user" });
        }

        [HttpPost("change-password")]
        public ActionResult ChangePasswordService(ChangePasswordViewModel changePasswordViewModel)
        {
            var data = _accountRepository.changePassword(changePasswordViewModel);
            if (data == 1)
                return Ok(new { statusCode = 200, message = "Success change user password" });

            return BadRequest(new { statusCode = 400, message = "Failed change user password" });
        }

        [HttpPost("forgot-password")]
        public ActionResult ForgotPasswordService(RegisterViewModel registerViewModel)
        {
            var data = _accountRepository.forgotPassword(registerViewModel);
            if (data == 1)
                return Ok(new { statusCode = 200, message = "Success change user password" });

            return BadRequest(new { statusCode = 400, message = "Failed change user password" });
        }
    }
}
