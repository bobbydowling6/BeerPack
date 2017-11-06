using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerPack.Models
{
    public class Cart
    {
        public Ales[] Products { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingAndHandling { get; set; }
        public decimal Total { get; set; }


        public static Cart BuildCart(HttpRequestBase request)
        {
            Cart cart = new Cart();
            cart.Products = new Ales[0];
            if (request.Cookies.AllKeys.Contains("productName"))
            {
                cart.Products = new Ales[1];
                cart.Products[0] = new Ales();
                cart.Products[0].Name = (request.Cookies["productName"].Value);
            }
            if (request.Cookies.AllKeys.Contains("productPrice"))
            {
                cart.Products[0].Price = decimal.Parse(request.Cookies["productPrice"].Value.Replace("$", ""));
            }
            if (request.Cookies.AllKeys.Contains("productQuantity"))
            {
                cart.Products[0].Quantity = int.Parse(request.Cookies["productQuantity"].Value);
            }
            cart.SubTotal = cart.Products.Sum(x => x.Price * x.Quantity);

            cart.Tax = cart.SubTotal * .1025m;
            cart.ShippingAndHandling = cart.Products.Sum(x => x.Quantity) * 1m;
            cart.Total = cart.SubTotal + cart.Tax + cart.ShippingAndHandling;
            return cart;
        }
    }
}