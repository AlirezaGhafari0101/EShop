using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels
{
    public class ContactUsViewModel
    {
        [Display(Name ="نام خانوادگی")]
        [Required]
        [StringLength(500)]

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
