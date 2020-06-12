using NUnit.Framework;
using DessertShop;
using DessertShop.Controllers;
using DessertShop.Models;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace DessertShopUnitTest
{
    class ShoppingCartControllerTest
    {
        private Mock<IPieRepository> mockPieRepository;
        private Mock<ICakeRepository> mockCakeRepository;
        private Mock<IShoppingCartRepository> mockShoppingCartRepository;


        private ShoppingCartController shoppingCartController;

        [SetUp]
        public void Setup()
        {
            mockPieRepository = new Mock<IPieRepository>();
            mockShoppingCartRepository = new Mock<IShoppingCartRepository>();
            mockCakeRepository = new Mock<ICakeRepository>();

            shoppingCartController = new ShoppingCartController(mockPieRepository.Object, mockCakeRepository.Object, mockShoppingCartRepository.Object);
        }



    }
}