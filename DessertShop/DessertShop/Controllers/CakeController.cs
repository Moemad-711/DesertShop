using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DessertShop.Models;
using DessertShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DessertShop.Controllers
{
    public class CakeController : Controller
    {
        private readonly ICakeRepository _CakeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CakeController(ICakeRepository CakeRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _CakeRepository = CakeRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            DesertViewModel CakeViewModel = new DesertViewModel
            {
                Cakes = _CakeRepository.AllCakes
            };
            return View(CakeViewModel);
        }
        [Authorize(Roles = Constants.AdministratorRole)]
        public ViewResult AddCake()
        {
            DesertViewModel CakeViewModel = new DesertViewModel
            {
                Categories = _categoryRepository.Categories
            };
            return View(CakeViewModel);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public RedirectToActionResult AddCake(Cake cake)
        {
            if (cake == null)
                return RedirectToAction("AddCake");
            else
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadedFile(cake);
                    cake.CakePhoto = uniqueFileName;
                    _CakeRepository.CreateCake(cake);
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = Constants.AdministratorRole)]
        private string UploadedFile(Cake model)
        {
            string uniqueFileName = null;

            if (model.CakePhotoName != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CakePhotoName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CakePhotoName.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        [Authorize(Roles = Constants.AdministratorRole)]
        public RedirectToActionResult RemoveCake(Guid id)
        {
            var cake = _CakeRepository.GetCakeById(id);
            _CakeRepository.RemoveCake(cake);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public ViewResult EditCake(Guid id)
        {
            var cake = _CakeRepository.GetCakeById(id);
            DesertViewModel CakeViewModel = new DesertViewModel
            {
                Cake = cake,
                Categories = _categoryRepository.Categories
            };
            return View(CakeViewModel);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public RedirectToActionResult EditCake(Cake cake)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(cake);
                if (uniqueFileName != null) // if the user didn't change the photo, we won't assign a null to the photo name, we instead sent the photo name as hidden parameter, if a value is sent , the value would replace the hidden parameter
                    cake.CakePhoto = uniqueFileName;

                var the_cake = _CakeRepository.GetCakeById(cake.CakeId);
                if (the_cake != null)
                    _CakeRepository.EditCake(the_cake, cake);
            }
            return RedirectToAction("Index");
        }
        public IActionResult MakeCakeOfTheWeek(Guid id)
        {
            var cake = _CakeRepository.GetCakeById(id);
            _CakeRepository.MakeCakeOfTheWeek(cake);
            return RedirectToAction("Index");
        }
        public IActionResult Details(Guid id)
        {
            var cake = _CakeRepository.GetCakeById(id);
            if (cake == null)
                return NotFound();

            return View(cake);
        }
    }
}
