
using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class CheckoutController : Controller
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

        // GET: Checkout
        public ActionResult Index()
        {
            Models.CheckoutDetails details = new Models.CheckoutDetails();
            Guid cartID = Guid.Parse(Request.Cookies["cartID"].Value);

            details.CurrentCart = db.Carts.Find(cartID);

            return View(details);
        }

        // POST: Checkout
        [HttpPost]
        public ActionResult Index(Models.CheckoutDetails model)
        {


            //model.CurrentCart = Models.Cart.BuildCart(Request);
            Guid cartID = Guid.Parse(Request.Cookies["cartID"].Value);

            model.CurrentCart = db.Carts.Find(cartID);

            if (ModelState.IsValid)
            {
                string trackingNumber = Guid.NewGuid().ToString().Substring(0, 8);
                decimal tax = (model.CurrentCart.CartProducts.Sum(x => x.Beer.Price * x.Quantity) ?? 0) * .1025m;
                decimal subtotal = model.CurrentCart.CartProducts.Sum(x => x.Beer.Price * x.Quantity) ?? 0;
                decimal shipping = model.CurrentCart.CartProducts.Sum(x => x.Quantity);
                decimal total = subtotal + tax + shipping;

                #region pay for order
                string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
                string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
                string publicKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
                string privateKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                Braintree.BraintreeGateway gateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

                var customerGateway = gateway.Customer;
                Braintree.CustomerSearchRequest query = new Braintree.CustomerSearchRequest();
                query.Email.Is(User.Identity.Name);
                var matchedCustomers = customerGateway.Search(query);
                Braintree.Customer customer = null;
                if (matchedCustomers.Ids.Count == 0)
                {
                    Braintree.CustomerRequest newCustomer = new Braintree.CustomerRequest();
                    newCustomer.Email = User.Identity.Name;

                    var r = customerGateway.Create(newCustomer);
                    customer = r.Target;
                }
                else
                {
                    customer = matchedCustomers.FirstItem;
                }

                Braintree.TransactionRequest transaction = new Braintree.TransactionRequest();
                //transaction.Amount = 1m;    //I can hard-code a dollar amount for now to test everything else
                transaction.Amount = total;
                transaction.TaxAmount = tax;
                transaction.OrderId = trackingNumber;
                transaction.CustomerId = customer.Id;
                //https://developers.braintreepayments.com/reference/general/testing/ruby
                transaction.CreditCard = new Braintree.TransactionCreditCardRequest
                {
                    CardholderName = model.CardholderName,
                    CVV = model.CVV,
                    Number = model.CreditCardNumber,
                    ExpirationYear = model.ExpirationYear,
                    ExpirationMonth = model.ExpirationMonth
                };

                var result = gateway.Transaction.Sale(transaction);
                #endregion
                #region save order
                if (string.IsNullOrEmpty(result.Message))
                {
                    Order o = new Order
                    {
                        DateCreated = DateTime.UtcNow,
                        DateLastModified = DateTime.UtcNow,
                        TrackingNumber = trackingNumber,
                        ShippingAndHandling = shipping,
                        Tax = tax,
                        SubTotal = subtotal,
                        Email = model.ContactEmail,
                        PurchaserName = model.ContactName,
                        ShippingAddress1 = model.ShippingAddress,
                        ShippingCity = model.ShippingCity,
                        ShippingPostalCode = model.ShippingPostalCode,
                        ShippingState = model.ShippingState
                    };
                    db.Orders.Add(o);

                    db.SaveChanges();
                    #endregion
                    #region send email

                    BeerPackEmailService emailService = new BeerPackEmailService();
                    emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                    {
                        Subject = "Your Receipt for order " + trackingNumber,
                        Destination = model.ContactEmail,
                        Body = "Thank you for shopping."
                    });
                    #endregion
                    #region reset cart
                    //Reset the cart - Trash the cookie, so they'll get a new cart next time they need one
                    Response.SetCookie(new HttpCookie("cartID") { Expires = DateTime.UtcNow });
                    //If you have a cart table, you can clear this cart out since it has now been converted to an order
                    db.CartProducts.RemoveRange(model.CurrentCart.CartProducts);
                    db.Carts.Remove(model.CurrentCart);
                    db.SaveChanges();

                    #endregion
                    return RedirectToAction("Index", "Receipt", new { id = trackingNumber });
                }
                ModelState.AddModelError("CreditCardNumber", result.Message);
            }
            return View(model);
        }
    }
}
