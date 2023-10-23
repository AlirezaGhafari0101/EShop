using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IUserFavouriteRepository
    {
        Task<List<UserFavourite>> GetAllUserFavouriteAsync();
        Task CreateUserFavouriteAsync(UserFavourite userFavourite);
        Task DeleteUserFavouriteAsync(UserFavourite userFavourite);
        Task<UserFavourite> GetUserFavouriteByProductIdAndUserIdAsync(int productId, int userId);
        Task<List<UserFavourite>> GetUserFavouritesProductsAsync(int userId);
        Task SaveChangesAsync();

    }
}
