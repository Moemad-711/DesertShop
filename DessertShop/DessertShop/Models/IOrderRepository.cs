using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
