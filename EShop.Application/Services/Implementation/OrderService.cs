using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels.Product.Color;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        public OrderService(IOrderRepository orderRepository
            , IProductService productService
            )
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public async Task<int> AddToOrderServiceAsync(int userId, int productId, int colorId)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllOrdersAsync();
            Order order = orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            ProductColorViewModel color = await _productService.GetColorByIdServiceAsync(colorId);
            if (order == null)
            {
                order = new Order()
                {
                    IsFinaly = false,
                    UserId = userId,
                    OrderSum = color.Price,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {

                            ColorId = colorId,
                            ProductId = productId,
                            Count=1,
                            Price=color.Price,
                        }
                    }
                };
                await _orderRepository.AddOrderAsync(order);
                await _orderRepository.SaveChangeAsync();


            }
            else
            {
                IEnumerable<OrderDetail> details = await _orderRepository.GetAllOrderDetailsAsync();
                OrderDetail detail = details.FirstOrDefault(or => or.OrderId == order.Id && or.ProductId == productId && or.ColorId == colorId);
                if (detail != null)
                {
                    detail.Count += 1;
                    detail.Price += color.Price;
                    await _orderRepository.UpdateOrderDetailAsync(detail);
                    await _orderRepository.SaveChangeAsync();
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        Count = 1,
                        ProductId = productId,
                        Price = color.Price,
                        ColorId = colorId,
                    };
                    await _orderRepository.AddOrderDetailAsync(detail);
                    await _orderRepository.SaveChangeAsync();
                }

                _orderRepository.UpdatePriceOrder(order.Id);

            }

            return order.Id;

        }



    }
}
