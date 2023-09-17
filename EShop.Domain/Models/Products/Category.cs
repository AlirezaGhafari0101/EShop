using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models.Products
{
    public class Category : BaseEntity
    {

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CategoryTitle { get; set; }


        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }


        #region Relations
        [ForeignKey("ParentId")]
        public List<Category> Categories { get; set; }


        [InverseProperty("Category")]
        public List<Product> ProductCategories { get; set; }
        #endregion

    }
}
