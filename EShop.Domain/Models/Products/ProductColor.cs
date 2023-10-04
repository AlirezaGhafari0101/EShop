using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models.Products
{
    public class ProductColor : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string ColorName { get; set; }
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
