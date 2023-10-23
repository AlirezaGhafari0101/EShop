using EShop.Application.ViewModels.common;
using EShop.Domain.Models.Wallet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZarinPal.Class;

namespace EShop.Application.ViewModels.Wallet
{
    public class WalletVM:BaseViewModel
    {
        [Display(Name = "نوع تراکنش")]
        public int TypeId { get; set; }

        [Display(Name = "کاربر")]
        public int UserId { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [Display(Name = "مبلغ")]
        [Range(10000,double.MaxValue,ErrorMessage ="مقدار {0} وارد شده غیر مجاز می باشد")]
        
        public double Amount { get; set; }
        [Display(Name = "تایید شده")]

        public bool IsPay { get; set; }

        public int? OrderId { get; set; }
        public PaymentType? PaymentType { get; set; }
    }
}
