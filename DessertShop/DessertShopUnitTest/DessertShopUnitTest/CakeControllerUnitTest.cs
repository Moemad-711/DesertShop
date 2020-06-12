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
    [TestFixture]
    class CakeControllerUnitTest
    {
        private Mock<ICakeRepository> _CakeRepository;
        private Mock<ICategoryRepository> _CategoryRepository;
        private Mock<IWebHostEnvironment> _IWebHostEnvironment;
        private CakeController _CakeController;

        [SetUp]
        public void Setup()
        {
            _CakeRepository = new Mock<ICakeRepository>();
            _CategoryRepository = new Mock<ICategoryRepository>();
            _IWebHostEnvironment = new Mock<IWebHostEnvironment>();

            _CakeController = new CakeController(_CakeRepository.Object, _CategoryRepository.Object, _IWebHostEnvironment.Object);
        }
        [Test]
        public void IndexTest()
        {
            const string expected = "DessertShop.ViewModels.DesertViewModel";

            var result = _CakeController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }
        [Test]
        public void AddPieTest()
        {
            const string expected = "DessertShop.ViewModels.DesertViewModel";

            var result = _CakeController.AddCake() as ViewResult;

            Assert.AreEqual("AddCake", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }

        [Test]
        public void AddPiePostTest()
        {
            const string firstExpected = "Index";
            const string secondExpected = "AddCake";

            var _cake = new Cake
            {
                CakeId = Guid.NewGuid(),
                CakeName = "Chocolate cake",
                Price = 10,
                ShortDescreption = "UnitTestCake",
                LongDescreption = "UnitTestCake",
                CakesOfTheWeek = true
            };

            var firstResult = _CakeController.AddCake(_cake) as RedirectToActionResult;
            var secondResult = _CakeController.AddCake(null) as RedirectToActionResult;

            Assert.AreEqual(firstExpected, firstResult.ActionName);
            Assert.AreEqual(secondExpected, secondResult.ActionName);
        }

        [Test]
        public void RemoveCakeTest()
        {

            var CakeId = Guid.NewGuid();
            //Arrange
            var _cake = new Cake
            {
                CakeId = Guid.NewGuid(),
                CakeName = "Chocolate cake",
                Price = 10,
                ShortDescreption = "UnitTestCake",
                LongDescreption = "UnitTestCake",
                CakesOfTheWeek = true
            };

            //this tells the mock repository when an invoke of GetCakeByID with CakeID return _pie
            _CakeRepository.Setup(expression: p => p.GetCakeById(CakeId)).Returns(_cake);

            const string firstExpected = "Index";
            const string secondExpected = "NotFoundAction";

            //Act
            var firstResult = _CakeController.RemoveCake(CakeId) as RedirectToActionResult;
            var secondResult = _CakeController.RemoveCake(Guid.NewGuid()) as RedirectToActionResult;

            //Assert
            Assert.AreEqual(firstExpected, firstResult.ActionName);
            Assert.AreEqual(secondExpected, secondResult.ActionName);
        }

        [Test]
        public void EditCakeTest()
        {

            var CakeId = Guid.NewGuid();
            //Arrange
            var _cake = new Cake
            {
                CakeId = Guid.NewGuid(),
                CakeName = "Chocolate cake ",
                Price = 10,
                ShortDescreption = "UnitTestCake",
                LongDescreption = "UnitTestCake",
                CakesOfTheWeek = true
            };

            //this tells the mock repository when an invoke of GetPieByID with PieID return _pie
            _CakeRepository.Setup(expression: p => p.GetCakeById(CakeId)).Returns(_cake);

            const string firstExpected = "EditCake";
            //const string secondExpected = "NotFoundAction";

            //Act
            var firstResult = _CakeController.EditCake(CakeId) as ViewResult;
            //var secondResult = _CakeController.EditCake(Guid.NewGuid()) as ActionResult;

            //Assert
            Assert.AreEqual(firstExpected, firstResult.ViewName);
            //Assert.AreEqual(secondExpected, secondResult.);
        }
    }
}
