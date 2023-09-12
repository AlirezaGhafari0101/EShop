using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EShop.Application.ViewModels.User.UserPanel
{
    public class EditProfileViewModel
    {

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی میباشد")]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "{0} وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی میباشد")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن {0} الزامی میباشد")]
        [StringLength(50)]
        public string LastName { get; set; }

        public IFormFile? Avatar { get; set; }
        public string AvatarName { get; set; }
    }
}
