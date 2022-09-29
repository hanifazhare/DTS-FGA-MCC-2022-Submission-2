using InventoryManagementWebAPI.Contexts;
using InventoryManagementWebAPI.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManagementWebAPI.Repositories.Datas
{
    public class AccountRepository
    {
        private readonly DBContext _context;

        public AccountRepository(DBContext context)
        {
            _context = context;
        }

        public ResponseLoginViewModel login(LoginViewModel login)
        {
            var data = _context.UserRole
                .Include(i => i.Role)
                .Include(i => i.User)
                .Include(i => i.User.Employee)
                .FirstOrDefault(f =>
                    f.User.Employee.email.Equals(login.email) &&
                    f.User.password.Equals(login.password));
            
            if (data != null)
            {
                ResponseLoginViewModel responseLogin = new ResponseLoginViewModel()
                {
                    id = data.User.id,
                    fullName = data.User.Employee.fullName,
                    email = data.User.Employee.email,
                    role = data.Role.name
                };

                return responseLogin;
            }

            return null;
        }
    }
}
