using EShop.Application.ViewModels.common;
using EShop.Domain.Models.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Product.Rate
{
    public class AddRateViewModel:BaseViewModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public RatingScores RateScore { get; set; }
    }
}
