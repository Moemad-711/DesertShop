using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public interface IShoppingCartRepository
    {
        public  Task<ShoppingCart> GetCartAsync();

        public void AddToCart(Pie pie, int amount);
        public void AddToCart(Cake cake, int amount);
        public int RemoveFromCart(Pie pie);
        public List<ShoppingCartItem> GetShoppingCartItems();
        public void ClearCart();
        public decimal GetShoppingCartTotal();

    }

}
