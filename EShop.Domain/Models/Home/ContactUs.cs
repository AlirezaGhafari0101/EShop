﻿using EShop.Domain.common;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Models.Home
{
    public class ContactUs : BaseEntity
    {

        [Required]
        [StringLength(250)]
        public string FullName { get; set; }
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        public string? Answer { get; set; }
        public bool IsRead { get; set; }
    }
}
