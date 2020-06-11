using DessertShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DessertShop.ViewModels
{
    public class DesertViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Pie Pie { get; set; }
        public IEnumerable<Pie> Pies { get; set; }
        public Cake Cake { get; set; }
        public IEnumerable<Cake> Cakes { get; set; }
    }
}
