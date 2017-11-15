using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerPack.Models
{
    public class CartProduct
    {
        public System.Guid CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateLastModified { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Beer Product { get; set; }
    }
}