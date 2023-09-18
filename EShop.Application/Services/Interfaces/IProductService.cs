using EShop.Application.ViewModels.Product;
using EShop.Application.ViewModels.Product.Category;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        #region Category
        Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesServiceAsync(int? parentId);
        Task<ProductCategroyViewModel> GetCategoryServiceAsync(int id);
        Task AddCategory(CreateCategoryViewModel category,int? categoryId);
        Task UpdateCategory(UpdateCategoryViewModel category, int id);
        Task DeleteCategory(int id);
        #endregion







        #region Products

        Task<IEnumerable<ProductViewModel>> GetAllProductsServiceAsync();

        #endregion



    }
}
