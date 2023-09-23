using EShop.Domain.Models.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EShop.Domain.Interfaces
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Discount>> GetAllDiscountsAsync();
        Task SaveChangesAsync();
        Task CreateDiscountAsync(Discount discount);
    }
}
