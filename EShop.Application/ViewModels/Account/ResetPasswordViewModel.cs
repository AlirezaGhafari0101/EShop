using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage ="وارد کردن {0}الزامی می باشد")]
        [StringLength(50)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "وارد کردن {0}الزامی می باشد")]
        [StringLength(50)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور با رمز عبور مغایرت دارد")]
        public string RepeatPassword { get; set; }

        public string ActiveCode { get; set; }


    }
}
