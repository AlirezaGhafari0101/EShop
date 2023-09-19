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
            return users.Select(u => new UserViewModel
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
                Avatar = ImageService.CreateImage(userViewModel.Avatar, "UserAvatar"),
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

        public async Task<EditUserViewModel> GetUserByIdForEditAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return new EditUserViewModel()
            {
                Name = user.FirstName,
                Family = user.LastName,
                Email = user.Email,
                AvatarName = user.Avatar,
                IsActive = user.IsActive,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
            };
        }

        public async Task EditUserFromAdminAsync(EditUserViewModel model, int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (model.Avatar != null)
            {
                user.Avatar = ImageService.CreateImage(model.Avatar,"UserAvatar", model.AvatarName);
            }
            user.IsActive = model.IsActive;
            if (model.Password != null)
                user.Password = PasswordHelper.EncodePasswordMd5(model.Password);
            user.IsAdmin = model.IsAdmin;
            user.FirstName = model.Name;
            user.LastName = model.Family;
            await _userRepository.UpdateUserAsync(user);
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
                Avatar = user.Avatar,

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
                AvatarName = user.Avatar,

            };

            return viewModel;
        }

        public async Task EditUserProfileAsync(EditProfileViewModel profileViewModel, int id)
        {
            User user = await _userRepository.GetUserByIdAsync(id);

            user.FirstName = profileViewModel.FirstName;
            user.LastName = profileViewModel.LastName;
            user.Email = profileViewModel.Email;
            user.Avatar = ImageService.CreateImage(profileViewModel.Avatar,"UserAvatar", profileViewModel.AvatarName);

            _userRepository.UpdateUserAsync(user);
            _userRepository.SaveChangeAsync();

        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel, int id)
        {
            string hashedCurrentPassword = PasswordHelper.EncodePasswordMd5(changePasswordViewModel.CurrentPassword);
            string hashedNewPassword = PasswordHelper.EncodePasswordMd5(changePasswordViewModel.Password);
            User user = await _userRepository.GetUserByIdAsync(id);

            if (hashedCurrentPassword == user.Password)
            {

                user.Password = hashedNewPassword;

                await _userRepository.UpdateUserAsync(user);
                await _userRepository.SaveChangeAsync();
                return true;
            }

            return false;



        }


    }
}
