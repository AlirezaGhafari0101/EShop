using EShop.Data.Context;
using EShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Data.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly EshopDBContext _ctx;
        public OrderRepository(EshopDBContext ctx)
        {
            _ctx = ctx;
        }

    }
}
