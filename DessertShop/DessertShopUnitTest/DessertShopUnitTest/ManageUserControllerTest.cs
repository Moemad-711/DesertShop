using NUnit.Framework;
using DessertShop;
using DessertShop.Controllers;
using DessertShop.Models;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DessertShopUnitTest
{
    [TestFixture]
    class ManageUserControllerTest
    {
        private Mock<UserManager<IdentityUser>> _userManager;
        private ManageUsersController _ManageUsersController;

        [SetUp]
        public void Setup()
        {
            _userManager = new Mock<UserManager<IdentityUser>>();
            _ManageUsersController = new ManageUsersController(_userManager.Object);
        }
        [Test]
        public async Task Index()
        {
            const string expected = "DessertShop.ViewModels.ManageUsersViewModel";

            var result = await _ManageUsersController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }
    }
}
