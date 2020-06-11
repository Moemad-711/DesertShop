using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public Guid ShoppingCartItemId { get; set; }
        public StockItem stockitem { get; set; }
        public int Amount { get; set; }
        public String ShoppingCartId { get; set; }
    }
}
