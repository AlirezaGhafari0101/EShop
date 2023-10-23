using EShop.Application.ViewModels.common;
using EShop.Application.ViewModels.Discount;
using EShop.Application.ViewModels.Product.Color;
using EShop.Application.ViewModels.Product.ProductGallery;
using EShop.Domain.Models.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name ="نام محصول")]
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
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از 200 کااراکتر باشد.")]
        public IFormFile Image { get; set; }

        public string? ImageName { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public short Count { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = " یک {0} انتخاب کنید  .")]
        public int CategoryId { get; set; }

        public List<ProductGalleryViewModel> Gallery { get; set; }

        public double Price { get; set; }
        [Display(Name = "ویژگی ها")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public string Features { get; set; }
        public int? DiscountId { get; set; }
        public double ProductRate { get; set; }
        public int RatesCount { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = " یک {0} انتخاب کنید  .")]
        public string CategoryTitle { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<ProductColorViewModel> Colors { get; set; }
        public List<UserFavourite.UserFavouriteViewModel>? UserFavourites { get; set; }

        public DiscountViewModel? Discount { get; set; }

        public List<Comment.CommentViewModel>? Comments { get; set; }
    }
}
