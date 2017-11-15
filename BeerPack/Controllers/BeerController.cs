using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BeerPack.Controllers
{

    public class BeerController : Controller
    {
        protected BeerPackEntities db = new BeerPackEntities();

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

        [HttpPost]
        public ActionResult Index(Beer model)
        {
            //TODO: Save the posted information to a database!
            Guid cartID;
            Cart cart = null;
            if (Request.Cookies.AllKeys.Contains("cartID"))
            {

                cartID = Guid.Parse(Request.Cookies["cartID"].Value);
                cart = db.Carts.Find(cartID);
            }
            if (cart == null)
            {
                cartID = Guid.NewGuid();
                cart = new Cart
                {
                    ID = cartID,
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow
                };
                db.Carts.Add(cart);
                Response.AppendCookie(new HttpCookie("cartID", cartID.ToString()));
            }

            CartProduct product = cart.CartProducts.FirstOrDefault(x => x.ProductID == model.BeerID);
            if (product == null)
            {
                product = new CartProduct
                {
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow,
                    ProductID = model.BeerID,
                    Quantity = 0
                };
                cart.CartProducts.Add(product);
            }

            product.Quantity += model.Quantity ?? 1;
            product.DateLastModified = DateTime.UtcNow;
            cart.DateLastModified = DateTime.UtcNow;

            db.SaveChanges();


            TempData.Add("NewItem", model.Name);

            //TODO: build up the cart controller!
            return RedirectToAction("Index", "Cart");

        }

    }
}