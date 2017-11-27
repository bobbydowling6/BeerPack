using System.Threading.Tasks;
using BeerPack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

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
            details.Addresses = new Braintree.Address[0];
            if (User.Identity.IsAuthenticated)
            {
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

                    var result = customerGateway.Create(newCustomer);
                    customer = result.Target;
                }
                else
                {
                    customer = matchedCustomers.FirstItem;
                }

                details.Addresses = customer.Addresses;
            }
            return View(details);
        }

        // POST: Checkout
        [HttpPost]
        public async Task<ActionResult> Index(Models.CheckoutDetails model, string addressId)
        {


            //model.CurrentCart = Models.Cart.BuildCart(Request);
            Guid cartID = Guid.Parse(Request.Cookies["cartID"].Value);

            model.CurrentCart = db.Carts.Find(cartID);
            model.Addresses = new Braintree.Address[0];
            if (ModelState.IsValid)
            {
                string trackingNumber = Guid.NewGuid().ToString().Substring(0, 8);
                decimal tax = (model.CurrentCart.CartProducts.Sum(x => x.Beer.Price * x.Quantity) ?? 0) * .1025m;
                decimal subtotal = model.CurrentCart.CartProducts.Sum(x => x.Beer.Price * x.Quantity) ?? 0;
                decimal shipping = model.CurrentCart.CartProducts.Sum(x => x.Quantity);
                decimal total = subtotal + tax + shipping;

                #region pay for order
                BeerPackPaymentService payments = new BeerPackPaymentService();
                string email = User.Identity.IsAuthenticated ? User.Identity.Name : model.ContactEmail;
                string message = await payments.AuthorizeCard(email, total, tax, trackingNumber, addressId, model.CardholderName, model.CVV, model.CreditCardNumber, model.ExpirationMonth, model.ExpirationYear);
                #endregion
                #region save order
                if (string.IsNullOrEmpty(message))
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

                    await db.SaveChangesAsync();
                    #endregion
                    #region send email

                    //BeerPackEmailService emailService = new BeerPackEmailService();
                    //emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                    //{
                    //    Subject = "Your Receipt for order " + trackingNumber,
                    //    Destination = model.ContactEmail,
                    //    Body = "Thank you for shopping."
                    //});
                    
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
                ModelState.AddModelError("CreditCardNumber", message);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ValidateAddress(string street, string city, string state, string zip)
        {
            string authId = ConfigurationManager.AppSettings["SmartyStreets.AuthID"];
            string authToken = ConfigurationManager.AppSettings["SmartyStreets.AuthToken"];
            SmartyStreets.ClientBuilder clientBuilder = new SmartyStreets.ClientBuilder(authId, authToken);
            var client = clientBuilder.BuildUsStreetApiClient();
            SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup();
            lookup.City = city;
            lookup.ZipCode = zip;
            lookup.Street = street;
            lookup.State = state;
            client.Send(lookup);

            return Json(lookup.Result.Select(x => new { street = x.DeliveryLine1, city = x.Components.CityName, state = x.Components.State, zip = x.Components.ZipCode + "-" + x.Components.Plus4Code }));
        }
    }
}
