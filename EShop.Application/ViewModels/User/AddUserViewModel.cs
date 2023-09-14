using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.User
{
    public class AddUserViewModel
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(50, ErrorMessage ="{0} نمی تواند بیشتر از 50 کااراکتر باشد.")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از 50 کااراکتر باشد.")]
        public string Family { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(150, ErrorMessage = "{0} نمی تواند بیشتر از 150 کااراکتر باشد.")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از 50 کااراکتر باشد.")]
        public string Password { get; set; }

        [Display(Name = "آواتار")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public IFormFile Avatar { get; set; }




    }
}
