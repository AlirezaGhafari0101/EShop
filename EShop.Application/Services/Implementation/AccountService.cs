using EShop.Application.Convertors;
using EShop.Application.Generator;
using EShop.Application.Security;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Account;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> ActiveAccountServiceAsync(string activeCode)
        {
            var user = await _userRepository.GetUserByActiveCodeAsync(activeCode);

            if (user == null || user.IsActive)
            {
                return null;
            }

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUnipNDigitCode(6);

            _userRepository.UpdateUserAsync(user);
           await _userRepository.SaveChangeAsync();

            return user;

        }

        public async Task ChangeUserPasswordAsync(ChangePasswordViewModel viewMode)
        {
            User user = await _userRepository.GetUserByEmailAsync(viewMode.Email);

            string hashedPassword = PasswordHelper.EncodePasswordMd5(viewMode.Password);

            user.Password = hashedPassword;

            _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangeAsync();

        }

        public async Task<User> CheckForgotPasswordAsync(string code)
        {
            User user = await _userRepository.GetUserByActiveCodeAsync(code);

            user.ActiveCode = NameGenerator.GenerateUnipNDigitCode(6);
            _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangeAsync();

            return user;
        }

        public async Task<User> ForgotPasswordServiceAsync(string email)
        {
            string Fixedemail = FixedText.FixEmail(email);
            User user = await _userRepository.ForgotPasswordAsync(Fixedemail);

            return user;
        }

        public async Task<bool> IsExistUserEmailServiceAsync(string email)
        {
            return await _userRepository.IsExistUserEmailAsync(email);
        }

        public async Task<User> LoginUserServiceAsync(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);
            string email = FixedText.FixEmail(loginViewModel.Email);

            var user = await _userRepository.LoginUserAsync(email, password);
            return user;
        }

        public async Task<User> UserLoginAsync(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);

            return await _userRepository.LoginAsync(loginViewModel.Email, password);
        }

        public async Task<User> UserRegisterAsync(RegisterViewModel registerViewModel)
        {
            var hashedPassword = PasswordHelper.EncodePasswordMd5(registerViewModel.Password);
            var hashedActiveCode = PasswordHelper.EncodePasswordMd5(NameGenerator.GenerateUnipNDigitCode(6));
            User userModel = new User()

            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = FixedText.FixEmail(registerViewModel.Email),
                Password = hashedPassword,
                ActiveCode = NameGenerator.GenerateUnipNDigitCode(6),
                CreateDate = DateTime.Now,
                Avatar = "Defult.jpg"




            };

           
         

            User registredUser = await _userRepository.RegisterAsync(userModel);
            await _userRepository.SaveChangeAsync();

            return registredUser;
        }
    }
}
