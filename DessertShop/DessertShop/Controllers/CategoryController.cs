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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            DesertViewModel DesertViewModel = new DesertViewModel
            {
                Categories = _categoryRepository.Categories
            };
            return View("Index",DesertViewModel);
          
        }
        [Authorize(Roles = Constants.AdministratorRole)]
        public ViewResult AddCategory()
        {
            return View("AddCategory");
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public RedirectToActionResult AddCategory(Category category)
        {
            _categoryRepository.CreateCategory(category);
            return RedirectToAction("Index");
        }
    }
}
