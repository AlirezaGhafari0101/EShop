using EShop.Application.Convertors;
using EShop.Application.Generator;
using EShop.Application.Security;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
using EShop.Application.ViewModels.User;
using EShop.Application.ViewModels.User.UserPanel;
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

        public async Task<UserViewModel> GetUserInforServiceAsync(int id)
        {
            User user = await _userRepository.GetUserInforAsync(id);

            UserViewModel viewModel = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreateDate = user.CreateDate
            };

            return viewModel;
        }

        public async Task<UserViewModel> GetSideBarUserPanelDataAsync(int id)
        {
            User user = await _userRepository.GetUserInforAsync(id);

            UserViewModel viewModel = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Avatar=user.Avatar,
              
            };

            return viewModel;
        }

        public async Task<EditProfileViewModel> GetDataForEditProfileUserAsync(int id)
        {
            User user = await _userRepository.GetUserInforAsync(id);

            EditProfileViewModel viewModel = new EditProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AvatarName=user.Avatar,

            };

            return viewModel;
        }

        public async Task EditUserProfileAsync(EditProfileViewModel profileViewModel, int id)
        {
            User user=await _userRepository.GetUserByIdAsync(id);

            user.FirstName = profileViewModel.FirstName;
            user.LastName = profileViewModel.LastName;
            user.Email = profileViewModel.Email;
            user.Avatar=ImageService.CreateImage(profileViewModel.Avatar,profileViewModel.AvatarName);

            _userRepository.UpdateUserAsync(user);
            _userRepository.SaveChangeAsync();

        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel, int id)
        {
            string hashedPassword = PasswordHelper.EncodePasswordMd5(changePasswordViewModel.CurrentPassword);

            User user = await _userRepository.GetUserByIdAsync(id);

            if(hashedPassword == user.Password)
            {
                string hashedNewPassword=PasswordHelper.EncodePasswordMd5(changePasswordViewModel.Password);
                user.Password = hashedNewPassword;

                await _userRepository.UpdateUserAsync(user);
                return true;
            }

            return false;


        }
    }
}
