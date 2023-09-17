using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.ContactUs
{
    public class CreateContactUsViewModel
    {
 
  
        
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }
        
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Message { get; set; }
    }
}
