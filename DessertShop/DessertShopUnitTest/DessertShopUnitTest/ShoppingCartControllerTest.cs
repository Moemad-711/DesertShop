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
        public async Task ndexAsyncTestAsync()
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

            IdentityUser indentityUser = new IdentityUser
            {
                Id = userId
            };

            ShoppingCart shoppingCart = new ShoppingCart
            {
                ShoppingCartId = shoppingCartId,
                ShoppingCartItems = ShoppingCartItems,
                User = indentityUser

            };

            mockUserManger.Setup( um => um.FindByIdAsync(userId)).ReturnsAsync(indentityUser);
            mockShoppingCartRepository.Setup(sc => sc.GetCartAsync()).ReturnsAsync(shoppingCart);

            //act
            var result = await shoppingCartController.IndexAsync() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
            Assert.AreEqual(shoppingCart, result.ViewData["shoppingCart"]);

        }



    }
}