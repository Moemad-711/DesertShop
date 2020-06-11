using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DessertShop.Models
{
    public class Pie
    {
        public Guid PieId { get; set; }
        public string PieName { get; set; }
        [NotMapped]
        public IFormFile PiePhotoName { get; set; }

        public string PiePhoto{ get; set; }
        public string ShortDescreption { get; set; }
        public string LongDescreption { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool PiesOfTheWeek { get; set; }
    }
}
