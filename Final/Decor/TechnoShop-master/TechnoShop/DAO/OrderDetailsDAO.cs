using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections;
using TechnoShop.Models;
using System.Diagnostics;
using TechnoShop.Models.ViewModel;

namespace TechnoShop.DAO
{
    public class OrderDetailsDAO : DataAcess
    {
        //lay get listOrder
        public static List<Order_Detail> GetListOrder(int order)
        {
            List<Order_Detail> list = new List<Order_Detail>(); 
            string sql = "SELECT * FROM Order_Details where @Order_ID=Order_ID ";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@Order_ID", SqlDbType.Int);
                p1.Value = order;
                Command.Parameters.Add(p1);
            }
            catch(Exception e)
            {
                throw e;
            }
            DataTable data = GetDataBySQLCommand(Command);
            foreach (DataRow dr in data.Rows)
            {
                Order_Detail orderDetails = new Order_Detail();
                orderDetails.Id = Convert.ToInt32(dr["Id"].ToString());
                orderDetails.Order_ID = Convert.ToInt32(dr["Order_ID"].ToString());
                orderDetails.Product_ID = Convert.ToInt32(dr["Product_ID"].ToString());
                orderDetails.Quantity = Convert.ToInt32(dr["Quantity"].ToString());
                list.Add(orderDetails);

            }
            return list;
        } 
        //create new order 
        public static void NewOrderDetails(Order_Detail orderdetails)
        {
            int quantity = 0;
            int orderdetailsid = 0;
            List<Order_Detail> list = GetListOrder(orderdetails.Order_ID); // get list order theo Order ID
            foreach(Order_Detail order_Detail in list) 
            { 
                if(order_Detail.Product_ID == orderdetails.Product_ID) // neu da co product
                {
                    quantity = order_Detail.Quantity; // lay so luong tra cart
                    orderdetailsid = order_Detail.Id; // lay orderdetails id
                }
            }
            if(quantity > 0)
            {
                int updatequantity = quantity + 1; // tinh quantity
                UpdateOrdersDetails(orderdetailsid, updatequantity); // update quantity in order details
            }
            else
            { // neu khong co thi tao moi 
                string sql = "INSERT INTO Order_Details VALUES(@Order_ID,@Product_ID,@Quantity)";
                SqlCommand Command = new SqlCommand(sql, GetConnection());
                try
                {
                    SqlParameter p1 = new SqlParameter("@Order_ID", SqlDbType.Int);
                    p1.Value = orderdetails.Order_ID;
                    Command.Parameters.Add(p1);
                    SqlParameter p2 = new SqlParameter("@Product_ID", SqlDbType.Int);
                    p2.Value = orderdetails.Product_ID;
                    Command.Parameters.Add(p2);
                    SqlParameter p3 = new SqlParameter("@Quantity", SqlDbType.Int);
                    p3.Value = orderdetails.Quantity;
                    Command.Parameters.Add(p3);
                    Command.Connection.Open();
                    Command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
                finally
                {
                    Command.Connection.Close();
                }
            }
          
        }
        //update order_detail
        public static void UpdateOrdersDetails(int orderDetails_ID, int Quantity)
        {
            String sql = "Update Order_Details set Quantity=@Quantity Where Id=@Id";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@Quantity", SqlDbType.Int);
                p1.Value = Quantity;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Id", SqlDbType.Float);
                p2.Value = orderDetails_ID;
                Command.Parameters.Add(p2);
                Command.Connection.Open();
                Command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Command.Connection.Close();
            }
        }
        //delete order details
        public static void DeleteCartbyID(int id)
        {
            string sql = "DELETE Order_Details WHERE Id = @Id";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            SqlParameter p = new SqlParameter("@Id", SqlDbType.Int)
            {
                Value = id
            };
            Command.Parameters.Add(p);
            Command.Connection.Open();
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }
        // details het cart
        public static void DeleteCart(int id)
        {
            string sql = "DELETE Order_Details WHERE Order_ID = @Order_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            SqlParameter p = new SqlParameter("@Order_ID", SqlDbType.Int)
            {
                Value = id
            };
            Command.Parameters.Add(p);
            Command.Connection.Open();
            Command.ExecuteNonQuery();
            Command.Connection.Close();
        }
        public static List<Total> GetListTotal()
        {
            List<Total> list = new List<Total>();
            string sql = "Select TOP(5) Product_ID, SUM(Quantity) AS 'Tong' from Order_Details GROUP BY Product_ID  ";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable data = GetDataBySQLCommand(Command);
            foreach (DataRow dr in data.Rows)
            {
                Total total = new Total();
                 total.Product = Convert.ToInt32(dr["Product_ID"].ToString());
                  total.Tong = Convert.ToInt32(dr["Tong"].ToString());
                list.Add(total);

            }
            return list;
        }
    }
}