using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DessertShop.Models;
using DessertShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DessertShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICakeRepository _cakeRepository;
        public ShoppingCartController(IPieRepository pieRepository, ICakeRepository cakeRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _pieRepository = pieRepository;
            _cakeRepository = cakeRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
        [Authorize]
        public async Task<ViewResult> IndexAsync()
        {
            var shoppingCart = await _shoppingCartRepository.GetCartAsync();

            var items = _shoppingCartRepository.GetShoppingCartItems(shoppingCart);

            shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCart,
                ShoppingCartTotal = _shoppingCartRepository.GetShoppingCartTotal(shoppingCart)
            };

            ViewData["shoppingCart"] = shoppingCart;
            //ViewData["shoppingCartModel"] = shoppingCartViewModel;

            return View("Index", shoppingCartViewModel);
        }
        [Authorize]
        public async Task<RedirectToActionResult> AddToCartAsync(Guid id)
        {
            var shoppingCart = await _shoppingCartRepository.GetCartAsync();

            var pie = _pieRepository.GetPieById(id);
            var cake = _cakeRepository.GetCakeById(id);

            if (pie != null)
            {
                _shoppingCartRepository.AddToCart(pie, shoppingCart, 1);
            }
            else if (cake != null)
            {
                _shoppingCartRepository.AddToCart(cake, shoppingCart, 1);
            }
            return RedirectToAction("Index");
        }
    }
}
