using EShop.Application.Interfaces;
using EShop.Application.Security;
using EShop.Application.ViewModels.Users;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> UserLogin(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);

            return await _userRepository.Login(loginViewModel.Email, password);
        }

        public void UserRegister(RegisterViewModel registerViewModel)
        {
            var hashedPassword = PasswordHelper.EncodePasswordMd5(registerViewModel.Password);
            var userModel = new User()

            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                Password = hashedPassword,

            };

            _userRepository.Register(userModel);
        }
    }
}
