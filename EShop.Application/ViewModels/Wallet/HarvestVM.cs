using EShop.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Wallet
{
    public class HarvestVM
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int TypeId { get; set; }
        public double Amount { get; set; }
        public bool IsPay { get; set; }
        public PaymentType  PaymentType { get; set; }


    }
}
