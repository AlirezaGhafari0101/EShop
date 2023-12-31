﻿using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.Domain.Models.Discount;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Users;
using EShop.Domain.Models.Rating;

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

        public List<ProductColor>? Colors { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }

        #region Favourite
        public List<UserFavourite>? UserFavourites { get; set; }

        #endregion

        #region Comments

        public List<Comment.Comment>? Comments { get; set; }
        public List<Rate> Rates { get; set; }

        #endregion


        #endregion

    }
}
