using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace BeerPack.Controllers
{
    public class AccountController : Controller
    {

        BeerPackPaymentService beerpackPaymentService = new BeerPackPaymentService();
        // GET: Account
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var customer = await beerpackPaymentService.GetCustomer(User.Identity.Name);
            return View(customer);    
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Index(string firstName, string lastName, string id)
        {
            Braintree.Customer customer = await beerpackPaymentService.UpdateCustomer(firstName, lastName, id);
            ViewBag.Message = "Updated Successfully";

            return View(customer);
        }

        [Authorize]
        public async Task<ActionResult> Addresses()
        {
            var customer = await beerpackPaymentService.GetCustomer(User.Identity.Name);
            return View(customer.Addresses);
        }

        [Authorize]
        public async Task<ActionResult> DeleteAddress(string id)
        {
            await beerpackPaymentService.DeleteAddress(User.Identity.Name, id);
            TempData["SuccessMessage"] = "Address deleted successfully";
            return RedirectToAction("Addresses");

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddAddress(string firstName, string lastName, string company, string streetAddress, string extendedAddress, string locality, string region, string postalCode, string countryName)
        {

           await beerpackPaymentService.AddAddress(User.Identity.Name, firstName, lastName, company, streetAddress, extendedAddress, locality, region, postalCode, countryName);

            TempData["SuccessMessage"] = "Address added successfully";
            return RedirectToAction("Addresses");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(string username, string password)
        {
            //Make sure the following statements are in the using block:
            //using Microsoft.AspNet.Identity;
            //using Microsoft.AspNet.Identity.EntityFramework;
            //using Microsoft.AspNet.Identity.Owin;
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            IdentityUser user = new IdentityUser { Email = username, UserName = username };

            IdentityResult result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }, userIdentity);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = result.Errors;
            return View();
            
        }
        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string userName, string password, bool? staySignedIn)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByName(userName);
            if (user != null)
            {
                bool isPasswordValid = userManager.CheckPassword(user, password);
                if (isPasswordValid)
                {
                    var claimsIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                    {
                        IsPersistent = staySignedIn ?? false,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    }, claimsIdentity);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Error = new string[] { "Unable to sign in" };
            return View();
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if (user != null)
            {
                string resetToken = await userManager.GeneratePasswordResetTokenAsync(user.Id);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ResetPassword?email=" + email + "&token=" + resetToken;
                string message = string.Format("<a href=\"{0}\">Reset your password</a>", resetUrl);
                await userManager.SendEmailAsync(user.Id, "your password reset token", message);

                SendForgotEmail();
                IRestResponse SendForgotEmail()
                {
                    RestClient client = new RestClient();
                    client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                    client.Authenticator =
                        new HttpBasicAuthenticator("api",
                                                    System.Configuration.ConfigurationManager.AppSettings["MailGun.PrivateKey"]);
                    RestRequest request = new RestRequest();
                    request.AddParameter("domain", "sandboxf4eb1f22f4094912ad448a1ec94c09ef.mailgun.org", ParameterType.UrlSegment);
                    request.Resource = "{domain}/messages";
                    request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxf4eb1f22f4094912ad448a1ec94c09ef.mailgun.org>");
                    request.AddParameter("to", email);
                    request.AddParameter("subject", "Hello");
                    request.AddParameter("text", message);
                    //request.AddParameter("text", "hi");
                    request.Method = Method.POST;
                    return client.Execute(request);
                }
            }
           
            return RedirectToAction("ForgotPasswordSent");
        }

        public ActionResult ForgotPasswordSent()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(string email, string token, string newPassword)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                IdentityResult result = await userManager.ResetPasswordAsync(user.Id, token, newPassword);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Your password has been updated successfully";
                    return RedirectToAction("SignIn", "Account");
                }

            }
            return RedirectToAction("Index", "Home");
        }
    }
}

