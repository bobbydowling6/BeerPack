using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class LagerController : Controller
    {
        // GET: Lager
        public ActionResult List()
        {
            List<Lagers> lagers = new List<Lagers>();
            lagers.Add(new Lagers
            {
                ID = 1,
                Name = "Kona Brewing Co.",
                Price = 2,
                Description = "Longboard Island Lager",
                Image = "/images/Lagers/beer-longboard-lager.jpg"
            });

            lagers.Add(new Lagers
            {
                ID = 2,
                Name = "Guinness",
                Price = 2,
                Description = "Hop House 13 Lager",
                Image = "/images/Lagers/guinness-guinness-hop-house-13-lager_1478111845.png"
            });

            lagers.Add(new Lagers
            {
                ID = 3,
                Name = "Great Lakes Brewing Co.",
                Price = 2,
                Description = "Eliot Ness Amber Lager",
                Image = "/images/Lagers/Lager-Beer-Gear-Patrol-Great-Lakes.jpg"
            });

            lagers.Add(new Lagers
            {
                ID = 4,
                Name = "Peroni",
                Price = 2,
                Description = "Italian Style Lager",
                Image = "/images/Lagers/peroni.jpg"
            });

            lagers.Add(new Lagers
            {
                ID = 5,
                Name = "Boston Brewing Co.",
                Price = 2,
                Description = "Samual Adams Boston Lager",
                Image = "/images/Lagers/samual adams.jpg"
            });

            lagers.Add(new Lagers
            {
                ID = 6,
                Name = "Yuengling Brewing Co.",
                Price = 2,
                Description = "Traditional Lager",
                Image = "/images/Lagers/yuengling-lager-btl-single.png"
            });
            return View(lagers);
        }

        public ActionResult Index(int id)
        {
            var lagers = new Models.Lagers();
            if (id == 1)
            {
                lagers.Name = "Kona Brewing Co. ";
                lagers.Description = "Longboard Island Lager";
                lagers.Price = 3;
                lagers.Image = "/images/Lagers/beer-longboard-lager.jpg";
            }
            else if (id == 2)
            {
                lagers.Name = "Guinness";
                lagers.Description = "Hop House 13 Lager";
                lagers.Price = 3;
                lagers.Image = "/images/Lagers/guinness-guinness-hop-house-13-lager_1478111845.png";
            }
            else if (id == 3)
            {
                lagers.Name = "Great Lakes Brewing Co.";
                lagers.Description = "Eliot Ness Amber Lager";
                lagers.Price = 2;
                lagers.Image = "/images/Lagers/Lager-Beer-Gear-Patrol-Great-Lakes.jpg";
            }
            else if (id == 4)
            {
                lagers.Name = "Peroni";
                lagers.Description = "Italian Style Lager";
                lagers.Price = 2;
                lagers.Image = "/images/Lagers/peroni.jpg";
            }
            else if (id == 5)
            {
                lagers.Name = "Boston Brewing Co.";
                lagers.Description = "Boston Lager";
                lagers.Price = 2;
                lagers.Image = "/images/Lagers/samual adams.jpg";
            }
            else if (id == 6)
            {
                lagers.Name = "Yuengling Brewing Co.";
                lagers.Description = "Traditional Lager";
                lagers.Price = 2;
                lagers.Image = "/images/Lagers/yuengling-lager-btl-single.png";
            }
            else
            {
                return HttpNotFound("This product doesn't exist");
            }


            return View(lagers);
        }

        [HttpPost]
        public ActionResult Index(Lagers model)
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
