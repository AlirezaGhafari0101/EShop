using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        {

            return await _ctx.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async void Register(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChangesAsync();
        }

        #region CRUD User
        public async Task<User> GetUserById(int userId)
        {
            return await _ctx.Users.FindAsync(userId);
        }

        public async void UpdateUser(User user)
        {
            _ctx.Users.Update(user);
            _ctx.SaveChangesAsync();
        }

        public async void CreateUser(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChangesAsync();
        }

        public async void DeleteUser(int userId)
        {
            User user = await GetUserById(userId);
            _ctx.Users.Remove(user);
            _ctx.SaveChangesAsync();
        }
        #endregion
    }
}
