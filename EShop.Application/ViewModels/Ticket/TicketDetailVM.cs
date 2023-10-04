using EShop.Domain.Models.Users.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Ticket
{
    public class TicketDetailVM
    {
        public IEnumerable<TicketMessage>? Messages { get; set; }
        [Display(Name = "متن پیام")]
        public string? Message { get; set; }
        public bool? Closed { get; set; }


    }
}
