using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
namespace EShop.Domain.Models.Banner
{
    public class Banner:BaseEntity
    {
        [Display(Name = "موقعیت بنر")]
        [StringLength(50,ErrorMessage ="{0}نمی تواند بیشتر از {1}کاراکتر باشد")]
        public string Position { get; set; }
       
        public string Image { get; set; }
    }
}
