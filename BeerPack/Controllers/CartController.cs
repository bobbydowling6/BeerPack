using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BeerPack.Controllers
{
    public class CartController : Controller
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
        // GET: Cart
        public ActionResult Index()
        {
            Guid? cartID = this.GetCartID();

            return View(db.Carts.Find(cartID));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.Cart model)
        {
            var cart = db.Carts.Find(model.ID);
            for (int i = 0; i < model.CartProducts.Count; i++)
            {
                cart.CartProducts.ElementAt(i).Quantity = model.CartProducts.ElementAt(i).Quantity;
            }
            db.CartProducts.RemoveRange(cart.CartProducts.Where(x => x.Quantity == 0));
            db.SaveChanges();
            return View(cart);
        }
    }
}