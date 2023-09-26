using EShop.Application.Convertors;
using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Discount;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementation
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;

        public DiscountService(IDiscountRepository discountRepository, IProductRepository productRepository)
        {
            _discountRepository = discountRepository;
            _productRepository = productRepository; 

        }


        public async Task<IEnumerable<DiscountViewModel>> GetAllDiscountsServiceAsync()
        {
            var discounts = await _discountRepository.GetAllDiscountsAsync();
            return discounts.Select(d => new DiscountViewModel
            {
                Id = d.Id,
                DiscountCode = d.DiscountCode,
                DiscountPercentage = d.DiscountPercentage,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                IsActive = d.IsActive,
            }).ToList();
        }

        public async Task<DiscountViewModel> GetDiscountByIdServiceAsync(int id)
        {
            var discount = await _discountRepository.GetDiscountByIdAsync(id);
            return new DiscountViewModel
            {
                Id= id,
                DiscountCode = discount.DiscountCode,
                DiscountPercentage = discount.DiscountPercentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                IsActive = discount.IsActive,
            };
        }

        public async Task CreateDiscountServiceAsync(AddDiscountViewModel discountModel)
        {
            var discount = new Discount
            {
                IsDelete = false,
                DiscountCode = discountModel.DiscountCode,
                DiscountPercentage = discountModel.DiscountPercentage,
                StartDate = FixedText.FixShamsiDateToAdDate(discountModel.StartDate),
                EndDate = FixedText.FixShamsiDateToAdDate(discountModel.EndDate),
                IsActive=discountModel.IsActive,
                CreateDate=DateTime.Now,   
            };
            await _discountRepository.CreateDiscountAsync(discount);
            await _discountRepository.SaveChangesAsync();
        }

        public async Task UpdateDiscountServiceAsync(EditDiscountViewModel discountModel, int id)
        {
            var discount = await _discountRepository.GetDiscountByIdAsync(id);
            discount.DiscountCode = discountModel.DiscountCode;
            discount.DiscountPercentage = discountModel.DiscountPercentage;
            discount.StartDate = FixedText.FixShamsiDateToAdDate(discountModel.StartDate);
            discount.EndDate = FixedText.FixShamsiDateToAdDate(discountModel.EndDate);
            discount.IsActive = discountModel.IsActive;

            await _discountRepository.UpdateDiscountAsync(discount);
            await _discountRepository.SaveChangesAsync();
        }

        public async Task DeleteDiscountServiceAsync(int id)
        {
            var discount = await _discountRepository.GetDiscountByIdAsync(id);
            var products = discount.Products;
            discount.IsDelete = true;
            await _discountRepository.UpdateDiscountAsync(discount);
            await _discountRepository.SaveChangesAsync();
            products?.ForEach(async p =>
            {
                p.DiscountId = null;
                await _productRepository.UpdateProductAsync(p);
            });
           await _productRepository.SaveChangeAsync();
        }
    }
}
