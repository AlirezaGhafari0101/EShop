using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product.Category
{
    public class UpdateCategoryViewModel
    {
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی می باشد")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string CategoryTitle { get; set; }
        
    }
}
