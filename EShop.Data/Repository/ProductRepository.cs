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

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _ctx.Categories.ToListAsync();
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
    }
}
