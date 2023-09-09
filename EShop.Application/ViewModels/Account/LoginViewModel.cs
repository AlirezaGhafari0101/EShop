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
        [Required]
        [EmailAddress(ErrorMessage ="فرمت ایمیل وارد شده نادرست می باشد")]
        public string Email { get; set; }
        [Required]
        
        public string Password { get; set; }
    }
}
