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
        Task<User> UserRegisterAsync(RegisterViewModel registerViewModel);
        Task<User> UserLoginAsync(LoginViewModel loginViewModel);

        Task<bool> IsExistUserEmailServiceAsync(string email);

        Task<User> ActiveAccountServiceAsync(string activeCode);

        Task<User> LoginUserServiceAsync(LoginViewModel loginViewModel);

        Task<User> ForgotPasswordServiceAsync(string email);
        Task<User> CheckForgotPasswordAsync(string code);

        Task ChangeUserPasswordAsync(ChangePasswordViewModel viewMode);

    }
}
