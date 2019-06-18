using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StationeryWebApp.Models
{
    public class Basket
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ItemName { get; set; }
        public int BasketId { get; set; }
    }
}