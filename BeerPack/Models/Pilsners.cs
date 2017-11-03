using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerPack.Models
{
    public class Pilsners
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "How many beers")]
        public int Quantity { get; set; }
    }
}