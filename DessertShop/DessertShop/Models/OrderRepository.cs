using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
        public class OrderRepository : IOrderRepository
        {
            private readonly AppDbContext _appDbContext;
            private readonly ShoppingCart _shoppingCart;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart, IHttpContextAccessor httpContextAccessor)
            {
                _appDbContext = appDbContext;
                _shoppingCart = shoppingCart;
                _httpContextAccessor = httpContextAccessor;
            }

            public void CreateOrder(Order order)
            {

                order.OrderPlaced = DateTime.Now;


                var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                order.UserId = currentUserId;

                var shoppingCartItems = _shoppingCart.ShoppingCartItems;
                order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

                order.OrderDetails = new List<OrderDetail>();
                //adding the order with its details

                foreach (var shoppingCartItem in shoppingCartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        Amount = shoppingCartItem.Amount,
                        stockitemId = shoppingCartItem.stockitem.id,
                        Price = shoppingCartItem.stockitem.Price
                    };

                    order.OrderDetails.Add(orderDetail);
                }

                _appDbContext.Orders.Add(order);

                _appDbContext.SaveChanges();
            }
        }
}

