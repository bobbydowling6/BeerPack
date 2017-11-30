using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class ProductController : Controller
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

        // GET: Product/List
        public async Task<ActionResult> List(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(db.Beers);
            }
            else
            {
                var cat = await db.Categories.FindAsync(id);
                return View(cat.Beers.Where(x => x.Beer_Style == id));
            }
        }

        // GET: Product
        public async Task<ActionResult> Index(int? id)
        {
            return View(await db.Beers.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Index(Beer model)
        {
            Guid? cartID = this.GetCartID();
            Cart cart = null;
            if (cartID.HasValue)
            {
                cart = await db.Carts.FindAsync(cartID);
            }
            if (cart == null)
            {
                cartID = Guid.NewGuid();
                cart = new Cart
                {
                    ID = cartID.Value,
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow
                };
                db.Carts.Add(cart);
                Response.AppendCookie(new HttpCookie(CartHelper.CartID, cartID.ToString()));
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

            await db.SaveChangesAsync();
            TempData.Add("NewItem", model.Name);

            return RedirectToAction("Index", "Cart");

        }
    }
}