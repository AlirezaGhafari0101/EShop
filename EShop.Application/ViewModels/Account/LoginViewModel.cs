using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی میباشد")]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی میباشد")]
        [StringLength(50)]
        [MinLength(6, ErrorMessage = "طول {0} حداقل {1} میباشد")]
        public string Password { get; set; }
    }
}
