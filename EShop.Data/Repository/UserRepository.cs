using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using EShop.Domain.Models.Users.Ticket;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private EshopDBContext _ctx;

        public UserRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<User> LoginAsync(string email, string password)
            => await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);


        public async Task<User> RegisterAsync(User user)
        {
            _ctx.Users.Add(user);
            return user;
        }

        #region CRUD User
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _ctx.Users.FindAsync(userId);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _ctx.Users.Update(user);

        }

        public async Task CreateUserAsync(User user)
        {
            _ctx.Users.Add(user);

        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            user.IsDelete = true;
            await UpdateUserAsync(user);
        }

        public async Task<bool> IsExistUserEmailAsync(string email)
        {
            return await _ctx.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByActiveCodeAsync(string activeCode)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.ActiveCode == activeCode);
        }

        public async Task ActiveAccountAsync(User user)
        {
            _ctx.Update(user);

        }

        public async Task<User> LoginUserAsync(string Email, string Password)
        {

            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
        }

        public async Task<User> ForgotPasswordAsync(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task<User> GetUserInforAsync(int id)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
        }







        #endregion




        #region Wallet

        public async Task<int> BalanceUeserWalletAsync(int userId)
        {
            List<int> diposit = await _ctx.Wallets.Where(w => w.UserId == userId && w.IsPay && w.TypeId == 1).Select(w => w.Amount).ToListAsync();

            List<int> harvest = await _ctx.Wallets.Where(w => w.UserId == userId && w.IsPay && w.TypeId == 2).Select(w => w.Amount).ToListAsync();

            return (diposit.Sum() - harvest.Sum());
        }

        public async Task<IEnumerable<Wallet>> GetAllUserWalletsAsync(int userId)
        {
            return await _ctx.Wallets.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task ChargeWalletAsync(Wallet wallet)
        {
            await _ctx.Wallets.AddAsync(wallet);
        }

        public async Task<Wallet> GetWalletByIdAsync(int id)
        {
            return await _ctx.Wallets.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task UpdateWallet(Wallet wallet)
        {
            _ctx.Wallets.Update(wallet);
        }




        #endregion


      
    }
}
