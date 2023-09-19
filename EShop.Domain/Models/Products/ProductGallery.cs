using EShop.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Products
{
    public class ProductGallery : BaseEntity
    {
        public string ProductImage { get; set; }

        #region Relations


        #region Product
        public int ProductId { get; set; }
        public Product Product { get; set; }
        #endregion

        #endregion

    }
}
