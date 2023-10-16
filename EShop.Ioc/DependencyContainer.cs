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

            service.AddScoped<IUserFavouriteRepository, UserFavouriteRepository>();
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
            service.AddScoped<IRateRepository, RateRepository>();
            
        }

        public static void DiscountServices(IServiceCollection service)
        {
            service.AddScoped<IDiscountRepository, DiscountRepository>();
            service.AddScoped<IDiscountService, DiscountService>();
        }


        public static void TicketServices(IServiceCollection service)
        {
            service.AddScoped<ITicketRepository, TicketRepository>();
            service.AddScoped<ITicketService, TicketService>();
        }

        public static void OrderServices(IServiceCollection service)
        {
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IOrderService, OrderService>();
        }

        public static void CommentService(IServiceCollection service)
        {
            service.AddScoped<ICommentRepository, CommentRepository>();
            service.AddScoped<ICommentService, CommentService>();
            service.AddScoped<IUserCommentLikeOrDislikeRepository, UserCommentLikeOrDislikeRepository>();
        }

  
    }
}
