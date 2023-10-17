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
    public class UserFavouriteRepository : IUserFavouriteRepository
    {
        private readonly EshopDBContext _ctx;

        public UserFavouriteRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<UserFavourite>> GetAllUserFavouriteAsync()
        {
            return await _ctx.UserFavourites.ToListAsync();
        }
        public async Task<UserFavourite> GetUserFavouriteByProductIdAndUserIdAsync(int productId, int userId)
        {
            return await _ctx.UserFavourites.FirstOrDefaultAsync(uf => uf.ProductId == productId && uf.UserId ==userId);
        }
        public async Task CreateUserFavouriteAsync(UserFavourite userFavourite)
        {
          await _ctx.UserFavourites.AddAsync(userFavourite);
        }

        public async Task DeleteUserFavouriteAsync(UserFavourite userFavourite)
        {
            _ctx.UserFavourites.Remove(userFavourite);
        }
        public async Task<List<UserFavourite>> GetUserFavouritesProductsAsync(int userId)
        {
            return await _ctx.UserFavourites
                .Where(uf => uf.UserId == userId)
                .Include(uf => uf.Product)
                .ThenInclude(p => p.Colors)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

       
    }
}
