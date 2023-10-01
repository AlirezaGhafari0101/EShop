using EShop.Application.Convertors;
using EShop.Application.Services.Implementation;
using EShop.Application.Services.Interfaces;
using EShop.Data.Repository;
using EShop.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ZarinPal.Interface;
using ZarinPal.Class;





namespace EShop.Ioc
{
    public class DependencyContainer
    {
        public static void UserServices(IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();

            service.AddScoped<IAccountService, AccountService>();

            service.AddScoped<IViewRenderService, RenderViewToString>();

            service.AddScoped<IUserService, UserService>();

            service.AddScoped<Payment, Payment>();
        }

        public static void ContactUsServices(IServiceCollection service)
        {
            service.AddScoped<IContactUsRepository, ContactUsRepository>();
            service.AddScoped<IContactUsService, ContactUsService>();
        }

        public static void ProductServieces(IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IProductService, ProductService>();
        }

        public static void DiscountServices(IServiceCollection service)
        {
            service.AddScoped<IDiscountRepository, DiscountRepository>();
            service.AddScoped<IDiscountService, DiscountService>();
        }
    }
}
