using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product.ProductGallery
{
    public class ProductGalleryViewModel
    {
        public List<IFormFile> ProductImages { get; set; }
        public int ProductId { get; set; }
    }
}
