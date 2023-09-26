using EShop.Application.Convertors;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product;
using EShop.Application.ViewModels.Product.Category;
using EShop.Application.ViewModels.Product.Color;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Products;
using EShop.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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

            var subGroups = await _productRepository.GetAllCategoriesAsync(group.Id);
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

        public async Task<IEnumerable<ProductCategroyViewModel>> GetAllCategoriesForCreatingProductServiceAsync()
        {
            var categories = await _productRepository.GetAllCategoriesForCreatingProductAsync();
            return categories.Select(c => new ProductCategroyViewModel
            {
                Id = c.Id,
                CategoryTitle = c.CategoryTitle,   
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
                Count = product.Count,
                CreatedDate = product.CreateDate
            };

        }
        public async Task<bool> CreateProductServiceAsync(AddProductViewModel model)
        {
            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                Count = model.Count,
                Tag = model.Tag,
                CategoryId = model.CategoryId,
                Image = ImageService.CreateImage(model.Image, "ProductImages"),
                IsDelete = false,
                CreateDate = DateTime.Now,
            };

            await _productRepository.CreateProductAsync(product);
            await _productRepository.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateProductServiceAsync(EditProductViewModel model, int id)
        {
            var selectedProduct = await _productRepository.GetProductByIdAsync(id);

            if(model.Image != null) {
                selectedProduct.Image = ImageService.CreateImage(model.Image, "ProductImages", model.ImageName);
            }
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
            var product = await _productRepository.DeleteProductAsync(id);
            await _productRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> IsProductExistServiceAsync(string title)
        {
            return await _productRepository.IsProductExistAsync(title);
        }
        #endregion

        #region ProductColor
        public async Task<IEnumerable<ProductColorViewModel>> GetAllProductColorsServiceAsync(int productId)
        {
            IEnumerable<ProductColor> productColors = await _productRepository.GetAllProductColorsAsync(productId);

            return  productColors.Select(c => new ProductColorViewModel() {
            Id=c.Id,
            Hex=c.Hex,
            ProductId=c.ProductId,    
            CreateDate=c.CreateDate,
            Price=c.Price,
            IsDelete=c.IsDelete,
            }).ToList();
        }

        public async Task<UpdateProductColorViewModel> GetProductColorServiceAsync(int colorId)
        {
           ProductColor productColor=await _productRepository.GetProductColorAsync(colorId);
            return new UpdateProductColorViewModel() {
                Id = productColor.Id,
                Hex = productColor.Hex,
                ProductId = productColor.ProductId,
                CreateDate = productColor.CreateDate,
                Price = productColor.Price,
                IsDelete = productColor.IsDelete,
            };
        }

        public async Task AddProductColorServiceAsync(AddProductColorViewModel color)
        {
            ProductColor pColor=new ProductColor()
            {
                Hex=color.Hex,
                Price=color.Price,
                ProductId=color.ProductId,
                
            };

            await _productRepository.AddProductColorAsync(pColor);
            await _productRepository.SaveChangeAsync();
        }

        public async Task UpdateProductColorServiceAsync(UpdateProductColorViewModel color)
        {
            ProductColor pColor =await _productRepository.GetProductColorAsync(color.Id);
            pColor.Hex = color.Hex;
            pColor.Price= color.Price;

            await _productRepository.UpdateProductColorAsync(pColor);
            await _productRepository.SaveChangeAsync();
        }

        public async Task DeleteProductColorServiceAsync(int colorId)
        {
            ProductColor pColor = await _productRepository.GetProductColorAsync(colorId);

            pColor.IsDelete = true;
            await _productRepository.UpdateProductColorAsync(pColor);
            await _productRepository.SaveChangeAsync();



        }

        public async Task<UpdateProductColorViewModel> GetColorForUpdateServiceAsync(int colorId)
        {
            ProductColor GetColor = await _productRepository.GetProductColorAsync(colorId);
            return new UpdateProductColorViewModel()
            {
               Hex=GetColor.Hex,
               Price=GetColor.Price,
               ProductId=GetColor.ProductId,
               ColorId=colorId  
            };
        }
        #endregion
    }
}
