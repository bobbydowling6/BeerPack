using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerPack.Models
{
    public class Ales
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}