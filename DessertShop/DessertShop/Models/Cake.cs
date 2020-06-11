using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DessertShop.Models
{
    public class Cake
    {
        [Key]
        public Guid CakeId { get; set; }
        public string CakeName { get; set; }
        [NotMapped]
        public IFormFile CakePhotoName { get; set; }

        public string CakePhoto { get; set; }
        public string ShortDescreption { get; set; }
        public string LongDescreption { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool CakesOfTheWeek { get; set; }
    }
}
