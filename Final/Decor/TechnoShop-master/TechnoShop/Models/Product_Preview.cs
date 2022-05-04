using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoShop.Models
{
    public class Product_Preview
    {
        public int Product_ID { get; set; }
        public int User_ID { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime Created_Time { get; set; }

        public Product_Preview() { }
        public Product_Preview(int product_ID, int user_ID, int rate, string comment, DateTime created_Time)
        {
            Product_ID = product_ID;
            User_ID = user_ID;
            Rate = rate;
            Comment = comment;
            Created_Time = created_Time;
        }
    }
}