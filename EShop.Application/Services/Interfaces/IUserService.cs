using EShop.Application.ViewModels;
using EShop.Application.ViewModels.User;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Interfaces
{
    public interface IUserService
    {
     
       Task<bool> IsExistUserEmailService(string email);

        Task CreateUserAsync(AddUserViewModel userViewModel);

        Task<List<UserViewModel>> GetAllUsersAsync();
      
    }
}
