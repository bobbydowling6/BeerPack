using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerPack.Models
{
    public partial class Order
    {
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Email { get; set; }
        public string PurchaserName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingAndHandling { get; set; }
        public decimal Tax { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateLastModified { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }

}