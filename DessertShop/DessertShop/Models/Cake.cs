using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace DessertShop.Models
{
    public class Cake
    {
        [Key]
        public Guid CakeId { get; set; }
        [StringLength(50)]
        public string CakeName { get; set; }
        [NotMapped]

        public IFormFile CakePhotoName { get; set; }
        [MaxLength(1000)]

        public string CakePhoto { get; set; }
        [StringLength(50)]
        public string ShortDescreption { get; set; }
        [StringLength(1000)]
        public string LongDescreption { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public bool CakesOfTheWeek { get; set; }
    }
}
