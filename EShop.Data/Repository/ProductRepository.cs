using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EshopDBContext _ctx;
        public ProductRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }
        #region CagteGory
        public async Task AddCategoryAsync(Category category)
        {
            _ctx.Add(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _ctx.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(int? parentId)
        {
            return await _ctx.Categories.Where(c=> c.ParentId==parentId).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesForCreatingProductAsync()
        {
            return await _ctx.Categories.Where(c => c.ParentId != null).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _ctx.Categories.FirstOrDefaultAsync(cg => cg.Id == id);
        }

        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _ctx.Update(category);
        }

        #endregion



        #region Product

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _ctx.Products.Include(P=> P.Category).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _ctx.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var user = await GetProductByIdAsync(id);
            _ctx.Products.Remove(user);
            return true;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _ctx.Products.Update(product);
            return true;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            await _ctx.Products.AddAsync(product);
            return true;
        }

        public async Task<bool> IsProductExistAsync(string title)
        {
            return await _ctx.Products.AnyAsync(p => p.Title == title);
        }
        #endregion

        #region ProductColor

        public async Task<IEnumerable<ProductColor>> GetAllProductColorsAsync(int productId)
        {
            return await _ctx.ProductColors.Where(pc=> pc.ProductId== productId).ToListAsync();
        }

        public async Task<ProductColor> GetProductColorAsync(int colorId)
        {
            return await _ctx.ProductColors.FirstOrDefaultAsync(pc => pc.Id == colorId);
        }

        public async Task AddProductColorAsync(ProductColor color)
        {
            await _ctx.AddAsync(color);
        }

        public async Task UpdateProductColorAsync(ProductColor color)
        {
             _ctx.Update(color);
        }

        public async Task DeleteProductColorAsync(int colorId)
        {
            var color = await GetProductColorAsync(colorId);
            _ctx.Remove(color);
        }
        #endregion
    }
}
