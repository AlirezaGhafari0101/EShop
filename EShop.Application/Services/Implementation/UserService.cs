using EShop.Application.Generator;
using EShop.Application.Security;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
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

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
           var users = await _userRepository.GetAllUsersAsync();
            return  users.Select(u=>new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsActive = u.IsActive,
                IsAdmin = u.IsAdmin,
                CreateDate = u.CreateDate
            }).ToList();
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

        public async Task DeleteUserByIdAsync(int id)
        {
            await _userRepository.DeleteUserByIdAsync(id);
            await _userRepository.SaveChangeAsync();
        }

        public async Task<EditUserViewModel> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return new EditUserViewModel()
            {
                Name=user.FirstName,
                Family=user.LastName,
                Email=user.Email,
                Avatar=user.Avatar,
                Password=user.Password,
                IsActive=user.IsActive,
                IsAdmin=user.IsAdmin,
            } ;
        }
    }
}
