using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Email { get; set; }
        
        [StringLength(50)]
        public string Password { get; set; }
      
        [StringLength(50)]
        public string FirstName { get; set; }
      
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(150)]
        public string Avatar { get; set; }
        [StringLength(150)]
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
