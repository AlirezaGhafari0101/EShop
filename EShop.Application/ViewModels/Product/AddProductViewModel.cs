using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product
{
    public class AddProductViewModel
    {
      
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از 200 کااراکتر باشد.")]
        public string Title { get; set; }

        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public string Description { get; set; }

        [Display(Name = "تگ")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]

        public string Tag { get; set; }

        [Display(Name = "تصویر محصول")]  
        public IFormFile Image { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public short Count { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = " یک {0} انتخاب کنید  .")]
        public int CategoryId { get; set; }


    }
}
