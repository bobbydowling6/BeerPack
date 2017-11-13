using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BeerPack.Controllers
{

    public class BeerController : Controller
    {
        BeerPack.Models.BeerPackEntities db = new Models.BeerPackEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Beer/List
        public ActionResult List()
        {

            return View(db.Beers);
        }

        // GET: Beer
        public ActionResult Index(int? id)
        {
            
            return View(db.Beers.Find(id));
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