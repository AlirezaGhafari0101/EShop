using EShop.Domain.Models.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IRateRepository
    {
        Task CreateRateAsync(Rate rate );
        //void UpdateRateAsync(Rate rate );
        Task<Rate> FindRateExistAsync(int productId, int userId);
        Task<double> CalculateAverageRateForProductAsync(int productId);
    }
}
