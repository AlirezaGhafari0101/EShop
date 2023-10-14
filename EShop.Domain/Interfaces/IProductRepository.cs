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
        Task<IEnumerable<Category>> GetAllCategoriesClientSideAsync();
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
        
        //Task<List<Category>> GetAllProductsByCategoryIdAsync(int categoryId);
        Task<List<int>> GetAllChildCategoryIDsByCategoryId(int categoryId);
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> IsProductExistAsync(string title);

        Task<Product> GetProductImageAndTitleByIdAsync(int id);
        #endregion









        #region product gallery
        Task CreateProductGalleryAsync(ProductGallery productGallery);
        Task<List<ProductGallery>> GetProductGallerisByParentIdAsync(int productId);

        Task<ProductGallery> GetProductGalleryByIdAsync(int galleryId);
        Task UpdateProductGalleryAsync(ProductGallery productGallery);
       
        Task DeleteProductGalleryAsync(ProductGallery gallery);
        #endregion 


        Task<IEnumerable<ProductColor>> GetAllProductColorAsync(int productId);
        Task<ProductColor> GetProductColorAsync(int id);
        Task AddProductColorAsync(ProductColor color);
        Task UpdateProductColorAsync(ProductColor color);
        Task DeleteProductColorAsync(ProductColor color);
        int GetFirstColorByProductIdAsync(int productId);




    }
}       
