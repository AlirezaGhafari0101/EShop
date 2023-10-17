﻿using EShop.Application.ViewModels;
using EShop.Application.ViewModels.User;
using EShop.Application.ViewModels.User.UserPanel;
using EShop.Application.ViewModels.Ticket;
using EShop.Application.ViewModels.UserFavourite;
using EShop.Domain.Models.Users;

namespace EShop.Application.Services.Interfaces
{
    public interface IUserService
    {
        // Start Admin
        Task<bool> IsExistUserEmailService(string email);

        Task CreateUserAsync(AddUserViewModel userViewModel);

        Task DeleteUserByIdAsync(int id);
        Task<List<UserViewModel>> GetAllUsersAsync();

        Task<EditUserViewModel> GetUserByIdForEditAsync(int id);

        Task EditUserFromAdminAsync(EditUserViewModel userViewModel, int id);

        // End Admin

        //Start User Panel
        Task<UserViewModel> GetUserInforServiceAsync(int id);
        Task<UserViewModel> GetSideBarUserPanelDataAsync(int id);
        Task<EditProfileViewModel> GetDataForEditProfileUserAsync(int id);
        Task EditUserProfileAsync(EditProfileViewModel profileViewModel, int id);
        Task<bool> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel, int id);

        //End User Panel






        #region Wallet
        Task<int> BalanceUserWalletAsyncService(int userId);
        Task<IEnumerable<WalletVM>> GetAllUserWalletsAsyncService(int userId);
        Task<int> ChargeWalletAsyncService(int userId, int amount, string description);
        Task<WalletVM> GetWalletByIdAsyncService(int id);
        Task IsPayWallet(int id);
        #endregion


        #region UserFavourite

        Task<List<AddUserFavouriteViewModel>> GetAllUserFavouriteServiceAsync();
        //Task<UserFavouriteViewModel> GetUserFavouriteByIdServiceAsync(int id);
        Task CreateUserFavouriteServiceAsync(AddUserFavouriteViewModel uf);
        Task DeleteUserFavouriteServiceAsync(int productId, int userId);
        Task<List<UserFavouriteViewModel>> GetUserFavouritesProductsServiceAsync(int userId);

        #endregion


    }
}
