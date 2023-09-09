using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
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
            => await _ctx.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);


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

        public async Task UpdateUserAsync(User user)
        {
            _ctx.Users.Update(user);
            
        }

        public async Task CreateUserAsync(User user)
        {
            _ctx.Users.Add(user);
        
        }

        public async Task DeleteUserAsync(int userId)
        {
            User user = await GetUserByIdAsync(userId);
            _ctx.Users.Remove(user);
            _ctx.SaveChangesAsync();

        }

        public async Task<bool> IsExistUserEmailAsync(string email)
        {
            return await _ctx.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByActiveCodeAsync(string activeCode)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.ActiveCode == activeCode);
        }

        public async Task ActiveAccountAsync(User user)
        {
            _ctx.Update(user);

        }

        public async Task<User> LoginUserAsync(string Email, string Password)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Email == Email && u.Password == Password);
        }

        public async Task<User> ForgotPasswordAsync(string email)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }

      


        #endregion
    }
}
