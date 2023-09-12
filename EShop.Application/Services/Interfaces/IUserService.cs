using EShop.Application.ViewModels;
using EShop.Application.ViewModels.User;
using EShop.Application.ViewModels.User.UserPanel;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface IUserService
    {
     
       Task<bool> IsExistUserEmailService(string email);

        Task CreateUserAsync(AddUserViewModel userViewModel);

        Task<List<UserViewModel>> GetAllUsersAsync();



        //Start User Panel
        Task<UserViewModel> GetUserInforServiceAsync(int id);
        Task<UserViewModel> GetSideBarUserPanelDataAsync(int id);
        Task<EditProfileViewModel> GetDataForEditProfileUserAsync(int id);
        Task EditUserProfileAsync(EditProfileViewModel profileViewModel,int id);
        Task<bool> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel,int id);

        //End User Panel
      
    }
}
