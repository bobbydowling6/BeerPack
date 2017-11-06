using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class PilsnerController : Controller
    {
        // GET: Lager
        public ActionResult List()
        {
            ViewBag.Cart = Models.Cart.BuildCart(this.Request);

            List<Pilsners> pilsners = new List<Pilsners>();
            pilsners.Add(new Pilsners
            {
                ID = 1,
                Name = "Brewing Revolution Brewing Co.",
                Price = 2,
                Description = "Rev Pils",
                Image = "/images/Pilsners/brewingrevolutionpils .jpg"
            });

            pilsners.Add(new Pilsners
            {
                ID = 2,
                Name = "FIRESTONE Walker Brewing Co.",
                Price = 2,
                Description = "FireStone Pils",
                Image = "/images/Pilsners/FirestonePils.jpg"
            });

            pilsners.Add(new Pilsners
            {
                ID = 3,
                Name = "Pilsner Urquell",
                Price = 2,
                Description = "The Original Pilsner Beer",
                Image = "/images/Pilsners/PilsnerUrquell.jpg"
            });

            pilsners.Add(new Pilsners
            {
                ID = 4,
                Name = "Scrimshaw",
                Price = 2,
                Description = "Pilsner Style Beer",
                Image = "/images/Pilsners/scrimshaw.jpg"
            });

            pilsners.Add(new Pilsners
            {
                ID = 5,
                Name = "Summit Brewing Co.",
                Price = 2,
                Description = "Keller Pils",
                Image = "/images/Pilsners/summit keller pils .jpg"
            });

            pilsners.Add(new Pilsners
            {
                ID = 6,
                Name = "Urban Chestnut Brewing Co.",
                Price = 2,
                Description = "Stammtisch German Style Pilsner",
                Image = "/images/Pilsners/urban chestnut stammtisch .jpg"
            });
            return View(pilsners);
        }

        public ActionResult Index(int id)
        {
            ViewBag.Cart = Models.Cart.BuildCart(this.Request);

            var pilsners = new Models.Pilsners();
            if (id == 1)
            {
                pilsners.Name = "Brewing Revolution Brewing Co.";
                pilsners.Description = "Rev Pils";
                pilsners.Price = 3;
                pilsners.Image = "/images/Pilsners/brewingrevolutionpils .jpg";
            }
            else if (id == 2)
            {
                pilsners.Name = "FIRESTONE Walker Brewing Co.";
                pilsners.Description = "Firestone Pils";
                pilsners.Price = 3;
                pilsners.Image = "/images/Pilsners/FirestonePils.jpg";
            }
            else if (id == 3)
            {
                pilsners.Name = "Pilsner Urquell";
                pilsners.Description = "The Original Pilsner Beer";
                pilsners.Price = 2;
                pilsners.Image = "/images/Pilsners/PilsnerUrquell.jpg";
            }
            else if (id == 4)
            {
                pilsners.Name = "Scrimshaw";
                pilsners.Description = "Pilsner Style Beer";
                pilsners.Price = 2;
                pilsners.Image = "/images/Pilsners/scrimshaw.jpg";
            }
            else if (id == 5)
            {
                pilsners.Name = "Summit Brewing Co.";
                pilsners.Description = "Keller Pils";
                pilsners.Price = 2;
                pilsners.Image = "/images/Pilsners/summit keller pils .jpg";
            }
            else if (id == 6)
            {
                pilsners.Name = "Urban Chestnut Brewing Co.";
                pilsners.Description = "Stammtisch German Style Pilsner";
                pilsners.Price = 2;
                pilsners.Image = "/images/Pilsners/urban chestnut stammtisch .jpg";
            }
            else
            {
                return HttpNotFound("This product doesn't exist");
            }


            return View(pilsners);
        }

        [HttpPost]
        public ActionResult Index(Lagers model)
        {
            ViewBag.Cart = Models.Cart.BuildCart(this.Request);

            HttpContext.Session.Add("productName", model.Name);
            HttpContext.Session.Add("productPrice", model.Price.ToString("C"));
            HttpContext.Session.Add("productQuantity", model.Quantity.ToString());

            Response.AppendCookie(new HttpCookie("productName", model.Name));
            Response.AppendCookie(new HttpCookie("productPrice", model.Price.ToString("C")));
            Response.AppendCookie(new HttpCookie("productQuantity", model.Quantity.ToString()));

            TempData.Add("NewItem", model.Name);

            return RedirectToAction("Index", "Cart");
        }
    }
}
