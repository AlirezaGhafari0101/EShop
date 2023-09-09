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
        //Account Roles
        void Register(User user);

        Task<User> Login(string email, string password);

        Task<bool> IsExistUserEmail(string email);

        Task<User> GetUserByActiveCode(string activeCode);

        Task ActiveAccount(User user);

        //End Account Roles

        Task<User> GetUserByIdAsync(int UserId);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);

    }
}
