using EShop.Application.ViewModels.Product;
using EShop.Application.ViewModels.Product.Category;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        #region Category
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

        Task<bool> CreateProductServiceAsync(AddProductViewModel model);

        Task<bool> DeleteProductServiceAsync(int id);

        Task<bool> UpdateProductServiceAsync(EditProductViewModel model, int id);

        Task<bool> IsProductExistServiceAsync(string title);

        #endregion



    }
}
