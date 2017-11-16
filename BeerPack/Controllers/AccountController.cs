using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            }
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            HttpContext.GetOwinContext().Authentication.SignIn();
            return View();
        }
        public ActionResult LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            //Make sure the following statements are in the using block:
            //using Microsoft.AspNet.Identity;
            //using Microsoft.AspNet.Identity.EntityFramework;
            //using Microsoft.AspNet.Identity.Owin;
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            IdentityUser user = new IdentityUser { Email = username, UserName = username };

            IdentityResult result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
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
    }
}