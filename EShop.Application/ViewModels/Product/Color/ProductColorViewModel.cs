using EShop.Application.ViewModels.common;
using EShop.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product.Color
{
    public class ProductColorViewModel:BaseViewModel
    {
        
        public string Hex { get; set; }
       
        public double Price { get; set; }
 
        public int ProductId { get; set; }

        public string ColorName { get; set; }
        public int Count { get; set; }



    }
}
