using System.ComponentModel.DataAnnotations;

namespace EShop.Application.ViewModels.Product.Color
{
    public class AddProductColorViewModel
    {
        [Display(Name = "اسم رنگ")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string ColorName { get; set; }

        [Display(Name = "کدرنگ")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string Hex { get; set; }
        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public int Price { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
