using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Rating;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.Repository
{
    public class RateRepository : IRateRepository
    {
        private readonly EshopDBContext _ctx;

        public RateRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }
        public async Task CreateRateAsync(Rate rate)
        {
            await _ctx.Rates.AddAsync(rate);
        }
        //public void UpdateRateAsync(Rate rate)
        //{
        //     _ctx.Rates.Update(rate);
        //}

        public async Task<Rate> FindRateExistAsync(int productId, int userId)
        {
            return await _ctx.Rates.FirstOrDefaultAsync(r => r.ProductId == productId && r.UserId == userId);
        }

        public async Task<double> CalculateAverageRateForProductAsync(int productId)
        {
            return await _ctx.Rates.Where(r => r.ProductId == productId).AverageAsync(r => ((double)r.RateScore));
        }
    }
}
