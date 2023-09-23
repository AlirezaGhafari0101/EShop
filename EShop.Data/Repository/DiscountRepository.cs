using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Discount;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.Repository
{
    public class DiscountRepository: IDiscountRepository
    {
        private readonly EshopDBContext _ctx;

        public DiscountRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<Discount>> GetAllDiscountsAsync()
        {
            return await _ctx.Discounts.ToListAsync();
        }

        public async Task CreateDiscountAsync(Discount discount) {
            await _ctx.Discounts.AddAsync(discount);
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
