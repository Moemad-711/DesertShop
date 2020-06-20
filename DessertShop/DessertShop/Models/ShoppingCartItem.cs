using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public Guid ShoppingCartItemId { get; set; }
        public Guid stockitemId { get; set; }
        [ForeignKey(nameof(stockitemId))]
        public StockItem stockitem { get; set; }
        [MaxLength(3)]
        public int Amount { get; set; }
        public String ShoppingCartId { get; set; }
        [ForeignKey(nameof(ShoppingCartId))]
        public ShoppingCart ShoppingCart { get; set; }
    }
}
