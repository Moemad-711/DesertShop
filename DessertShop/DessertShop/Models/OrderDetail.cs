//using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DessertShop.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
        
        public Guid stockitemId { get; set; }
        [ForeignKey(nameof(stockitemId))]
        public StockItem stockitem { get; set; }
        public int Amount { get; set; }
        [MaxLength(5)]
        public decimal Price { get; set; }
    }
}
