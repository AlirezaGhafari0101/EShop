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
        public async Task<User> Login(string email, string password)
            => await _ctx.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);


        public async Task<User> Register(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<bool> IsExistUserEmail(string email)
        {
            return await _ctx.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByActiveCode(string activeCode)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.ActiveCode == activeCode);
        }

        public async Task ActiveAccount(User user)
        {
            _ctx.Update(user);

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
            await _ctx.SaveChangesAsync();

        }

        public async Task DeleteUserAsync(int userId)
        {
            User user = await GetUserByIdAsync(userId);
            _ctx.Users.Remove(user);

        }

        public async Task<User> LoginUser(string Email, string Password)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Email == Email && u.Password == Password);
        }

        public async Task<User> ForgotPassword(string email)
        {
            return await _ctx.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmail(string email)
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
