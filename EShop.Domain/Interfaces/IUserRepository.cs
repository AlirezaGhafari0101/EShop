using EShop.Domain.Models.Users;
using EShop.Domain.Models.Ticket;
using EShop.Domain.Models.Wallet;

namespace EShop.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task SaveChangeAsync();
        //Account Roles
        Task<User> RegisterAsync(User user);

        Task<User> LoginAsync(string email, string password);

        Task<bool> IsExistUserEmailAsync(string email);

        Task<User> GetUserByActiveCodeAsync(string activeCode);
        Task<User> GetUserByEmailAsync(string email);

        Task ActiveAccountAsync(User user);

        Task<User> LoginUserAsync(string Email,string Password);

        Task<User> ForgotPasswordAsync( string email);





        //End Account Roles

        //User Panel Role
        Task<User> GetUserInforAsync(int id);
        
        //End User Panel Role

        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersAsync();  
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserByIdAsync(int userId);














        #region Wallet
        Task<double> BalanceUeserWalletAsync(int userId);
        Task<IEnumerable<Wallet>> GetAllUserWalletsAsync(int userId);
        Task ChargeWalletAsync(Wallet wallet);
        Task<Wallet> GetWalletByIdAsync(int id);
        Task UpdateWallet(Wallet wallet);
        #endregion





    }
}
