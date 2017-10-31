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
            return Content(id);
        }

        
    }
}