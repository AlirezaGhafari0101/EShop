using EShop.Application.ViewModels.common;
using EShop.Domain.Models.Ticket;
using System.ComponentModel.DataAnnotations;

namespace EShop.Application.ViewModels.Ticket
{
    public class TicketVM : BaseViewModel
    {
        [Display(Name = "عنوان تیکت")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string Title { get; set; }
        public bool Pending { get; set; }
        public bool Answered { get; set; }
        public bool Closed { get; set; }
        public DateTime UpdateDate { get; set; }
        [Required]
        public int UserId { get; set; }

        public IEnumerable<TicketMessage> TicketMessages { get; set; }

        public TicketSection TicketSection { get; set; }
        public TicketPriority TicketPriority { get; set; }
    }
}
