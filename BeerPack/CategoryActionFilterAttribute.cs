using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerPack
{
    public class CategoryActionFilterAttribute : ActionFilterAttribute, IActionFilter
    {

        private static string[] _categories = null;
        //Happens before the controller method is run
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_categories == null)
            {
                using (BeerPack.Models.BeerPackEntities db = new BeerPack.Models.BeerPackEntities())
                {

                    _categories = db.Categories.Select(x => x.Id).ToArray();
                }
            }
            filterContext.Controller.ViewBag.Categories = _categories;
            base.OnActionExecuting(filterContext);
        }

        //Happens after the controller method is run, but before the view is rendered
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
        }
    }
}