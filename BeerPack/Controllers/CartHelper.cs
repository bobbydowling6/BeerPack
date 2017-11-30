using System;
using System.Linq;
using System.Web.Mvc;

namespace BeerPack.Controllers
{
    internal static class CartHelper
    {
        public const string CartID = "cartID";

        internal static Guid? GetCartID(this Controller controller)
        {
            if (controller.Request.Cookies.AllKeys.Contains(CartID))
            {
                Guid result;
                if(Guid.TryParse(controller.Request.Cookies[CartID].Value, out result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}