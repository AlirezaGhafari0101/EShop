using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task SaveChangeAsync();
        //Account Roles
        Task<User> Register(User user);

        Task<User> Login(string email, string password);

        Task<bool> IsExistUserEmail(string email);

        Task<User> GetUserByActiveCode(string activeCode);
        Task<User> GetUserByEmail(string email);

        Task ActiveAccount(User user);

        Task<User> LoginUser(string Email,string Password);

        Task<User> ForgotPassword( string email);

        



        //End Account Roles

        Task<User> GetUserByIdAsync(int UserId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);

    }
}
