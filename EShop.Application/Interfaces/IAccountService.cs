﻿using EShop.Application.ViewModels.Users;
using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Interfaces
{
    public interface IAccountService
    {
        void UserRegister(RegisterViewModel registerViewModel);
        Task<User> UserLogin(LoginViewModel loginViewModel);

    }
}
