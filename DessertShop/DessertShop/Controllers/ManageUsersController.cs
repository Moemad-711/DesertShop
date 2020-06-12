using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DessertShop.Models;
using DessertShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DessertShop.Controllers
{
    public class ManageUsersController : Controller
    {
        private readonly IManageUsers _manageUsers;

        public ManageUsersController(IManageUsers manageUsers)
        {
            _manageUsers = manageUsers;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _manageUsers.GetUsersInRoleAsync(Constants.AdministratorRole)).ToArray();

            var everyone =  _manageUsers.GetUsers().ToArray();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View("Index",model);
        }
    }
}
