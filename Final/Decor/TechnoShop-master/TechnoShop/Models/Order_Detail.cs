using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoShop.Models
{
    public class Order_Detail
    {  
        public int Id { get; set; }
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }

        public Order_Detail() { }

        public Order_Detail(int id, int order_ID, int product_ID, int quantity)
        {
            Id = id;
            Order_ID = order_ID;
            Product_ID = product_ID;
            Quantity = quantity;
        }
    }
}