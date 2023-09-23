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

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
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

        public async Task CreateDiscountServiceAsync(AddDiscountViewModel discountModel)
        {
            var discount = new Discount
            {
                IsDelete = false,
                DiscountCode = discountModel.DiscountCode,
                DiscountPercentage = discountModel.DiscountPercentage,
                StartDate = discountModel.StartDate,
                EndDate = discountModel.EndDate,
                IsActive=discountModel.IsActive,
                CreateDate=DateTime.Now,   
            };
            await _discountRepository.CreateDiscountAsync(discount);
            await _discountRepository.SaveChangesAsync();
        }

    }
}
