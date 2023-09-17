using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.ContactUs
{
    public class EmailSendAnswerContactUsViewModel
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Answer { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
