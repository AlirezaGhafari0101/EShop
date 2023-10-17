using EShop.Application.Convertors;
using EShop.Application.Generator;
using EShop.Application.Security;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
using EShop.Application.ViewModels.User;
using EShop.Application.ViewModels.User.UserPanel;
using EShop.Application.ViewModels.Ticket;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using EShop.Domain.Models.Ticket;
using EShop.Application.ViewModels.UserFavourite;

namespace EShop.Application.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUserFavouriteRepository _userFavouriteRepository;

        public UserService(IUserRepository userRepository, IUserFavouriteRepository userFavouriteRepository)
        {
            _userRepository = userRepository;
            _userFavouriteRepository = userFavouriteRepository;
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
                user.Avatar = ImageService.CreateImage(model.Avatar, "UserAvatar", model.AvatarName);
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
            user.Avatar = ImageService.CreateImage(profileViewModel.Avatar, "UserAvatar", profileViewModel.AvatarName);

            await _userRepository.UpdateUserAsync(user);
            await _userRepository.SaveChangeAsync();

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

        #region Wallet
        public async Task<int> BalanceUserWalletAsyncService(int userId)
        {
            return await _userRepository.BalanceUeserWalletAsync(userId);
        }

        public async Task<IEnumerable<WalletVM>> GetAllUserWalletsAsyncService(int userId)
        {
            IEnumerable<Wallet> wallets = await _userRepository.GetAllUserWalletsAsync(userId);

            return wallets.Select(w => new WalletVM()
            {
                Amount = w.Amount,
                CreateDate = w.CreateDate,
                TypeId = w.TypeId,
                Description = w.Description,
                IsPay = w.IsPay,
            }).ToList();
        }

        public async Task<int> ChargeWalletAsyncService(int userId, int amount, string description)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                TypeId = 1,
                Description = description,
                IsPay = false,
                UserId = userId,

            };

            await _userRepository.ChargeWalletAsync(wallet);
            await _userRepository.SaveChangeAsync();

            return wallet.Id;
        }

        public async Task<WalletVM> GetWalletByIdAsyncService(int id)
        {
            var wallet = await _userRepository.GetWalletByIdAsync(id);

            return new WalletVM()
            {
                Amount = wallet.Amount,
                TypeId = wallet.TypeId,
                Description = wallet.Description,
                CreateDate = wallet.CreateDate,
                IsPay = wallet.IsPay,
                TypeID = wallet.TypeId,


            };
        }

        public async Task IsPayWallet(int id)
        {
            Wallet wallet = await _userRepository.GetWalletByIdAsync(id);

            wallet.IsPay = true;

            await _userRepository.UpdateWallet(wallet);
            await _userRepository.SaveChangeAsync();
        }

        #endregion

        #region UserFavourite

        public async Task<List<AddUserFavouriteViewModel>> GetAllUserFavouriteServiceAsync()
        {
            var userFavourites = await _userFavouriteRepository.GetAllUserFavouriteAsync();
            return userFavourites.Select(uf => new AddUserFavouriteViewModel
            {
                ProductId = uf.ProductId,
                UserId = uf.UserId,
            }).ToList();
        }
        //public async Task<UserFavouriteViewModel> GetUserFavouriteByIdServiceAsync(int id)
        //{
        //    var uf = await _userFavouriteRepository.GetUserFavouriteByIdAsync(id);
        //    return new UserFavouriteViewModel
        //    {
        //        Id = uf.Id,
        //        ProductId = uf.ProductId,
        //        UserId = uf.UserId,
        //    };
        //}
        public async Task CreateUserFavouriteServiceAsync(AddUserFavouriteViewModel uf)
        {
            var userFavourite = new UserFavourite
            {
                ProductId = uf.ProductId,
                UserId = uf.UserId,
                CreateDate = DateTime.Now,
                IsDelete = false,
            };
            await _userFavouriteRepository.CreateUserFavouriteAsync(userFavourite);
            await _userFavouriteRepository.SaveChangesAsync();
        }
        public async Task DeleteUserFavouriteServiceAsync(int productId, int userId)
        {
            var uf = await _userFavouriteRepository.GetUserFavouriteByProductIdAndUserIdAsync(productId, userId);
            await _userFavouriteRepository.DeleteUserFavouriteAsync(uf);
            await _userFavouriteRepository.SaveChangesAsync();
        }

       public async Task<List<UserFavouriteViewModel>> GetUserFavouritesProductsServiceAsync(int userId)
        {
            var favourites = await _userFavouriteRepository.GetUserFavouritesProductsAsync(userId);
            return favourites.Select(uf => new UserFavouriteViewModel
            {
                Id = uf.Id,
                ProductId= uf.ProductId,
                ProductImageName=uf.Product.Image,
                ProductTitle=uf.Product.Title,
                ProductPrice=uf.Product.Colors.First().Price
            }).ToList();
        }

        #endregion

    }
}
