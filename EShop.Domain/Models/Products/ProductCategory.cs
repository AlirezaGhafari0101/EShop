using EShop.Domain.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Products
{
    public class ProductCategory:BaseEntity
    {
      
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }


        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }


        #region Relations
        [ForeignKey("ParentId")]
        public List<ProductCategory> ProductCategories { get; set; }


        [InverseProperty("ProductCategory")]
        public List<Product> CateGories { get; set; }

        [InverseProperty("Category")]
        public List<Product> SubCategories { get; set; }
        #endregion

    }
}
