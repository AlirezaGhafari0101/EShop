using EShop.Application.ViewModels.Wallet;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface IWalletService
    {
        Task<int> HarvestServiceAsync(HarvestVM harvest);
        Task<WalletVM> GetWalletByIdServiceAsync(int id);
        Task<int> ChargeWalletAsyncService(int userId, double amount, string description,bool isPay=false);
        Task<double> BalanceWalletAsyncService(int userId);
        Task UpdateWalletServiceAsync(UpdateWalletVM updateWalletVM);
        Task<WalletVM> GetWalletByOrderIdServiceAsync(int orderId);


    }
}
