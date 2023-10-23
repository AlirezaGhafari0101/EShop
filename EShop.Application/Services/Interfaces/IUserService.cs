using EShop.Application.ViewModels;
using EShop.Application.ViewModels.User;
using EShop.Application.ViewModels.User.UserPanel;
using EShop.Application.ViewModels.Ticket;
using EShop.Application.ViewModels.Wallet;

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
        Task<double> BalanceUserWalletAsyncService(int userId);
        Task<IEnumerable<WalletVM>> GetAllUserWalletsAsyncService(int userId);
        Task<int> ChargeWalletAsyncService(int userId, double amount, string description);
        Task<WalletVM> GetWalletByIdAsyncService(int id);
        Task IsPayWallet(int id);
        #endregion




    }
}
