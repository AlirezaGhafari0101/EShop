using EShop.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Users.Ticket
{
    public class TicketMessage:BaseEntity
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
        public Ticket Ticket { get; set; }

        #endregion
    }
}
