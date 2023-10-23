using EShop.Application.Services.Interfaces;
using EShop.Application.ViewModels;
using EShop.Application.ViewModels.Order;
using EShop.Application.ViewModels.Product.Color;
using EShop.Application.ViewModels.Wallet;
using EShop.Data.Repository;
using EShop.Domain.Interfaces;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Users;
using EShop.Domain.Models.Wallet;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace EShop.Application.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;
        private readonly IWalletService _walletService;
        public OrderService(IOrderRepository orderRepository
            , IProductService productService, IWalletService walletService
            )
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _walletService = walletService;

        }

        public async Task<int> AddToOrderServiceAsync(int userId, int productId, int colorId)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllOrdersAsync();
            Order order = orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            UpdateProductColorViewModel color = await _productService.GetProductColorServiceAsync(colorId);

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
                            Count = 1,
                            Price = color.Price,

                        }
                    }
                };

                await _orderRepository.AddOrderAsync(order);
                await _orderRepository.SaveChangeAsync();
                color.Count -= 1;
                await _productService.UpdateProductColorServiceAsync(color);
                await _orderRepository.SaveChangeAsync();
            }
            else
            {
                if(order.Wallet !=null && order.Wallet.IsPay == false)
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
                            Count = 1,
                            Price = color.Price,

                        }
                    }
                    };

                    await _orderRepository.AddOrderAsync(order);
                    await _orderRepository.SaveChangeAsync();
                    color.Count -= 1;
                    await _productService.UpdateProductColorServiceAsync(color);
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
                    color.Count -= 1;
                    await _productService.UpdateProductColorServiceAsync(color);
                    await _orderRepository.SaveChangeAsync();
                }
            }
            return order.Id;
        }



        public async Task<bool> DeleteOrderCountServiceAsync(int orderDetailId, int orderId)
        {
            OrderDetail orderDetail = await _orderRepository.GetOrderDetailByIdAsync(orderDetailId);

            if (orderDetail != null)
            {
                if (orderDetail.Count >= 2)
                {
                    orderDetail.Count -= 1;
                    orderDetail.Price -= orderDetail.Color.Price;
                    orderDetail.Color.count += 1;

                    await _orderRepository.SaveChangeAsync();

                }
                else
                {
                    await _orderRepository.DeleteOrderDetailAsync(orderDetail);
                    orderDetail.Color.count += 1;

                    await _orderRepository.SaveChangeAsync();

                }


                IEnumerable<OrderDetail> detailsOrder = await _orderRepository.GetOrderDetailsForOrder(orderId);

                if (detailsOrder.IsNullOrEmpty())
                {
                    Order order = await _orderRepository.GetOrderAsync(orderId);
                    await _orderRepository.DeleteOrderAsync(order);
                    await _orderRepository.SaveChangeAsync();

                }

                _orderRepository.UpdatePriceOrder(orderId);

                return true;

            }
            else { return false; }
        }

        public async Task<bool> DeleteOrderServiceAsync(int orderDetailId, int orderId)
        {
            OrderDetail orderDetail = await _orderRepository.GetOrderDetailByIdAsync(orderDetailId);
            await _orderRepository.DeleteOrderDetailAsync(orderDetail);
            orderDetail.Color.count += 1;
            await _orderRepository.SaveChangeAsync();


            IEnumerable<OrderDetail> detailsOrder = await _orderRepository.GetOrderDetailsForOrder(orderId);

            if (detailsOrder.IsNullOrEmpty())
            {
                Order order = await _orderRepository.GetOrderAsync(orderId);
                await _orderRepository.DeleteOrderAsync(order);
                await _orderRepository.SaveChangeAsync();

            }


            return true;

        }

        public async Task<OrderVM> ShowUserOrderInforServiceAsync(int userId)
        {

            Order orderInfor = await _orderRepository.GetUserOrderInforAsync(userId);
            if (orderInfor != null)
            {
                return new OrderVM()
                {
                    Id = orderInfor.Id,
                    IsFinaly = orderInfor.IsFinaly,
                    OrderDetails = orderInfor.OrderDetails,
                    OrderSum = orderInfor.OrderSum,
                    User = orderInfor.User,
                    CreateDate = orderInfor.CreateDate,
                    IsDelete = orderInfor.IsDelete,
                    UserId = orderInfor.UserId,
                };
            }
            else
            {
                return null;
            }

        }
        #region Payment


        public async Task IsFinallyOrder(int orderId, double amountPaid)
        {
            Order order = await _orderRepository.GetOrderAsync(orderId);
            order.IsFinaly = true;
            order.AmountPaid = amountPaid;
            await _orderRepository.UpdateOrderAsync(order);
            await _orderRepository.SaveChangeAsync();
        }

        public async Task<WalletVM> BillPaymentWithWalletServiceAsync(int userId, double amount, string description, int orderId,bool isPay,int paymentMethod)
        {
     
            HarvestVM harvestVM = new HarvestVM()
            {
                Description = description,
                OrderId = orderId,
                Amount = amount,
                UserId = userId,
                TypeId = 2,
                IsPay= isPay,
                PaymentType= (PaymentType)paymentMethod


            };
            int walletId = await _walletService.HarvestServiceAsync(harvestVM);

            return await _walletService.GetWalletByIdServiceAsync(walletId);
        }

        public async Task<IEnumerable<OrderVM>> GetAllOrdersForAdminServiceAsync()
        {
            IEnumerable<Order> orderList = await _orderRepository.GetAllOrdersForAdminAsync();

            return orderList.Select(order => new OrderVM()
            {
                Id = order.Id,
                CreateDate = order.CreateDate,
                IsDelete = order.IsDelete,
                IsFinaly = order.IsFinaly,
                OrderSum = order.OrderSum,
                User = order.User,
                UserId = order.UserId,
                AmountPaid=(double)order.AmountPaid

            }).ToList();


        }

        public async Task<IEnumerable<OrderDetailVM>> GetOrderDetailsForAdminServiceAsync(int orderId)
        {
            IEnumerable<OrderDetail> orderDetail =await _orderRepository.GetOrderDetailsForAdminAsync(orderId);

            return orderDetail.Select(od => new OrderDetailVM()
            {
                Count = od.Count,
                Price = od.Price,
                Color = od.Color,
                Product = od.Product,


            }).ToList(); 
        }

        public UserViewModel GetOrderUserService(int orderId)
        {
            User user=_orderRepository.GetOrderUser(orderId);

            return new UserViewModel()
            {
                Id=user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        public async Task DeleteOrderServiceAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);

            order.IsDelete = true;

            await _orderRepository.SaveChangeAsync();
        }




        #endregion



        #region UserPanel

        public async Task<IEnumerable<OrderVM>> GetUserOrderListServiceAsync(int userId)
        {
            IEnumerable<Order> orderList=await _orderRepository.GetUserOrderListAsync(userId);

            IEnumerable<OrderVM> result= orderList.Select(o => new OrderVM()
            {
                Id = o.Id,
                CreateDate = o.CreateDate,
                IsDelete = o.IsDelete,
                IsFinaly = o.IsFinaly,
                OrderSum = o.OrderSum,
                User = o.User,
                UserId = o.UserId,
                AmountPaid =o.AmountPaid,
                Wallet=o.Wallet !=null?new WalletVM() {
                    Id=o.Wallet.Id,
                    Amount = o.Wallet.Amount,
                    IsDelete = o.IsDelete,
                    CreateDate=o.CreateDate,
                    

                }:null
               
            }).ToList();

            return result;
        }

        public async Task<IEnumerable<OrderDetailVM>> GetUserOrderDetailsServiceAsync(int orderId)
        {
            IEnumerable<OrderDetail> orderDetail = await _orderRepository.GetUserOrderDetailsAsync(orderId);

            return orderDetail.Select(od => new OrderDetailVM()
            {
                Count = od.Count,
                Price = od.Price,
                Color = od.Color,
                Product = od.Product,


            }).ToList();
        }

        public async Task<OrderVM> GetOrderByIdServiceAsync(int orderId)
        {
            Order order=await _orderRepository.GetOrderAsync(orderId);

            return new OrderVM() {
            CreateDate = order.CreateDate,
            IsFinaly= order.IsFinaly,
            OrderSum = order.OrderSum,
            AmountPaid=order.AmountPaid!=null? order.AmountPaid:null
            };
        }
        #endregion
    }
}
