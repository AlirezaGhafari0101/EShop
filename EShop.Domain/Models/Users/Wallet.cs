﻿using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Models.Users
{
    public class Wallet : BaseEntity
    {

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }
        [Display(Name = "مبلغ")]
        public int Amount { get; set; }
        [Display(Name = "تایید شده")]

        public bool IsPay { get; set; }


        #region Relations
        [ForeignKey("TypeId")]
        public WalletType WalletType { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion

    }
}
