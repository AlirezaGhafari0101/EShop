using EShop.Application.ViewModels.Account;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> UserRegister(RegisterViewModel registerViewModel);
        Task<User> UserLogin(LoginViewModel loginViewModel);

        Task<bool> IsExistUserEmailService(string email);

        Task<bool> ActiveAccountService(string activeCode);

    }
}
