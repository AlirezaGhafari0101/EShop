﻿using EShop.Application.Generator;
using EShop.Application.Security;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.User;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;

namespace EShop.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsExistUserEmailService(string email)
        {
            return await _userRepository.IsExistUserEmailAsync(email);
        }

        public async Task CreateUserAsync(AddUserViewModel userViewModel)
        {
            User user = new User()
            {
                FirstName = userViewModel.Name,
                LastName = userViewModel.Family,
                Email = userViewModel.Email,
                ActiveCode = NameGenerator.GenerateUniqCode(),
                CreateDate = DateTime.Now,
                Avatar = "Default.jpg",
                IsActive = true,
                Password = PasswordHelper.EncodePasswordMd5(userViewModel.Password),
            };
              await _userRepository.CreateUserAsync(user);
              await _userRepository.SaveChangeAsync();
        }
    }
}
