using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class AleController : Controller
    {

        public ActionResult List()
        {
            List<Ales> ales = new List<Ales>();
            ales.Add(new Ales
            {
                ID = 1,
                Name = "Shocktop",
                Price = 3,
                Description = "Belgian White Ale",
                Image = "/images/Ales/shocktop.jpg"
            });

            ales.Add(new Ales
            {
                ID = 2,
                Name = "Goose Island 312",
                Price = 3,
                Description = "Wheat Ale",
                Image = "/images/Ales/gooseisland312.png"
            });
            return View(ales);
        }
        // GET: Ale
        public ActionResult Index(int id)
        {
            var ales = new Models.Ales();
            if(id == 1)
            {
                ales.Name = "Shocktop";
                ales.Description = "Belgian White Ale";
                ales.Price = 3;
                ales.Image = "/images/Ales/shocktop.jpg";
            }
            else if (id == 2)
            {
                ales.Name = "Goose Island 312";
                ales.Description = "Wheat Ale";
                ales.Price = 3;
                ales.Image = "/images/Ales/gooseisland312.png";
            }else
            {
                return HttpNotFound("This product doesn't exist");
            }
            

            return View(ales);
        }
    }
}