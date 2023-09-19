﻿using EShop.Application.Services.Interfaces;
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
        public async Task AddCategoryServiceAsync(CreateCategoryViewModel category, int? categoryId)
        {
            Category newCategory = new Category()
            {
                CategoryTitle = category.CategoryTitle,
                ParentId = categoryId

            };

            await _productRepository.AddCategoryAsync(newCategory);
            await _productRepository.SaveChangeAsync();


        }

        public async Task DeleteCategoryServiceAsync(int id)
        {
   
            Category group = await _productRepository.GetCategoryByIdAsync(id);

            IEnumerable<Category> subGroups = await _productRepository.GetAllCategoriesAsync(group.Id);
            if (subGroups.Any()) {

                foreach (Category subGroup in subGroups)
                {

                    IEnumerable<Category> subGroupsIn= await _productRepository.GetAllCategoriesAsync(subGroup.Id);

                    if(subGroupsIn.Any())
                    {
                        foreach (Category category in subGroupsIn)
                        {
                            category.IsDelete = true;
                            await _productRepository.UpdateCategoryAsync(group);
                            await _productRepository.SaveChangeAsync();
                        }
                    }


                    subGroup.IsDelete = true;
                    await _productRepository.UpdateCategoryAsync(group);
                    await _productRepository.SaveChangeAsync();

                    

                    


                }
            }
            group.IsDelete = true;
            await _productRepository.UpdateCategoryAsync(group);
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

        public async Task UpdateCategoryServiceAsync(UpdateCategoryViewModel category, int id)
        {
            Category findCategory = await _productRepository.GetCategoryByIdAsync(id);

            findCategory.CategoryTitle = category.CategoryTitle;

            await _productRepository.UpdateCategoryAsync(findCategory);
            await _productRepository.SaveChangeAsync();

        }

        public async Task<UpdateCategoryViewModel> GetCategoryForUpdateCategory(int id)
        {
            Category category = await _productRepository.GetCategoryByIdAsync(id);
            return new UpdateCategoryViewModel()
            {
                CategoryTitle = category.CategoryTitle,

            };
        }
        #endregion

        #region Product

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsServiceAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Count = p.Count,
                Tag = p.Tag,
                CategoryId = p.CategoryId,
                Image = p.Image,
                CreatedDate = p.CreateDate
            }).ToList();
        }
        public async Task<ProductViewModel> GetProductByIdServiceAsync(int id)
        {

            var product = await _productRepository.GetProductByIdAsync(id);
            return new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Tag = product.Tag,
                Image = product.Image,
                CategoryId = product.CategoryId,
                Count = product.Count,
                CreatedDate = product.CreateDate
            };

        }
        public async Task<bool> CreateProductServiceAsync(ProductViewModel model)
        {
            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                Count = model.Count,
                Tag = model.Tag,
                CategoryId = model.CategoryId,
                Image = model.Image,
                IsDelete = false,
                CreateDate = DateTime.Now,
            };

            await _productRepository.CreateProductAsync(product);
            await _productRepository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateProductServiceAsync(ProductViewModel model, int id)
        {
            var selectedProduct = await _productRepository.GetProductByIdAsync(id);



            await _productRepository.UpdateProductAsync(selectedProduct);
            await _productRepository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteProductServiceAsync(int id)
        {
            var product = await _productRepository.DeleteProductAsync(id);
            await _productRepository.SaveChangeAsync();
            return true;
        }

        #endregion
    }
}
