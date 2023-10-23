using EShop.Domain.common;
using EShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Order
{
    public class OrderDetail:BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }



        #region Relations
        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
        [ForeignKey("ProductId")]
        public Products.Product? Product { get; set; }
        [ForeignKey("ColorId")]
        public ProductColor? Color { get; set; }
        #endregion
    }
}
