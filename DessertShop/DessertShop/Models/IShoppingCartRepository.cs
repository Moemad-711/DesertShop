using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public interface IShoppingCartRepository
    {
        public  Task<ShoppingCart> GetCartAsync();

        public void AddToCart(Pie pie, ShoppingCart shoppingCart, int amount);
        public void AddToCart(Cake cake, ShoppingCart shoppingCart, int amount);
        public int RemoveFromCart(Pie pie, ShoppingCart shoppingCart);
        public List<ShoppingCartItem> GetShoppingCartItems(ShoppingCart shoppingCarts);
        public void ClearCart(ShoppingCart shoppingCart);
        public decimal GetShoppingCartTotal(ShoppingCart shoppingCart);

    }

}
