using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Models.Rating
{
    public enum RatingScores
    {
        [Display(Name ="بسیار ضعیف")]
        OneStar = 1,
        [Display(Name = "ضعیف")]
        TwoStars = 2,
        [Display(Name = "متوسط")]
        ThreeStars = 3,
        [Display(Name = "خوب")]
        FourStars = 4,
        [Display(Name = "عالی")]
        FiveStars = 5
    }
}
