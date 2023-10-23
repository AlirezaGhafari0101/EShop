using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Wallet
{
    public class UpdateWalletVM
    {

        public int WalletId { get; set; }
        public bool IsPay { get; set; }

        
    }
}
