using EShop.Application.ViewModels.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.UserFavourite
{
    public class UserFavouriteViewModel: BaseViewModel
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string? ProductImageName { get; set; }
        public string? ProductTitle { get; set; }
        public int ProductPrice { get; set; }
    }
}
