using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.Models
{
    public interface IManageUsers
    {
        public List<IdentityUser> GetUsers();
        public Task<List<IdentityUser>> GetUsersInRoleAsync(String Role);
    }
}
