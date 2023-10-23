using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Wallet
{
    public enum PaymentType
    {
        [Display(Name ="پرداخت از طریق کیف پول")]
        Wallet=0,
        [Display(Name = "پرداخت اینترنتی")]
        InternetPayment = 1
    }
}
