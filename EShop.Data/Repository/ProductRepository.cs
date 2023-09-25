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
            return await _ctx.Categories.Where(c => c.ParentId == parentId).ToListAsync();
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
            return await _ctx.Products.Include(p => p.Category).Include(p => p.Discount).ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _ctx.Products.Include(p => p.productGalleries).FirstOrDefaultAsync(p => p.Id == id);
        }



        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            product.IsDelete = true;
            product.productGalleries.ForEach(pg =>
            {
                pg.IsDelete = true;
            });
            await UpdateProductAsync(product);
            return true;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _ctx.Products.Update(product);
            return true;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _ctx.Products.AddAsync(product);

        }

        public async Task<bool> IsProductExistAsync(string title)
        {
            return await _ctx.Products.AnyAsync(p => p.Title == title);
        }
        #endregion































        #region productGallery
        public async Task CreateProductGalleryAsync(ProductGallery gallery)
        {
            await _ctx.ProductGalleries.AddAsync(gallery);
        }

        public async Task<List<ProductGallery>> GetProductGallerisByParentIdAsync(int productId)
        {
            return await _ctx.ProductGalleries.Where(p => p.ProductId == productId).ToListAsync();
        }

        public async Task UpdateProductGalleryAsync(ProductGallery productGallery)
        {
            _ctx.ProductGalleries.Update(productGallery);
        }

        public async Task<ProductGallery> GetProductGalleryByIdAsync(int galleryId)
        {
            return await _ctx.ProductGalleries.FindAsync(galleryId);
        }

        public async Task DeleteProductGalleryAsync(ProductGallery gallery)
        {
             _ctx.Remove(gallery);
        }

        #endregion
    }
}
