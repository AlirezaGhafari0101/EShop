using EShop.Application.ViewModels.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Comment
{
    public class AddCommentViewModel:BaseViewModel
    {
        public string Message { get; set; }      
        public int ProductId { get; set; }
        public int UserId { get; set; }
       
    }
}
