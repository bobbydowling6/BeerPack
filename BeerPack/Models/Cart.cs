using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerPack.Models
{
    public partial class Cart
    {
          public Cart()
            {
                this.CartProducts = new HashSet<CartProduct>();
            }

            public System.Guid ID { get; set; }
            public string AspNetUserId { get; set; }
            public System.DateTime DateCreated { get; set; }
            public System.DateTime DateLastModified { get; set; }

            public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}
