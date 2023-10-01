using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models.Users.Ticket
{
    public class Ticket : BaseEntity
    {
        [Display(Name = "عنوان تیکت")]
        [Required(ErrorMessage = "پر کردن {0} الزامی می باشد.")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کااراکتر باشد.")]
        public string Title { get; set; }
        public bool Pending { get; set; }
        public bool Answered { get; set; }
        [Required]
        public int UserId { get; set; }


        #region Relations
        public List<TicketMessage> TicketMessages { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }
}
