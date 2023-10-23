using EShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Order
{
    public class OrderDetailVM
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

        #region Relations
        [ForeignKey("OrderId")]
        public EShop.Domain.Models.Order.Order? Order { get; set; }
        [ForeignKey("ProductId")]
        public EShop.Domain.Models.Products.Product? Product { get; set; }
        [ForeignKey("ColorId")]
        public ProductColor? Color { get; set; }
        #endregion
    }
}
