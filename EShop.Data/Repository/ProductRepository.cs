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
            return await _ctx.Products.ToListAsync();
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

        public async Task<bool> IsProductExistAsync(int id)
        {
            return await _ctx.Products.AnyAsync(p => p.Id == id);
        }
        #endregion
    }
}
