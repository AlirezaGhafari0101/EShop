using EShop.Domain.common;
using EShop.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Discount
{
    public class Discount : BaseEntity
    {
        public string? DiscountCode { get; set; }
        public int DiscountPercentage { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }

        #region Relations

        public List<Product>? Products { get; set; }

        #endregion
    }
}
