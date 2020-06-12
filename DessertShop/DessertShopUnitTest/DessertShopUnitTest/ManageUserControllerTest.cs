using DessertShop.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DessertShopUnitTest
{
    class ManageUserControllerTest
    {
        private Mock<UserManager<IdentityUser>> mockUserManger;

        private ManageUsersController manageUsersController;

        [SetUp]
        public void Setup()
        {
            mockUserManger = new Mock<UserManager<IdentityUser>>(
                    new Mock<IUserStore<IdentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<IdentityUser>>().Object,
                    new IUserValidator<IdentityUser>[0],
                    new IPasswordValidator<IdentityUser>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<IdentityUser>>>().Object);
            manageUsersController = new ManageUsersController(mockUserManger.Object);
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

            List<IdentityUser> users = new List<IdentityUser>();
            users.Add(user);
            mockUserManger.Setup(um => um.CreateAsync(user, "Moemad@admin123"));
            mockUserManger.Setup(um => um.AddToRoleAsync(user, "Administrator"));
            mockUserManger.Setup(um => um.GetUsersInRoleAsync("Administrator")).ReturnsAsync(users);

            var result = await manageUsersController.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expected, result.Model.ToString());
        }
    }
}
