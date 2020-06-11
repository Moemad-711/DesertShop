using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public Guid stockitemId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public StockItem stockitem { get; set; }
        public Order Order { get; set; }
    }
}
