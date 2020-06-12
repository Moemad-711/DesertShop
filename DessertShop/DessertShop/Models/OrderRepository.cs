using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace DessertShop.Models
{
        public class OrderRepository : IOrderRepository
        {
            private readonly AppDbContext _appDbContext;
            private readonly IShoppingCartRepository _shoppingCartRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public OrderRepository(AppDbContext appDbContext, IShoppingCartRepository shoppingCartRepository, IHttpContextAccessor httpContextAccessor)
            {
                _appDbContext = appDbContext;
                _shoppingCartRepository = shoppingCartRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task CreateOrderAsync(Order order)
            {
                var shoppingCart = await _shoppingCartRepository.GetCartAsync();

                order.OrderPlaced = DateTime.Now;


                var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                order.UserId = currentUserId;

                var shoppingCartItems = shoppingCart.ShoppingCartItems;
                order.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();

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

