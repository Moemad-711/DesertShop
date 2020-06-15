using NUnit.Framework;
using DessertShop;
using DessertShop.Controllers;
using DessertShop.Models;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DessertShopUnitTest
{
    [TestFixture]
    class OrderControllerTest
    {
        
            private Mock<IOrderRepository> _OrderRepository;
            private Mock<IShoppingCartRepository> _shoppingCartRepository;
            private OrderController _OrderController;

            [SetUp]
            public void Setup()
            {
                _OrderRepository = new Mock<IOrderRepository>();
                _shoppingCartRepository = new Mock<IShoppingCartRepository>();
                _OrderController = new OrderController(_OrderRepository.Object, _shoppingCartRepository.Object);

            }
            [Test]
            public void CheckoutTest()
            {
                var result = _OrderController.Checkout() as ViewResult;

                Assert.AreEqual("Checkout", result.ViewName);
            }
            [Test]
            public void CheckoutCompleteTest()
            {
                var result = _OrderController.CheckoutComplete() as ViewResult;

                Assert.AreEqual("CheckoutComplete", result.ViewName);
            }
            [Test]
            public async Task CheckoutAsyncTest()
            {
                //arrange 
                string expected = "CheckoutComplete";
                Order _order = new Order
                {
                    OrderId = 12345,
                    UserId= "User",
                };

           
                string shoppingCartId = Guid.NewGuid().ToString();

                List<ShoppingCartItem> ShoppingCartItems = new List<ShoppingCartItem>();

                for (int i = 0; i < 4; i++)
                {
                    ShoppingCartItems.Add(new ShoppingCartItem());
                }

                ShoppingCart shoppingCart = new ShoppingCart
                {
                    ShoppingCartId = shoppingCartId,
                    ShoppingCartItems = ShoppingCartItems,
                    User = new IdentityUser()

                };

            _shoppingCartRepository.Setup(mu => mu.GetCartAsync()).ReturnsAsync(shoppingCart);

                _shoppingCartRepository.Setup(mu => mu.GetShoppingCartItems()).Returns(ShoppingCartItems);
                //act
                var result = await _OrderController.CheckoutAsync(_order) as RedirectToActionResult;
            Assert.AreEqual(expected, result.ActionName);
        }
        [Test]
        public async Task CheckoutAsyncInvalidTest()
        {
            //arrange 
            string expected = "Checkout";
            Order invalidOrder = new Order
            {
                Email = "test"
            };
            _OrderController.ModelState.AddModelError("key", "error message");
            string shoppingCartId = Guid.NewGuid().ToString();

            List<ShoppingCartItem> ShoppingCartItems = new List<ShoppingCartItem>();

            for (int i = 0; i < 4; i++)
            {
                ShoppingCartItems.Add(new ShoppingCartItem());
            }

            ShoppingCart shoppingCart = new ShoppingCart
            {
                ShoppingCartId = shoppingCartId,
                ShoppingCartItems = ShoppingCartItems,
                User = new IdentityUser()

            };

            _shoppingCartRepository.Setup(mu => mu.GetCartAsync()).ReturnsAsync(shoppingCart);

            _shoppingCartRepository.Setup(mu => mu.GetShoppingCartItems()).Returns(ShoppingCartItems);
            //act
            var inValidResult = await _OrderController.CheckoutAsync(invalidOrder) as ViewResult;
            Assert.AreEqual(expected, inValidResult.ViewName);
        }
    }
}
