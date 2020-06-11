using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace DessertShop.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStockItemRepository stockItemRepository;

        [Key]
        public string ShoppingCartId { get; set; }

        public string userId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(AppDbContext appDbContext, IStockItemRepository stockItemRepository)
        {
            _appDbContext = appDbContext;
            this.stockItemRepository = stockItemRepository;
        }
        private ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var httpContext = services.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var context = services.GetService<AppDbContext>();

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var stockItemRepo = services.GetService<IStockItemRepository>();

            //string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            //string cartId = session.GetString("CartId");
            var shoppingCart = context.ShoppingCarts.FirstOrDefault(s => s.userId == currentUserId);
            if (shoppingCart != null)
            {
                string cartId = shoppingCart.ShoppingCartId;
            }
            else
            {

                shoppingCart = new ShoppingCart(context, stockItemRepo)
                {
                    ShoppingCartId = Guid.NewGuid().ToString(),
                    userId = currentUserId
                };
                context.ShoppingCarts.Add(shoppingCart);
                context.SaveChanges();
            }

            session.SetString("CartId", shoppingCart.ShoppingCartId);

            return new ShoppingCart(context, stockItemRepo) { ShoppingCartId = shoppingCart.ShoppingCartId };
        }

        public void AddToCart(Pie pie, int amount)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.stockitem.name == pie.PieName && s.ShoppingCartId == ShoppingCartId);

            var stockItem = stockItemRepository.GetStockItemByName(pie.PieName);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    stockitem = stockItem,
                    Amount = 1
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public void AddToCart(Cake cake, int amount)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.stockitem.name == cake.CakeName && s.ShoppingCartId == ShoppingCartId);



            var stockItem = stockItemRepository.GetStockItemByName(cake.CakeName);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    stockitem = stockItem,
                    Amount = 1
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }
        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.stockitem.name == pie.PieName && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.stockitem)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.stockitem.Price * c.Amount).Sum();
            return total;
        }
    }


}
