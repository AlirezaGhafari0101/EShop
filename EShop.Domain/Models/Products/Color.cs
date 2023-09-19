﻿using EShop.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Products
{
    public class Color : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Hex { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int ProductId { get; set; }


        #region Relations
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        #endregion

    }
}
