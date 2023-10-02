using EShop.Application.ViewModels.Product;
using EShop.Application.ViewModels.Product.Category;
using EShop.Application.ViewModels.Product.Color;
using EShop.Application.ViewModels.Product.ProductGallery;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        #region Category
        Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesClientSideServiceAsync();
        Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesServiceAsync(int? parentId);
        Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesForCreatingProductServiceAsync();
        Task<ProductCategroyViewModel> GetCategoryServiceAsync(int id);
        Task<UpdateCategoryViewModel> GetCategoryForUpdateCategory(int id);
        Task AddCategoryServiceAsync(CreateCategoryViewModel category,int? categoryId);
        Task UpdateCategoryServiceAsync(UpdateCategoryViewModel category, int id);
        Task DeleteCategoryServiceAsync(int id);
        #endregion







        #region Products

        Task<IEnumerable<ProductViewModel>> GetAllProductsServiceAsync();

        Task<ProductViewModel> GetProductByIdServiceAsync(int id);
        Task<List<ProductViewModel>> GetAllProductsByCategoryIdServiceAsync(int categoryId);
        Task<int> CreateProductServiceAsync(AddProductViewModel model);

        Task<bool> DeleteProductServiceAsync(int id);

        Task<bool> UpdateProductServiceAsync(EditProductViewModel model, int id);

        Task<bool> IsProductExistServiceAsync(string title);

        #endregion


        #region ProductColor
        Task<IEnumerable<ProductColorViewModel>> GetAllProductColorsServiceAsync(int productId);
        Task<UpdateProductColorViewModel> GetProductColorServiceAsync(int colorId);
        Task AddProductColorServiceAsync(AddProductColorViewModel color);
        Task UpdateProductColorServiceAsync(UpdateProductColorViewModel color);
        Task<UpdateProductColorViewModel> GetColorForUpdateServiceAsync(int colorId);
        Task DeleteProductColorServiceAsync(int colorId);
        
        #endregion












        #region product gallery
        Task CreateProductGalleryServiceAsync(ProductGalleryViewModel gallery);

        Task<List<EditProductGalleryViewModel>> GetProductGalleryByIdServiceAsync(int productId);

        Task DeleteAllProductGalleryServiceAsync(int galleryId);

        Task DeleteSingleProductGalleryServiceAsync(int galleryId);


        #endregion
    }
}
