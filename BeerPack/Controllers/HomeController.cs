using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult Beerbaskets()
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

        public ActionResult Search(string term)
        {
            (db as DbContext).Configuration.ProxyCreationEnabled = false;
            var results = db.Beers.Where(x => x.Name.Contains(term) || x.Description.Contains(term)).ToArray() ;
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        protected BeerPackEntities db = new BeerPackEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}