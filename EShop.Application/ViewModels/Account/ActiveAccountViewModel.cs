using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels.Account
{
    public class ActiveAccountViewModel
    {
        [Required]
        [StringLength(6)]
        [MinLength(6)]
        public string ActiveCode { get; set; }
    }
}
