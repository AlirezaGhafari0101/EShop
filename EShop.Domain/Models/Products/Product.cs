using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models.Products
{
    public class Product : BaseEntity
    {
        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public string Image { get; set; }

        public string Features { get; set; }

        public short Count { get; set; }


        [Required]
        public int CategoryId { get; set; }





        #region Relations

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<Color> Colors { get; set; }

        #endregion

    }
}
