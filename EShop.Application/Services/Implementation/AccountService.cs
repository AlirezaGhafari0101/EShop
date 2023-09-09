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

        public async Task<User> ActiveAccountService(string activeCode)
        {
            var user = await _userRepository.GetUserByActiveCode(activeCode);

            if (user == null || user.IsActive)
            {
                return null;
            }

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUnipNDigitCode(6);

            await _userRepository.ActiveAccount(user);

            return user;

        }

        public async Task ChangeUserPassword(ChangePasswordViewModel viewMode)
        {
            User user = await _userRepository.GetUserByEmail(viewMode.Email);

            string hashedPassword = PasswordHelper.EncodePasswordMd5(viewMode.Password);

            user.Password = hashedPassword;

            await _userRepository.UpdateUserAsync(user);

        }

        public async Task<User> CheckForgotPassword(string code)
        {
            User user = await _userRepository.GetUserByActiveCode(code);

            user.ActiveCode = NameGenerator.GenerateUnipNDigitCode(6);
            await  _userRepository.UpdateUserAsync(user);

            return user;
        }

        public async Task<User> ForgotPasswordService(string email)
        {
            string Fixedemail = FixedText.FixEmail(email);
            User user = await _userRepository.ForgotPassword(Fixedemail);

            return user;
        }

        public async Task<bool> IsExistUserEmailService(string email)
        {
            return await _userRepository.IsExistUserEmail(email);
        }

        public async Task<User> LoginUserService(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);
            string email = FixedText.FixEmail(loginViewModel.Email);

            var user = await _userRepository.LoginUser(email, password);
            return user;
        }

        public async Task<User> UserLogin(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);

            return await _userRepository.Login(loginViewModel.Email, password);
        }

        public async Task<User> UserRegister(RegisterViewModel registerViewModel)
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
                RegisterDate = DateTime.Now,
                Avatar = "Defult.jpg"




            };

            User registredUser = await _userRepository.Register(userModel);
            return registredUser;
        }
    }
}
