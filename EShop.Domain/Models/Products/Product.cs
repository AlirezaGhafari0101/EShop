using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.Domain.Models.Discount;

namespace EShop.Domain.Models.Products
{
    public class Product : BaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public string Image { get; set; }

        public string? Features { get; set; }

        public short Count { get; set; }


        [Required]
        public int CategoryId { get; set; }

        
        public int? DiscountId { get; set; }


        #region Relations

        #region Category
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        #endregion

        #region Gallery
        
        public List<ProductGallery> productGalleries { get; set; }

        #endregion

        #region Discount




        public Discount.Discount? Discount { get; set; }

        #endregion

        public List<Color> Colors { get; set; }

        #endregion

    }
}
