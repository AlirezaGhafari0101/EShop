using EShop.Domain.Models.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Ticket
{
    public class CreateTicketVM
    {
        [Display(Name = "عنوان تیکت")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string Title { get; set; }
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public string Message { get; set; }
   
        [Required(ErrorMessage = "انتخاب کردن کاربر الزامی می باشد.")]
        public int WichUser { get; set; }
        [Required(ErrorMessage = "انتخاب کردن کاربر الزامی می باشد.")]

        public TicketSection TicketSection { get; set; }
        [Required]
        public TicketPriority TicketPriority { get; set; }
    }
}
