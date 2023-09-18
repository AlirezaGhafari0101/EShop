using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product;
using EShop.Application.ViewModels.Product.Category;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Products;

namespace EShop.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #region Category
        public async Task AddCategory(CreateCategoryViewModel category, int? categoryId)
        {
            Category newCategory = new Category()
            {
                CategoryTitle = category.CategoryTitle,
                ParentId = categoryId

            };

            await _productRepository.AddCategoryAsync(newCategory);
            await _productRepository.SaveChangeAsync();


        }

        public async Task DeleteCategory(int id)
        {
            Category category = await _productRepository.GetCategoryByIdAsync(id);

            await _productRepository.DeleteCategoryAsync(category);
            await _productRepository.SaveChangeAsync();
        }

        public async Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesServiceAsync(int? parentId)
        {
            IEnumerable<Category> categories = await _productRepository.GetAllCategoriesAsync(parentId);

            return categories.Select(cg => new ProductCategroyViewModel()
            {
                CategoryTitle = cg.CategoryTitle,
                Id = cg.Id,
                CreateDate = cg.CreateDate,

            }).ToList();


        }

        public Task<IEnumerable<ProductViewModel>> GetAllProductsServiceAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductCategroyViewModel> GetCategoryServiceAsync(int id)
        {
            Category category = await _productRepository.GetCategoryByIdAsync(id);
            return new ProductCategroyViewModel()
            {
                CategoryTitle = category.CategoryTitle,
                Id = category.Id,
                CreateDate = category.CreateDate,
            };
        }

        public async Task UpdateCategory(UpdateCategoryViewModel category, int id)
        {
            Category findCategory = await _productRepository.GetCategoryByIdAsync(id);

            findCategory.CategoryTitle = category.CategoryTitle;

            await _productRepository.UpdateCategoryAsync(findCategory);
            await _productRepository.SaveChangeAsync();

        }
        #endregion
    }
}
