﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Models.Cart.BuildCart(Request);
            ViewBag.Cart = cart;
            return View(cart);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.Cart model)
        {
            ViewBag.Cart = Models.Cart.BuildCart(this.Request);
            Response.AppendCookie(new HttpCookie("productQuantity", model.Products[0].Quantity.ToString()));

            model.SubTotal = model.Products.Sum(x => x.Price * x.Quantity);

            model.Tax = model.SubTotal * .1025m;
            model.ShippingAndHandling = model.Products.Sum(x => x.Quantity) * 1m;
            model.Total = model.SubTotal + model.Tax + model.ShippingAndHandling;
            return View(model);
        }
    }
}