using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Data.Repository;
using EShop.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Ioc
{
    public class DependencyContainer
    {
        public static void UserServices(IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();

            service.AddScoped<IAccountService, AccountService>();
        }
    }
}
