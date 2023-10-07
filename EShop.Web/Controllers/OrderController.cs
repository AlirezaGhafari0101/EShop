﻿using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace EShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("add-to-order/{productId}/{colorId}")]
        [Authorize]
        public async Task<int> AddToOrder(int productId, int colorId)
        {
            int usaerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           int orderId= await _orderService.AddToOrderServiceAsync(usaerId, productId, colorId);
            return orderId;
        }
    }
}
