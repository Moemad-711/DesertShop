using DessertShop.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DessertShop.Models;

namespace DessertShopUnitTest
{
    class ManageUserControllerTest
    {
        private Mock<IManageUsers> mockManageUsers;
        private ManageUsersController manageUsersController;

        [SetUp]
        public void Setup()
        {
            mockManageUsers = new Mock<IManageUsers>();
            manageUsersController = new ManageUsersController(mockManageUsers.Object);
        }
        [Test]
        public async Task Index()
        {
            const string expected = "DessertShop.ViewModels.ManageUsersViewModel";

            var userId = Guid.NewGuid().ToString();

            IdentityUser user = new IdentityUser
            {
                Id = userId
            };


            List<IdentityUser> admins = new List<IdentityUser>();
            admins.Add(user);

            List<IdentityUser> users = new List<IdentityUser>();
            users.Add(user);
            users.Add(new IdentityUser());
            users.Add(new IdentityUser()); users.Add(new IdentityUser());

            mockManageUsers.Setup(mu => mu.GetUsers()).Returns(users);

            mockManageUsers.Setup(mu => mu.GetUsersInRoleAsync(Constants.AdministratorRole)).ReturnsAsync(admins);

            var result = await manageUsersController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }
    }
}
