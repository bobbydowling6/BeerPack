//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeerPack.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartProduct
    {
        public System.Guid CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateLastModified { get; set; }
    
        public virtual Beer Beer { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
