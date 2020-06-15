using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStockItemRepository stockItemRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public ShoppingCartRepository(AppDbContext appDbContext, IStockItemRepository stockItemRepository, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _appDbContext = appDbContext;
            this.stockItemRepository = stockItemRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<ShoppingCart> GetCartAsync()
        {
            ISession session = httpContextAccessor.HttpContext.Session;

            var httpContext = httpContextAccessor.HttpContext;

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.UserId == currentUserId);

            if (shoppingCart != null)
            {
                string cartId = shoppingCart.ShoppingCartId;
                session.SetString("CartId", shoppingCart.ShoppingCartId);
                return shoppingCart;

            }
            else
            {

                shoppingCart = new ShoppingCart
                {
                    ShoppingCartId = Guid.NewGuid().ToString(),
                    User =  await userManager.FindByIdAsync(currentUserId),
                    UserId = currentUserId
                };
                _appDbContext.ShoppingCarts.Add(shoppingCart);
                _appDbContext.SaveChanges();
                session.SetString("CartId", shoppingCart.ShoppingCartId);
                return shoppingCart;
            }


        }

        public void AddToCart(Pie pie, int amount)
        {
            var httpContext = httpContextAccessor.HttpContext;

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.User.Id == currentUserId);

            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.stockitem.name == pie.PieName && s.ShoppingCartId == shoppingCart.ShoppingCartId);

            var stockItem = stockItemRepository.GetStockItemByName(pie.PieName);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = shoppingCart.ShoppingCartId,
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
            var httpContext = httpContextAccessor.HttpContext;

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.User.Id == currentUserId);

            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.stockitem.name == cake.CakeName && s.ShoppingCartId == shoppingCart.ShoppingCartId);



            var stockItem = stockItemRepository.GetStockItemByName(cake.CakeName);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = shoppingCart.ShoppingCartId,
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
            var httpContext = httpContextAccessor.HttpContext;

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.User.Id == currentUserId);

            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.stockitem.name == pie.PieName && s.ShoppingCartId == shoppingCart.ShoppingCartId);

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
            var httpContext = httpContextAccessor.HttpContext;

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.User.Id == currentUserId);

            return shoppingCart.ShoppingCartItems ??
                   (shoppingCart.ShoppingCartItems =
                       _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == shoppingCart.ShoppingCartId)
                           .Include(s => s.stockitem)
                           .ToList());
        }

        public void ClearCart()
        {
            var httpContext = httpContextAccessor.HttpContext;

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.User.Id == currentUserId);

            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == shoppingCart.ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var httpContext = httpContextAccessor.HttpContext;

            var currentUserId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var shoppingCart = _appDbContext.ShoppingCarts.FirstOrDefault(s => s.User.Id == currentUserId);

            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == shoppingCart.ShoppingCartId)
                .Select(c => c.stockitem.Price * c.Amount).Sum();
            return total;
        }
    }
}
