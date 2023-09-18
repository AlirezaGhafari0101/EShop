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
        Task AddCategory(CreateCategoryViewModel category,int? categoryId);
        Task UpdateCategory(UpdateCategoryViewModel category, int id);
        Task DeleteCategory(int id);
        #endregion







        #region Products

        Task<IEnumerable<ProductViewModel>> GetAllProductsServiceAsync();

        Task<ProductViewModel> GetProductByIdServiceAsync(int id);

        Task<bool> CreateProductServiceAsync(AddProductViewModel model);

        Task<bool> DeleteProductServiceAsync(int id);

        Task<bool> UpdateProductServiceAsync(ProductViewModel model, int id);

        Task<bool> IsProductExistServiceAsync(string title);

        #endregion



    }
}
