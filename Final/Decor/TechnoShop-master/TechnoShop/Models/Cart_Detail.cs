using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoShop.Models
{
    public class Cart_Detail
    {
        public int Cart_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
    
        public Cart_Detail() { }
        public Cart_Detail(int cart_ID, int product_ID, int quantity)
        {
            Cart_ID = cart_ID;
            Product_ID = product_ID;
            Quantity = quantity;
        }
    }
}