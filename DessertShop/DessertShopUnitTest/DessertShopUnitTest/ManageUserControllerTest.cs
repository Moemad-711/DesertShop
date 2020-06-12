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
    class ManageUserControllerTest
    {
        private Mock<UserManager<IdentityUser>> mockUserManger;
        private ManageUsersController _ManageUsersController;

        [SetUp]
        public void Setup()
        {
            mockUserManger = new Mock<UserManager<IdentityUser>>();
            _ManageUsersController = new ManageUsersController(mockUserManger.Object);
        }
        [Test]
        public async Task Index()
        {
            const string expected = "DessertShop.ViewModels.ManageUsersViewModel";

            var result = await _ManageUsersController.Index();

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }
    }
}
