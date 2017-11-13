using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerPack.Models
{
    public class SalesReportModel
    {
        public string SelectedState { get; set; }
        public string[] States { get; set; }

        public TopSaleByDollar[] TopSalesByDollar { get; set; }
        public TopSaleByQuantity[] TopSalesByQuantity { get; set; }
    }

    public class TopSaleByQuantity
    {
        public int TotalQuantity { get; set; }
        public string ProductName { get; set; }
        public int ProductID { get; set; } 
    }

    public class TopSaleByDollar
    {
        public int ProductID { get; set; }
        //TODO: Add more properties
        public string[] States { get; set; }
        public string StateAddress { get; set; }
        public TopSaleByDollar[] TopSalesByDollar { get; set; }
        public int Price { get; set; }
    }
}