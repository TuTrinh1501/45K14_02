using TechnoShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TechnoShop.DAO
{
    public class ProductDAO : DataAcess
    {
        public static List<Product> GetList()
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Products where Status=1";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable data = GetDataBySQLCommand(Command);

            foreach (DataRow dr in data.Rows)
            {
                int Product_ID = Convert.ToInt32(dr["Product_ID"].ToString());
                string Product_Name = dr["Product_Name"].ToString();
                string Description = dr["Description"].ToString();
                int Category_ID = Convert.ToInt32(dr["Category_ID"].ToString());
                float Price = float.Parse(dr["Price"].ToString());
                int Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                string Image = dr["Image"].ToString();
                float PercentOfDiscount = float.Parse(dr["PercentOfDiscount"].ToString());
                int user_id = Convert.ToInt32(dr["User_ID"].ToString());
                list.Add(new Product(Product_ID, Product_Name, Description, Category_ID, Price, Quantity, Image, PercentOfDiscount, user_id));
            }
            return list;
        }

        public static List<Product> SearchProductByName(string searchString)
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Products WHERE Product_Name LIKE '%' + @Product_Name + '%' and Status=1";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            var p = new SqlParameter("@Product_Name", SqlDbType.VarChar)
            {
                Value = "%" + searchString + "%"
            };
            Command.Parameters.Add(p);
            DataTable data = GetDataBySQLCommand(Command);
            foreach (DataRow dr in data.Rows)
            {
                int Product_ID = Convert.ToInt32(dr["Product_ID"].ToString());
                string Product_Name = dr["Product_Name"].ToString();
                string Description = dr["Description"].ToString();
                int Category_ID = Convert.ToInt32(dr["Category_ID"].ToString());
                float Price = float.Parse(dr["Price"].ToString());
                int Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                string Image = dr["Image"].ToString();
                float PercentOfDiscount = float.Parse(dr["PercentOfDiscount"].ToString());
                int user_id = Convert.ToInt32(dr["User_ID"].ToString());
                list.Add(new Product(Product_ID, Product_Name, Description, Category_ID, Price, Quantity, Image, PercentOfDiscount,user_id));
            }
            return list;
        }
        public static List<Product> SearchProductByCategory(int category)
        {
            List<Product> list = new List<Product>();
            string sql = "SELECT * FROM Products WHERE Category_ID=@Category_ID and Status=1";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            SqlParameter p1 = new SqlParameter("@Category_ID", SqlDbType.Int);
            p1.Value = category;
            Command.Parameters.Add(p1);
            DataTable data = GetDataBySQLCommand(Command);
            foreach (DataRow dr in data.Rows)
            {
                int Product_ID = Convert.ToInt32(dr["Product_ID"].ToString());
                string Product_Name = dr["Product_Name"].ToString();
                string Description = dr["Description"].ToString();
                int Category_ID = Convert.ToInt32(dr["Category_ID"].ToString());
                float Price = float.Parse(dr["Price"].ToString());
                int Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                string Image = dr["Image"].ToString();
                float PercentOfDiscount = float.Parse(dr["PercentOfDiscount"].ToString());
                int user_id = Convert.ToInt32(dr["User_ID"].ToString());
                list.Add(new Product(Product_ID, Product_Name, Description, Category_ID, Price, Quantity, Image, PercentOfDiscount,user_id));
            }
            return list;
        }
        public void AddProduct(Product product)
        {
            string sql = "INSERT INTO Products(Product_Name,Description,Category_ID,Price,Quantity,Image,PercentOfDiscount)" +
                "VALUES(@Product_Name,@Description,@Category_ID,@Price,@Quantity,@Image,@PercentOfDiscount)";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@Product_Name", SqlDbType.VarChar);
                p1.Value = product.Product_Name;
                Command.Parameters.Add(p1);

                SqlParameter p2 = new SqlParameter("@Description", SqlDbType.NText);
                p2.Value = product.Description;
                Command.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter("@Category_ID", SqlDbType.Int);
                p3.Value = product.Category_ID;
                Command.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter("@Price", SqlDbType.Float);
                p4.Value = product.Price;
                Command.Parameters.Add(p4);

                SqlParameter p5 = new SqlParameter("@Quantity", SqlDbType.Int);
                p5.Value = product.Quantity;
                Command.Parameters.Add(p5);

                SqlParameter p6 = new SqlParameter("@Image", SqlDbType.VarChar);
                p6.Value = product.Image;
                Command.Parameters.Add(p6);

                SqlParameter p7 = new SqlParameter("@PercentOfDiscount", SqlDbType.Float);
                p7.Value = product.PercentOfDiscount;
                Command.Parameters.Add(p7);

                Command.Connection.Open();
                Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                Command.Connection.Close();
            }
        }

        public static void DeleteProduct(Product product) 
        {
            string sql = "DELETE Products WHERE Product_ID = @product_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            SqlParameter p = new SqlParameter("@product_ID", SqlDbType.Int)
            {
                Value = product.Product_ID
            };
            Command.Parameters.Add(p);
            Command.Connection.Open();
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }

        public static bool IsExistProduct(string product_Name)
        {
            bool check;
            string sql = "SELECT * FROM Products WHERE Product_Name = @product_Name";
            List<Product> list = new List<Product>();
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            var p = new SqlParameter("product_Name", SqlDbType.VarChar)
            {
                Value = product_Name
            };
            Command.Parameters.Add(p);
            DataTable data = GetDataBySQLCommand(Command);
            foreach (DataRow dr in data.Rows)
            {
                int Product_ID = Convert.ToInt32(dr["Product_ID"].ToString());
                string Product_Name = dr["Product_Name"].ToString();
                string Description = dr["Description"].ToString();
                int Category_ID = Convert.ToInt32(dr["Category_ID"].ToString());
                float Price = float.Parse(dr["Price"].ToString());
                int Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                string Image = dr["Image"].ToString();
                float PercentOfDiscount = float.Parse(dr["PercentOfDiscount"].ToString());
                int user_id = Convert.ToInt32(dr["User_ID"].ToString());
                list.Add(new Product(Product_ID, Product_Name, Description, Category_ID, Price, Quantity, Image, PercentOfDiscount, user_id));
            }
            if (list.Any())
            {
                check = true;
            }
            else
            {
                check = false;
            }
            return check;
        }

        public static Product GetProductByID(int id) 
        {
            string sql = "SELECT Products.Product_ID,Products.Product_Name, Products.Description, Categories.Category_Name, Products.Price, Products.Quantity, Products.Image, Products.PercentOfDiscount FROM Products JOIN Categories ON Products.Category_ID = Categories.Category_ID WHERE Product_ID = @Product_ID";
            Product product = new Product();
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            var p = new SqlParameter("Product_ID", SqlDbType.Int)
            {
                Value = id
            };
            Command.Parameters.Add(p);
            DataTable data = GetDataBySQLCommand(Command);
            
            foreach (DataRow dr in data.Rows)
            {
                int Product_ID = Convert.ToInt32(dr["Product_ID"].ToString());
                string Product_Name = dr["Product_Name"].ToString();
                string Description = dr["Description"].ToString();
                string Category_Name = dr["Category_Name"].ToString();
                float Price = float.Parse(dr["Price"].ToString());
                int Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                string Image = dr["Image"].ToString();
                float PercentOfDiscount = float.Parse(dr["PercentOfDiscount"].ToString());
                product = new Product(Product_ID, Product_Name, Description, Category_Name, Price, Quantity, Image, PercentOfDiscount);
            }
            return product;
        }

        public static float GetMax()
        {
            float Price = 0;
            string sql = "select MAX(Price) from Products where Status=1 ";
            Product product = new Product();
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            Command.Connection.Open();
            try
            {
                Price = float.Parse(Command.ExecuteScalar().ToString());
            }
            catch(Exception e)
            {

            }
           
          
            return Price;
        }
        public static float GetMin()
        {
            float Price = 0;
            string sql = "select MIN(Price) from Products where Status=1 ";
            Product product = new Product();
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            Command.Connection.Open();
            try
            {
                Price = float.Parse(Command.ExecuteScalar().ToString());
            }
            catch(Exception e)
            {

            }

          
            return Price;
        }
        public static List<Product> SearchProductByPrice(float min, float max)
        {
            List<Product> list = new List<Product>();
            string sql = "select* from Products where Price BETWEEN '"+min+"' and '"+max+ "' and Status=1";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable data = GetDataBySQLCommand(Command);
            foreach (DataRow dr in data.Rows)
            {
                int Product_ID = Convert.ToInt32(dr["Product_ID"].ToString());
                string Product_Name = dr["Product_Name"].ToString();
                string Description = dr["Description"].ToString();
                int Category_ID = Convert.ToInt32(dr["Category_ID"].ToString());
                float Price = float.Parse(dr["Price"].ToString());
                int Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                string Image = dr["Image"].ToString();
                float PercentOfDiscount = float.Parse(dr["PercentOfDiscount"].ToString());
                int user_id = Convert.ToInt32(dr["User_ID"].ToString());
                list.Add(new Product(Product_ID, Product_Name, Description, Category_ID, Price, Quantity, Image, PercentOfDiscount,user_id));
            }
            return list;
        }
    }
}