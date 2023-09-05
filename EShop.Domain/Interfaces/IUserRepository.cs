﻿using EShop.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Register(User user);

        Task<User> Login(string email,  string password);

        Task<User> GetUserById(int UserId);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);

    }
}
