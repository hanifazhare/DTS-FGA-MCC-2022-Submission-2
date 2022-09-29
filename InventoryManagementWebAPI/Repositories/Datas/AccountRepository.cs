using InventoryManagementWebAPI.Contexts;
using InventoryManagementWebAPI.Models;
using InventoryManagementWebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManagementWebAPI.Repositories.Datas
{
    public class AccountRepository
    {
        private readonly DBContext _dbContext;

        public AccountRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        private static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }

        public ResponseLoginViewModel login(LoginViewModel login)
        {
            bool isPasswordCorrect = false;
            var data = _dbContext.UserRole
                .Include(model => model.Role)
                .Include(model => model.User)
                .Include(model => model.User.Employee)
                .FirstOrDefault(model => model.User.Employee.email.Equals(login.email));
            
            if (data != null)
            {
                isPasswordCorrect = ValidatePassword(login.password, data.User.password);

                if (isPasswordCorrect)
                {
                    return new ResponseLoginViewModel()
                    {
                        id = data.User.id,
                        fullName = data.User.Employee.fullName,
                        email = data.User.Employee.email,
                        role = data.Role.name
                    };
                }

                return null;
            }

            return null;
        }

        public int register(RegisterViewModel registerViewModel)
        {
            int result = 0;

            var employee = new Employee
            {
                fullName = registerViewModel.fullName,
                email = registerViewModel.email
            };
            _dbContext.Employee.Add(employee);
            int isNewEmployeeSaved = _dbContext.SaveChanges();

            if (isNewEmployeeSaved == 1)
            {
                var employeeData = _dbContext.Employee.FirstOrDefault(model =>
                    model.fullName.Equals(employee.fullName) &&
                    model.email.Equals(employee.email));

                var user = new User
                {
                    id = employeeData.id,
                    password = HashPassword(registerViewModel.password)
                };
                _dbContext.User.Add(user);
                int isNewUserSaved = _dbContext.SaveChanges();

                if (isNewUserSaved == 1)
                {
                    var userData = _dbContext.User.FirstOrDefault(model => model.id.Equals(user.id));
                    
                    var userRole = new UserRole
                    {
                        userId = userData.id,
                        roleId = 1
                    };
                    _dbContext.UserRole.Add(userRole);
                    int isNewUserRoleSaved = _dbContext.SaveChanges();

                    if (isNewUserRoleSaved == 1)
                        return result + 1;

                    return result;
                }

                return result;
            }

            return result;
        }

        public int changePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            bool isPasswordCorrect = false;
            int result = 0;

            var data = _dbContext.UserRole
                .Include(data => data.User)
                .Include(data => data.User.Employee)
                .FirstOrDefault(model => model.User.Employee.email.Equals(changePasswordViewModel.email));

            if (data != null)
            {
                isPasswordCorrect = ValidatePassword(changePasswordViewModel.oldPassword, data.User.password);

                if (isPasswordCorrect)
                {
                    var newData = _dbContext.User.Find(data.User.id);
                    newData.password = HashPassword(changePasswordViewModel.newPassword);
                    _dbContext.User.Update(newData);
                    result = _dbContext.SaveChanges();

                    return result;
                }

                return result;
            }

            return result;
        }

        public int forgotPassword(RegisterViewModel registerViewModel)
        {
            int result = 0;

            var data = _dbContext.UserRole
                .Include(data => data.User.Employee)
                .Include(data => data.User)
                .FirstOrDefault(model =>
                    model.User.Employee.fullName.Equals(registerViewModel.fullName) &&
                    model.User.Employee.email.Equals(registerViewModel.email));

            if (data != null)
            {
                var newData = _dbContext.User.Find(data.User.id);
                newData.password = HashPassword(registerViewModel.password);
                _dbContext.User.Update(newData);
                result = _dbContext.SaveChanges();

                return result;
            }

            return result;
        }
    }
}
