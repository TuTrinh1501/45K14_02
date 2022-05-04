using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections;
using TechnoShop.Models;
using System.Diagnostics;

namespace TechnoShop.DAO
{
    public class OrdersDAO : DataAcess
    {   //get ListOrder
        public static List<Order> GetListOrder(int user_id)
        {
            List<Order> list = new List<Order>();
            string sql = "SELECT * FROM Orders where User_ID=@User_ID and Status != 1 ";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            SqlParameter p1 = new SqlParameter("@User_ID", SqlDbType.Int);
            p1.Value = user_id;
            Command.Parameters.Add(p1);
            DataTable data = GetDataBySQLCommand(Command);
            foreach (DataRow dr in data.Rows)
            {
                Order order = new Order();
                order.Order_ID = Convert.ToInt32(dr["Order_ID"].ToString());
                order.User_ID = Convert.ToInt32(dr["User_ID"].ToString());
                order.Total = float.Parse(dr["Total"].ToString());
                order.Status = Convert.ToInt32(dr["Status"].ToString());
                order.Created_Time = DateTime.Parse(dr["Created_Time"].ToString());
                list.Add(order);

            }
            return list;
        } 

        // add new order 
        public static int NewOrder(Order order)
        {
            int Order_ID = 0;
            string sql = "INSERT INTO Orders VALUES(@User_ID,@Total,@Status,@Created_Time) SELECT SCOPE_IDENTITY()";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@User_ID", SqlDbType.Int);
                p1.Value = order.User_ID;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Total", SqlDbType.Float);
                p2.Value = order.Total;
                Command.Parameters.Add(p2);
                SqlParameter p3 = new SqlParameter("@Status", SqlDbType.Int);
                p3.Value = order.Status;
                Command.Parameters.Add(p3);
                SqlParameter p4 = new SqlParameter("@Created_Time", SqlDbType.DateTime);
                p4.Value = order.Created_Time;
                Command.Parameters.Add(p4);
                Command.Connection.Open();
              Order_ID =Convert.ToInt32(Command.ExecuteScalar());

            }
            catch (Exception e)
            {
                Debug.WriteLine("loi " + e);
            }
            finally
            {
                Command.Connection.Close();
            }
            return Order_ID;
        } 
        // lay order Id theo User
        public static int getOrderIDbyUser(int UserID)
        {
            int Order_ID = 0;
            String sql = "select Order_ID FROM Orders where User_ID=@User_ID and Status=1";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@User_ID", SqlDbType.Int);
                p1.Value = UserID;
                Command.Parameters.Add(p1);
                Command.Connection.Open();
                SqlDataReader dataReader = Command.ExecuteReader();
                if (dataReader.Read())
                {
                    Order_ID =dataReader.GetInt32(0);
                }
                else
                {
                     Order_ID = 0;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("loi get Order by Id user " + e);
              return Order_ID = 0;
            }
            finally
            {
                Command.Connection.Close();
            }
            return Order_ID;
        }
        // Update stt orders 
        public static void UpdateSTTOrders(int Order_ID, int Status)
        {
            String sql = "Update Orders set Status=@Status Where Order_ID=@Order_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@Status", SqlDbType.Int);
                p1.Value = Status;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Order_ID", SqlDbType.Float);
                p2.Value = Order_ID;
                Command.Parameters.Add(p2);
                Command.Connection.Open();
                Command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                Command.Connection.Close();
            }
        }
        // update order 
        public static void UpdateOrders(Order order)
        {
            String sql = "Update Orders set Status=@Status,Total=@Total,Created_Time=@Created_Time Where Order_ID=@Order_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@Order_ID", SqlDbType.Int);
                p1.Value = order.Order_ID;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Total", SqlDbType.Float);
                p2.Value = order.Total;
                Command.Parameters.Add(p2);
                SqlParameter p3 = new SqlParameter("@Status", SqlDbType.Int);
                p3.Value = order.Status;
                Command.Parameters.Add(p3);
                SqlParameter p4 = new SqlParameter("@Created_Time", SqlDbType.DateTime);
                p4.Value = order.Created_Time;
                Command.Parameters.Add(p4);
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
    }
}