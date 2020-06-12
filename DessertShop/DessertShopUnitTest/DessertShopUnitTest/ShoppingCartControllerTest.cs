﻿using NUnit.Framework;
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
    class ShoppingCartControllerTest
    {
        private Mock<IPieRepository> mockPieRepository;
        private Mock<ICakeRepository> mockCakeRepository;
        private Mock<IShoppingCartRepository> mockShoppingCartRepository;
        private Mock<UserManager<IdentityUser>> mockUserManger;


        private ShoppingCartController shoppingCartController;

        [SetUp]
        public void Setup()
        {
            mockPieRepository = new Mock<IPieRepository>();
            mockShoppingCartRepository = new Mock<IShoppingCartRepository>();
            mockCakeRepository = new Mock<ICakeRepository>();
            mockUserManger = new Mock<UserManager<IdentityUser>>();
            shoppingCartController = new ShoppingCartController(mockPieRepository.Object, mockCakeRepository.Object, mockShoppingCartRepository.Object);
        }

        [Test]
        public async Task IndexAsyncTestAsync()
        {
            //arrange 
            string expected = "Index";

            string shoppingCartId = Guid.NewGuid().ToString();
            string userId = Guid.NewGuid().ToString();

            List<ShoppingCartItem> ShoppingCartItems =  new List<ShoppingCartItem>();

            for(int i = 0; i<4; i++)
            {
                ShoppingCartItems.Add(new ShoppingCartItem());
            }

            ShoppingCart shoppingCart = new ShoppingCart
            {
                ShoppingCartId = shoppingCartId,
                ShoppingCartItems = ShoppingCartItems,
                User = new IdentityUser()

            };

            mockShoppingCartRepository.Setup(sc => sc.GetCartAsync()).ReturnsAsync(shoppingCart);

            //act
            var result = await shoppingCartController.IndexAsync() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
            Assert.AreEqual(shoppingCart, result.ViewData["shoppingCart"]);
        }

        [Test]
        public async Task AddToCartAsyncTestAsync()
        {
            //arrange 
            string expected = "Index";

            string shoppingCartId = Guid.NewGuid().ToString();
            Guid cakeId = Guid.NewGuid();
            Guid pieId = Guid.NewGuid();

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

            Cake cake = new Cake
            {
                CakeId = cakeId
            };

            Pie pie = new Pie
            {
                PieId = pieId
            };

            mockShoppingCartRepository.Setup(sc => sc.GetCartAsync()).ReturnsAsync(shoppingCart);

            mockCakeRepository.Setup(c => c.GetCakeById(cakeId)).Returns(cake);

            mockPieRepository.Setup(c => c.GetPieById(pieId)).Returns(pie);
            //act
            var result = await shoppingCartController.AddToCartAsync(pieId) as RedirectToActionResult;

            Assert.AreEqual(expected, result.ActionName);

        }

    }
}