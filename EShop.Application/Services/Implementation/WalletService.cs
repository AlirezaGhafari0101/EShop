using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Wallet;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Wallet;

namespace EShop.Application.Services.Implementation
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<double> BalanceWalletAsyncService(int userId)
        {
            return await _walletRepository.BalanceWalletAsync(userId);
        }

        public async Task<int> ChargeWalletAsyncService(int userId, double amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                TypeId = 1,
                Description = description,
                IsPay = isPay,
                UserId = userId,

            };

            await _walletRepository.ChargeWalletAsync(wallet);
            await _walletRepository.SaveChangeAsync();

            return wallet.Id;
        }

        public async Task<WalletVM> GetWalletByIdServiceAsync(int id)
        {
            Wallet wallet = await _walletRepository.GetWalletByIdAsync(id);
            return new WalletVM()
            {
                Id = wallet.Id,
                OrderId = wallet.OrderId,
                Amount = wallet.Amount,
                CreateDate = wallet.CreateDate,
                Description = wallet.Description,
                IsDelete = wallet.IsDelete,
                IsPay = wallet.IsPay,
                TypeId = wallet.TypeId,
                UserId = wallet.UserId,
                PaymentType = wallet.PaymentType,


            };
        }

        public async Task<WalletVM> GetWalletByOrderIdServiceAsync(int orderId)
        {
            Wallet getWallet = await _walletRepository.GetWalletByOrderIdAsync(orderId);
            return new WalletVM()
            {
                Id = getWallet.Id,
                OrderId = getWallet.OrderId,
                IsPay = getWallet.IsPay,
                TypeId = getWallet.TypeId,
                Amount = getWallet.Amount,
                CreateDate = getWallet.CreateDate,
                Description = getWallet.Description,
                IsDelete = getWallet.IsDelete,
                UserId= getWallet.UserId,
                

            };
        }

        public async Task<int> HarvestServiceAsync(HarvestVM harvest)
        {
            Wallet wallet = new Wallet()
            {
                Amount = harvest.Amount,
                OrderId = harvest.OrderId,
                Description = harvest.Description,
                UserId = harvest.UserId,
                TypeId = harvest.TypeId,
                IsPay = harvest.IsPay,
                PaymentType = harvest.PaymentType,

            };
            await _walletRepository.HarvestAsync(wallet);
            await _walletRepository.SaveChangeAsync();
            return wallet.Id;


        }

        public async Task UpdateWalletServiceAsync(UpdateWalletVM updateWalletVM)
        {
            Wallet wallet = await _walletRepository.GetWalletByIdAsync(updateWalletVM.WalletId);

            wallet.IsPay = updateWalletVM.IsPay;

            _walletRepository.UpdateWallet(wallet);


        }
    }
}
