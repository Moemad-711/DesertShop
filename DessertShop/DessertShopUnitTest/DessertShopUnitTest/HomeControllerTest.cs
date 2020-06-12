using NUnit.Framework;
using DessertShop;
using DessertShop.Controllers;
using DessertShop.Models;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Logging;

namespace DessertShopUnitTest
{
    [TestFixture]
    class HomeControllerTest
    {
        private Mock<IPieRepository> _PieRepository;
        private Mock<ICakeRepository> _CakeRepository;
        private Mock <ILogger<HomeController>> _logger;
        private HomeController _HomeController;

        [SetUp]
        public void Setup()
        {
            _PieRepository = new Mock<IPieRepository>();
            _CakeRepository = new Mock<ICakeRepository>();
            _logger = new Mock<ILogger<HomeController>>();
            _HomeController = new HomeController(_logger.Object, _PieRepository.Object, _CakeRepository.Object);
        }
        [Test]
        public void IndexTest()
        {
            const string expected = "DessertShop.ViewModels.HomeViewModel";
            var result = _HomeController.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }
        [Test]
        public void PrivacyTest()
        {
            var result = _HomeController.Privacy() as ViewResult;
            Assert.AreEqual("Privacy", result.ViewName);
        }

    }
}
