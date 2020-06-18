﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DessertShop.Models
{
    public class StockItem
    {
        [Key]
        public Guid id { get; set; }
        [StringLength(50)]
        public string name { get; set; }
        [StringLength(5)]
        public decimal Price { get; set; }
    }
}
