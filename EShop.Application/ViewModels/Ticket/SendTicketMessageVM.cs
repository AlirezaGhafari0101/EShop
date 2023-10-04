using System.ComponentModel.DataAnnotations;

namespace EShop.Application.ViewModels.Ticket
{
    public class SendTicketMessageVM
    {
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(800, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string Message { get; set; }

    }
}
