using EShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IProductRepository
    {







        #region Category
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category>GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);

        #endregion

        Task SaveChangeAsync();
    }
}
