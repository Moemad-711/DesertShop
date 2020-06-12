using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public class ManageUsers : IManageUsers
    {
        private UserManager<IdentityUser> _userManager;
        public ManageUsers(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<IdentityUser> GetUsers() => _userManager.Users.ToList();

        public async Task<List<IdentityUser>> GetUsersInRoleAsync(string Role) 
        {
            return (await _userManager.GetUsersInRoleAsync(Role)).ToList();
        }
    }
}
