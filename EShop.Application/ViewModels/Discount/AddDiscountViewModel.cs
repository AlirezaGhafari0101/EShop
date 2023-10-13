using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Discount
{
    public class AddDiscountViewModel
    {
        public string? DiscountCode { get; set; }


        [Required(ErrorMessage = "پر کردن این فیلد الزامی می باشد.")]
        [Range(0,100, ErrorMessage ="درصد تخفیف نمی تواند از 100 بیشتر باشد.")]
        public int DiscountPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
