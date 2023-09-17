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

        #region Product
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<Product> GetProductByIdAsync(int id);
        public Task<bool> CreateProductAsync(Product product);
        public Task<bool> DeleteProductAsync(int id);
        public Task<bool> UpdateProductAsync(Product product);
        #endregion
    }
}
