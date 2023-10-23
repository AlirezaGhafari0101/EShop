using EShop.Data.Context;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly EshopDBContext _ctx;
        public WalletRepository(EshopDBContext ctx)
        {

            _ctx = ctx;

        }

        public async Task<double> BalanceWalletAsync(int userId)
        {
            List<double> diposit = await _ctx.Wallets.Where(w => w.UserId == userId && w.IsPay && w.TypeId == 1).Select(w => w.Amount).ToListAsync();

            List<double> harvest = await _ctx.Wallets.Where(w => w.UserId == userId && w.IsPay && w.TypeId == 2).Select(w => w.Amount).ToListAsync();

            return (diposit.Sum() - harvest.Sum());
        }

        public async Task ChargeWalletAsync(Wallet wallet)
        {

            await _ctx.Wallets.AddAsync(wallet);

        }

        public async Task<Wallet> GetWalletByIdAsync(int id)
        {
            return await _ctx.Wallets.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Wallet> GetWalletByOrderIdAsync(int id)
        {
            return await _ctx.Wallets.FirstOrDefaultAsync(w => w.OrderId == id);
        }

        public async Task HarvestAsync(Wallet wallet)
        {
            await _ctx.AddAsync(wallet);

        }



        public async Task SaveChangeAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public void UpdateWallet(Wallet wallet)
        {
            _ctx.Wallets.Update(wallet);
        }
    }
}
