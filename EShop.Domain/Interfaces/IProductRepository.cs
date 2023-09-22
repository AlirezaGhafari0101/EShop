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
        Task<IEnumerable<Category>> GetAllCategoriesAsync(int? parentId);
        Task<IEnumerable<Category>> GetAllCategoriesForCreatingProductAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        #endregion
        Task SaveChangeAsync();

        #region Product
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> IsProductExistAsync(string title);
        #endregion









        #region product gallery
        Task CreateProductGalleryAsync(ProductGallery productGallery);
        Task<List<ProductGallery>> GetProductGalleryByIdAsync(int productId);
        Task DeleteProductGalleryAsync(int galleryId);

        #endregion 
    }
}       
