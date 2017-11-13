using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewData["Cart"] = Models.Cart.BuildCart(this.Request);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string firstName, string lastName,
            string yourEmail, int yourPhone, string yourComments)
        {
            return Content("Thanks for properly filling out this form, we will contact " +
                "you very shortly to help answer all of your questions and concerns.");
        }
    }
}