using EShop.Domain.common;
using EShop.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models.Ticket
{
    public class Ticket : BaseEntity
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

        public TicketSection TicketSection { get; set; }
        public TicketPriority TicketPriority { get; set; }

        #region Relations
        public List<TicketMessage> TicketMessages { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }
}
