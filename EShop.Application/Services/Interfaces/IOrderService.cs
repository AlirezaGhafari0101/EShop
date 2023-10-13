namespace EShop.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> AddToOrderServiceAsync(int userId, int productId, int colorId);

    }
}
