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
                Price = 2,
                Description = "Belgian White Ale",
                Image = "/images/Ales/shocktop.jpg"
            });

            ales.Add(new Ales
            {
                ID = 2,
                Name = "Goose Island 312",
                Price = 2,
                Description = "Wheat Ale",
                Image = "/images/Ales/gooseisland312.png"
            });

            ales.Add(new Ales
            {
                ID = 3,
                Name = "Samual Adams Summer Ale",
                Price = 2,
                Description = "Summer Ale",
                Image = "/images/Ales/beer_171.jpg"
            });

            ales.Add(new Ales
            {
                ID = 4,
                Name = "Bells Best Brown Ale",
                Price = 2,
                Description = "Brown Ale",
                Image = "/images/Ales/bells-best-brown-ale.jpg"
            });

            ales.Add(new Ales
            {
                ID = 5,
                Name = "Kona Brewing Co. Big Wave",
                Price = 2,
                Description = "Golden Ale",
                Image = "/images/Ales/kona-beer.png"
            });

            ales.Add(new Ales
            {
                ID = 6,
                Name = "Shiner Bock Birthday Beer",
                Price = 2,
                Description = "Coffee Ale",
                Image = "/images/Ales/SHiner-793x525.png"
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
            }
            else if (id == 3)
            {
                ales.Name = "Samual Adams Summer Ale";
                ales.Description = "Summer Ale";
                ales.Price = 2;
                ales.Image = "/images/Ales/beer_171.jpg"; 
            }
            else if (id == 4)
            {
                ales.Name = "Bells Best Brown Ale";
                ales.Description = "Brown Ale";
                ales.Price = 2;
                ales.Image = "/images/Ales/bells-best-brown-ale.jpg";
            }
            else if (id == 5)
            {
                ales.Name = "Kona Brewing Co. Big Wave";
                ales.Description = "Golden Ale";
                ales.Price = 2;
                ales.Image = "/images/Ales/kona-beer.png";
            }
            else if (id == 6)
            {
                ales.Name = "Shiner Bock Birthday Beer";
                ales.Description = "Coffee Ale";
                ales.Price = 2;
                ales.Image = "/images/Ales/SHiner-793x525.png";
            }
            else
            {
                return HttpNotFound("This product doesn't exist");
            }
            

            return View(ales);
        }

        [HttpPost]
        public ActionResult Index(Ales model)
        {
            HttpContext.Session.Add("productName", model.Name);
            HttpContext.Session.Add("productPrice", model.Price.ToString("C"));
            HttpContext.Session.Add("productQuantity", model.Quantity.ToString());

            Response.AppendCookie(new HttpCookie("productName", model.Name));
            Response.AppendCookie(new HttpCookie("productPrice", model.Price.ToString("C")));
            Response.AppendCookie(new HttpCookie("productQuantity", model.Quantity.ToString()));

            return RedirectToAction("Index", "Cart");
        }
    }
}