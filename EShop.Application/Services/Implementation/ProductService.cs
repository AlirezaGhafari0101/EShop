﻿using EShop.Application.Convertors;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product;
using EShop.Application.ViewModels.Product.Category;
using EShop.Application.ViewModels.Product.Color;
using EShop.Application.ViewModels.Product.ProductGallery;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Products;
using EShop.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Policy;

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
            if (subGroups.Any())
            {

                foreach (Category subGroup in subGroups)
                {

                    IEnumerable<Category> subGroupsIn = await _productRepository.GetAllCategoriesAsync(subGroup.Id);

                    if (subGroupsIn.Any())
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
        public async Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesForCreatingProductServiceAsync()
        {
            var categories = await _productRepository.GetAllCategoriesForCreatingProductAsync();
            return categories.Select(c => new ProductCategroyViewModel
            {
                Id = c.Id,
                CategoryTitle = c.CategoryTitle,
            }).ToList();
        }

        public async Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesClientSideServiceAsync()
        {
            var categories = await _productRepository.GetAllCategoriesClientSideAsync();
            return categories.Select(c => new ProductCategroyViewModel
            {
                CategoryTitle= c.CategoryTitle,
                Id = c.Id,
                ParentId = c.ParentId,
                CreateDate = c.CreateDate,
            });
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
                ImageName = p.Image,
                CreatedDate = p.CreateDate,
                CategoryTitle = p.Category.CategoryTitle
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
                ImageName = product.Image,
                CategoryId = product.CategoryId,
                DiscountId = product.DiscountId,
                Count = product.Count,
                CreatedDate = product.CreateDate,
                Colors = product.Colors.Select(c => new ProductColorViewModel
                {
                    Hex = c.Hex,
                    Price = c.Price,
                    ProductId = c.ProductId
                }).ToList(),
                Gallery = product.productGalleries.Select(gallery => new ProductGalleryViewModel
                {
                    ProductId = gallery.ProductId,
                    ProductImageUrl = gallery.ProductImage,
                }).ToList(),
               Features=product.Features,

            };

        }
        public async Task<IEnumerable<ProductViewModel>> GetAllProductsByCategoryIdServiceAsync(int categoryId)
        {
            var products = await _productRepository.GetAllProductsByCategoryIdAsync(categoryId);
            var price = await _productRepository.GetProductColorAsync(categoryId);
          
            return products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Count = p.Count,
                Tag = p.Tag,
                CategoryId = p.CategoryId,
                ImageName = p.Image,
                CreatedDate = p.CreateDate,
                Price =   _productRepository.GetFirstColorByProductIdAsync(p.Id),
                Colors=p.Colors.Select(c => new ProductColorViewModel 
                {
                    Hex = c.Hex,
                    Price = c.Price,
                    ProductId=c.ProductId
                }).ToList()
                
            }).ToList();
        }
        public async Task<int> CreateProductServiceAsync(AddProductViewModel model)
        {
            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                Count = model.Count,
                Tag = model.Tag,
                CategoryId = model.CategoryId,
                DiscountId = model.DiscountId,
                Image = ImageService.CreateImage(model.Image, "ProductImages"),
                IsDelete = false,
                CreateDate = DateTime.Now,
                Features = model.Features,
            };



            await _productRepository.CreateProductAsync(product);

            await _productRepository.SaveChangeAsync();
            return product.Id;
        }
        public async Task<bool> UpdateProductServiceAsync(EditProductViewModel model, int id)
        {
            var selectedProduct = await _productRepository.GetProductByIdAsync(id);

            if (model.Image != null)
            {
                selectedProduct.Image = ImageService.CreateImage(model.Image, "ProductImages", model.ImageName);
            }
            if (model.DiscountId != null)
            {
                selectedProduct.DiscountId = model.DiscountId;
            }
            else
            {
                selectedProduct.DiscountId = null;
            }
          
            selectedProduct.Features= model.Features;
            selectedProduct.Title = model.Title;
            selectedProduct.Description = model.Description;
            selectedProduct.Count = model.Count;
            selectedProduct.CategoryId = model.CategoryId;
            selectedProduct.Tag = model.Tag;

            await _productRepository.UpdateProductAsync(selectedProduct);
            await _productRepository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteProductServiceAsync(int id)
        {

            var product = await _productRepository.GetProductByIdAsync(id);
            product.IsDelete = true;
            await _productRepository.UpdateProductAsync(product);
            await _productRepository.SaveChangeAsync();
            var gallery = product.productGalleries;
            product.productGalleries.ForEach(async pg =>
            {
                await DeleteAllProductGalleryServiceAsync(pg.Id);
            });
            await _productRepository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> IsProductExistServiceAsync(string title)
        {
            return await _productRepository.IsProductExistAsync(title);
        }
        #endregion








































































        #region product Gallery
        public async Task CreateProductGalleryServiceAsync(ProductGalleryViewModel gallery)
        {
            gallery?.ProductImages?.ForEach(async pg =>
            {
                var productGallery = new ProductGallery
                {
                    ProductImage = ImageService.CreateImage(pg, "ProductImages"),
                    ProductId = gallery.ProductId,
                    CreateDate = DateTime.Now,
                    IsDelete = false,
                };
                await _productRepository.CreateProductGalleryAsync(productGallery);

            });
            await _productRepository.SaveChangeAsync();
        }

        public async Task<List<EditProductGalleryViewModel>> GetProductGalleryByIdServiceAsync(int productId)
        {
            var productGalleries = await _productRepository.GetProductGallerisByParentIdAsync(productId);
            //var productGalleryImageUrls = new List<string>();

            //productGallery.ForEach(pg => {
            //    productGalleryImageUrls.Add(pg.ProductImage);
            //});

            return productGalleries.Select(pg => new EditProductGalleryViewModel
            {
                Id = pg.Id,
                ProductId = pg.ProductId,
                ProductImageUrl = pg.ProductImage,
            }).ToList();
        }

        public async Task DeleteAllProductGalleryServiceAsync(int galleryId)
        {
            ProductGallery pg = await _productRepository.GetProductGalleryByIdAsync(galleryId);

            await _productRepository.DeleteProductGalleryAsync(pg);
        }

        public async Task DeleteSingleProductGalleryServiceAsync(int galleryId)
        {
            var pg = await _productRepository.GetProductGalleryByIdAsync(galleryId);
            await _productRepository.DeleteProductGalleryAsync(pg);
            await _productRepository.SaveChangeAsync();
        }

        public async Task<IEnumerable<ProductColorViewModel>> GetAllProductColorsServiceAsync(int productId)
        {
            IEnumerable<ProductColor> productColors = await _productRepository.GetAllProductColorAsync(productId);

            return productColors.Select(pc => new ProductColorViewModel()
            {
                Id = pc.Id,
                Hex = pc.Hex,
                Price = pc.Price,
                ProductId = pc.ProductId,
                CreateDate = pc.CreateDate,
                IsDelete = pc.IsDelete,
            }).ToList();

        }

        public async Task<UpdateProductColorViewModel> GetProductColorServiceAsync(int colorId)
        {
            ProductColor productColor = await _productRepository.GetProductColorAsync(colorId);
            return new UpdateProductColorViewModel()
            {
                Id = productColor.Id,
                Hex = productColor.Hex,
                Price = productColor.Price,
                ProductId = productColor.ProductId,
                CreateDate = productColor.CreateDate,
                IsDelete = productColor.IsDelete,
            };
        }

     

        public async Task AddProductColorServiceAsync(AddProductColorViewModel color)
        {
            ProductColor pColor = new ProductColor()
            {
                Hex = color.Hex,
                Price = color.Price,
                ProductId = color.ProductId,

            };
            await _productRepository.AddProductColorAsync(pColor);
            await _productRepository.SaveChangeAsync();
        }

        public async Task UpdateProductColorServiceAsync(UpdateProductColorViewModel color)
        {
            ProductColor productColor = await _productRepository.GetProductColorAsync(color.Id);
            productColor.Hex = color.Hex;
            productColor.Price = color.Price;
            await _productRepository.UpdateProductColorAsync(productColor);
            await _productRepository.SaveChangeAsync();

        }

        public async Task<UpdateProductColorViewModel> GetColorForUpdateServiceAsync(int colorId)
        {
            ProductColor productColor = await _productRepository.GetProductColorAsync(colorId);
            return new UpdateProductColorViewModel()
            {
                Id = productColor.Id,
                Hex = productColor.Hex,
                Price = productColor.Price,
            };
        }

        public async Task DeleteProductColorServiceAsync(int colorId)
        {
            ProductColor productColor = await _productRepository.GetProductColorAsync(colorId);
            productColor.IsDelete = true;
            await _productRepository.UpdateProductColorAsync(productColor);
            await _productRepository.SaveChangeAsync(); 
        }
        #endregion
    }
}
