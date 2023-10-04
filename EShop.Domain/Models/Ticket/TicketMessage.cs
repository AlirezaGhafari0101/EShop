using EShop.Domain.common;
using EShop.Domain.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EShop.Domain.Models.Ticket
{
    public class TicketMessage : BaseEntity
    {
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        public string Message { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int TicketId { get; set; }
        #region Relations
        [ForeignKey("TicketId")]
        public Ticket? Ticket { get; set; }
        [ForeignKey("SenderId")]
        public User? User { get; set; }
        #endregion
    }
}
