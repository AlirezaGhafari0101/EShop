using EShop.Domain.Models.Wallet;

namespace EShop.Domain.Interfaces
{
    public interface IWalletRepository
    {
        Task HarvestAsync(Wallet wallet);
        Task<Wallet> GetWalletByIdAsync(int id);
        Task ChargeWalletAsync(Wallet wallet);
        Task<double> BalanceWalletAsync(int userId);
        void UpdateWallet(Wallet wallet);
        Task<Wallet> GetWalletByOrderIdAsync(int id);


        Task SaveChangeAsync();

        

    }
}
