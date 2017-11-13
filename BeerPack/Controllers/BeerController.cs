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
        public ActionResult List(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(db.Beers);
            }
            return View(db.Beers.Where(x => x.Beer_Style == id));
        }

        // GET: Beer
        public ActionResult Index(int? id)
        {
            
            return View(db.Beers.Find(id));
        }

    }
}