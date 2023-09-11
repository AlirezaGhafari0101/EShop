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
        Task<User> RegisterAsync(User user);

        Task<User> LoginAsync(string email, string password);

        Task<bool> IsExistUserEmailAsync(string email);

        Task<User> GetUserByActiveCodeAsync(string activeCode);
        Task<User> GetUserByEmailAsync(string email);

        Task ActiveAccountAsync(User user);

        Task<User> LoginUserAsync(string Email,string Password);

        Task<User> ForgotPasswordAsync( string email);

        



        //End Account Roles

        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();  
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserByIdAsync(int userId);

    }
}
