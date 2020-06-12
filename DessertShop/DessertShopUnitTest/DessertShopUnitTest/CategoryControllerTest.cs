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
    class CategoryControllerTest
    {
        private Mock<ICategoryRepository> _CategoryRepository;

        private CategoryController _CategoryController;

        [SetUp]
        public void Setup()
        {

            _CategoryRepository = new Mock<ICategoryRepository>();
            _CategoryController = new CategoryController(_CategoryRepository.Object);

        }

        [Test]
        public void IndexTest()
        {
            const string expected = "DessertShop.ViewModels.DesertViewModel";

            var result = _CategoryController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }
        [Test]
        public void AddCategoryTest()
        {

            var result = _CategoryController.AddCategory() as ViewResult;

            Assert.AreEqual("AddCategory", result.ViewName);
        }

        [Test]
        public void AddCategoryPostTest()
        {
            var _category = new Category
            {
                CategoryId=12345 ,
                CategoryName="categorey"
            };
            var result = _CategoryController.AddCategory(_category) as RedirectToActionResult;

            Assert.AreEqual("Index", result.ActionName);
        }
    }
}

