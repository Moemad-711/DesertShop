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
    class PieControllerUnitTest
    {
        private Mock<IPieRepository> _PieRepository;
        private Mock<ICategoryRepository> _CategoryRepository;
        private Mock<IWebHostEnvironment> _IWebHostEnvironment;
        private PieController _PieController;

        [SetUp]
        public void Setup()
        {
            _PieRepository = new Mock<IPieRepository>();
            _CategoryRepository = new Mock<ICategoryRepository>();
            _IWebHostEnvironment = new Mock<IWebHostEnvironment>();

            _PieController = new PieController(_PieRepository.Object, _CategoryRepository.Object, _IWebHostEnvironment.Object);            
        }

        [Test]
        public void IndexTest()
        {
            const string expected = "DessertShop.ViewModels.DesertViewModel";

            var result = _PieController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }

        [Test]
        public void AddPieTest()
        {
            const string expected = "DessertShop.ViewModels.DesertViewModel";

            var result = _PieController.AddPie() as ViewResult;

            Assert.AreEqual("AddPie", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }

        [Test]
        public void AddPiePostTest()
        {
            const string firstExpected = "Index";
            const string secondExpected = "AddPie";

            var _pie = new Pie
            {
                PieId = Guid.NewGuid(),
                PieName = "Apple Pie",
                Price = 10,
                ShortDescreption = "UnitTestPie",
                LongDescreption = "UnitTestPie",
                PiesOfTheWeek = true
            };

            var firstResult = _PieController.AddPie(_pie) as RedirectToActionResult;
            var secondResult = _PieController.AddPie(null) as RedirectToActionResult;

            Assert.AreEqual(firstExpected, firstResult.ActionName);
            Assert.AreEqual(secondExpected, secondResult.ActionName);
        }

        [Test]
        public void RemovePieTest()
        {
            
            var PieId = Guid.NewGuid();
            //Arrange
            var _pie = new Pie
            {
                PieId = PieId,
                PieName = "Apple Pie",
                Price = 10,
                ShortDescreption = "UnitTestPie",
                LongDescreption = "UnitTestPie",
                PiesOfTheWeek = true
            };

            //this tells the mock repository when an invoke of GetPieByID with PieID return _pie
            _PieRepository.Setup(expression: p => p.GetPieById(PieId)).Returns(_pie); 

            const string firstExpected = "Index";
            const string secondExpected = "NotFoundAction";

            //Act
            var firstResult = _PieController.RemovePie(PieId) as RedirectToActionResult;
            var secondResult = _PieController.RemovePie(Guid.NewGuid()) as RedirectToActionResult;

            //Assert
            Assert.AreEqual(firstExpected, firstResult.ActionName);
            Assert.AreEqual(secondExpected, secondResult.ActionName);
        }

        [Test]
        public void EditPieTest()
        {

            var PieId = Guid.NewGuid();
            //Arrange
            var _pie = new Pie
            {
                PieId = PieId,
                PieName = "Apple Pie",
                Price = 10,
                ShortDescreption = "UnitTestPie",
                LongDescreption = "UnitTestPie",
                PiesOfTheWeek = true
            };

            //this tells the mock repository when an invoke of GetPieByID with PieID return _pie
            _PieRepository.Setup(expression: p => p.GetPieById(PieId)).Returns(_pie);

            const string firstExpected = "EditPie";
            const string secondExpected = "NotFoundAction";

            //Act
            var firstResult = _PieController.EditPie(PieId) as ViewResult;
            //var secondResult = _PieController.EditPie(Guid.NewGuid()) as ActionResult;

            //Assert
            Assert.AreEqual(firstExpected, firstResult.ViewName);
            //Assert.AreEqual(secondExpected, secondResult.);
        }



    }   
}
