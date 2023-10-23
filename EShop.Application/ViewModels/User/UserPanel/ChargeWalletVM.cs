using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.User.UserPanel
{
    public class ChargeWalletVM
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(10000, double.MaxValue, ErrorMessage = "مقدار {0} وارد شده غیر مجاز می باشد")]
        public double Amount { get; set; }
    }
}
