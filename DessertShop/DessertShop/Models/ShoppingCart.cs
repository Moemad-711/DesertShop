using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DessertShop.Models
{
    public class ShoppingCart
    {
        [Key]
        public string ShoppingCartId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }     
    }


}
