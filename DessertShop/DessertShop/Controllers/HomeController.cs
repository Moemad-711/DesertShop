using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DessertShop.Models;
using DessertShop.ViewModels;

namespace DessertShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPieRepository _pieRepository;
        private readonly ICakeRepository _cakeRepository;

        public HomeController(ILogger<HomeController> logger, IPieRepository pieRepository, ICakeRepository cakeRepository)
        {
            _pieRepository = pieRepository;
            _logger = logger;
            _cakeRepository = cakeRepository;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewController = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOfTheWeek,
                CakesOfTheWeek = _cakeRepository.CakesOfTheWeek
            };

            return View("Index",homeViewController);
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
          
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
