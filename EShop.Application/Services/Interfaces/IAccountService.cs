using EShop.Application.ViewModels;
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
        Task<UserViewModel> UserRegisterAsync(RegisterViewModel registerViewModel);
        Task<User> UserLoginAsync(LoginViewModel loginViewModel);

        Task<bool> IsExistUserEmailServiceAsync(string email);

        Task<UserViewModel> ActiveAccountServiceAsync(string activeCode);

        Task<UserViewModel> LoginUserServiceAsync(LoginViewModel loginViewModel);

        Task<UserViewModel> ForgotPasswordServiceAsync(string email);
        Task<UserViewModel> CheckForgotPasswordAsync(string code);

        Task ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);

    }
}
