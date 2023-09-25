using EShop.Application.ViewModels.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountViewModel>> GetAllDiscountsServiceAsync();
        Task<DiscountViewModel> GetDiscountByIdServiceAsync(int id);
        Task CreateDiscountServiceAsync(AddDiscountViewModel discountModel);
        Task UpdateDiscountServiceAsync(EditDiscountViewModel discountModel, int id);
        Task DeleteDiscountServiceAsync(int discountId);
    }
}
