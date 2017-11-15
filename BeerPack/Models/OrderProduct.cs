namespace BeerPack.Models
{
    using System;
    using System.Collections.Generic;

    public class OrderProduct
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string PlacedName { get; set; }
        public decimal PlacedUnitPrice { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateLastModified { get; set; }

        public virtual Order Order { get; set; }
        public virtual Beer Product { get; set; }
    }
}