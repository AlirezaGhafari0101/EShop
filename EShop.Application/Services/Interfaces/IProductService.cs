using EShop.Application.ViewModels.Product;

namespace EShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        #region MyRegion

        #endregion







        #region Products

        Task<IEnumerable<ProductViewModel>> GetAllProductsServiceAsync();

        #endregion



    }
}
