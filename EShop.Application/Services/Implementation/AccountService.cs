using EShop.Application.Convertors;
using EShop.Application.Generator;
using EShop.Application.Security;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Account;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
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

        public async Task<bool> ActiveAccountService(string activeCode)
        {
            var user = await _userRepository.GetUserByActiveCode(activeCode);

            if (user == null || user.IsActive)
            {
                return false;
            }

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();

            await _userRepository.ActiveAccount(user);

            return true;

        }

        public async Task<bool> IsExistUserEmailService(string email)
        {
            return await _userRepository.IsExistUserEmail(email);
        }

        public async Task<User> UserLogin(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);

            return await _userRepository.Login(loginViewModel.Email, password);
        }

        public async Task<User> UserRegister(RegisterViewModel registerViewModel)
        {
            var hashedPassword = PasswordHelper.EncodePasswordMd5(registerViewModel.Password);
            User userModel = new User()

            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = FixedText.FixEmail(registerViewModel.Email),
                Password = hashedPassword,
                ActiveCode = NameGenerator.GenerateUniqCode(),
                RegisterDate = DateTime.Now,




            };

            User registredUser = await _userRepository.Register(userModel);
            return registredUser;
        }
    }
}
