using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DessertShop.Models;
using DessertShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DessertShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            DesertViewModel pieViewModel = new DesertViewModel
            {
                Pies = _pieRepository.AllPies
            };

            return View("Index",pieViewModel);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public ViewResult AddPie()
        {
            DesertViewModel pieViewModel = new DesertViewModel
            {
                Categories = _categoryRepository.Categories
            };
            return View("AddPie",pieViewModel);
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public RedirectToActionResult AddPie(Pie pie)
        {
            if (pie == null)
                return RedirectToAction("AddPie");
            else
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadedFile(pie);
                    pie.PiePhoto = uniqueFileName;
                    _pieRepository.CreatePie(pie);
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        private string UploadedFile(Pie model)
        {
            string uniqueFileName = null;

            if (model.PiePhotoName != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PiePhotoName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PiePhotoName.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public RedirectToActionResult RemovePie(Guid id)
        {
            
            var pie = _pieRepository.GetPieById(id);

            
            if (pie != null)
            {
                _pieRepository.RemovePie(pie);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotFoundAction");
            }
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        public ViewResult EditPie(Guid id)
        {
            var pie = _pieRepository.GetPieById(id);
            
            {
                DesertViewModel pieViewModel = new DesertViewModel
                {
                    Pie = pie,
                    Categories = _categoryRepository.Categories
                };
                return View("EditPie", pieViewModel);
            }
        }

        [Authorize(Roles = Constants.AdministratorRole)]
        [HttpPost]
        public RedirectToActionResult EditPie(Pie pie)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(pie);
                if (uniqueFileName != null) // if the user didn't change the photo, we won't assign a null to the photo name, we instead sent the photo name as hidden parameter, if a value is sent , the value would replace the hidden parameter
                    pie.PiePhoto = uniqueFileName;

                var the_pie = _pieRepository.GetPieById(pie.PieId);
                if (the_pie != null)
                    _pieRepository.EditPie(the_pie, pie);
                else
                    return RedirectToAction("NotFoundAction");
            }
            return RedirectToAction("Index");
        }

        public IActionResult MakePieOfTheWeek(Guid id)
        {
            
            var pie = _pieRepository.GetPieById(id);
            if(pie == null)
            {
                return RedirectToAction("NotFoundAction");
            }
            _pieRepository.MakePieOfTheWeek(pie);
            return RedirectToAction("Index");
        }
        public IActionResult Details(Guid id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return RedirectToAction("NotFoundAction");

            return View("Details", pie);
        }

        private IActionResult NotFoundAction()
        {
            return NotFound();
        }

        
        
    }
}
