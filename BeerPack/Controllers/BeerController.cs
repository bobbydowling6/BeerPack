using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{

    public class BeerController : Controller
    {
        // GET: Beer
        public ActionResult Index(string id)
        {
            var model = new BeerPack.Models.Ales();
            return Content(id);
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