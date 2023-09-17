using EShop.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Products
{
    public class Product: BaseEntity
    {
        [MaxLength(200)]
        public string Title{ get; set; }

        public string Description { get; set; }
  
        public string Tag { get; set; }

        public string Image { get; set; }

        public short Count { get; set; }
    }
}
