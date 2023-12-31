﻿using EShop.Application.ViewModels.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product.Color
{
    public class UpdateProductColorViewModel : BaseViewModel
    {
        [Display(Name = "اسم رنگ")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string ColorName { get; set; }
        public int ColorId { get; set; }
        [Display(Name = "کدرنگ")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string Hex { get; set; }
        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public double Price { get; set; }
        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public int Count { get; set; }
        public int ProductId { get; set; }



    }
}
