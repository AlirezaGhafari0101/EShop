using EShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product.Category
{
    public class ProductCategroyViewModel
    {
        public int Id { get; set; }
        public string CategoryTitle { get; set; }
        public DateTime CreateDate { get; set; }
        public List<EShop.Domain.Models.Products.Category>? SubCategories { get; set; }
    }
}
