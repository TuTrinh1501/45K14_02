using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TechnoShop.DAO
{
    //TamTD comment
    public class DataAcess
    {
        public static SqlConnection GetConnection()
        {
            String ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            //string ConnectionString = "server=LocalHost; database=MSS;Integrated Security=True;";
            //demo
            return new SqlConnection(ConnectionString);
        }
        public static DataTable GetDataBySQL(string sql)
        {
            SqlCommand command = new SqlCommand(sql, GetConnection());
            SqlDataAdapter adapt = new SqlDataAdapter();
            adapt.SelectCommand = command;
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            return ds.Tables[0];
        }
        public static DataTable GetDataBySQLCommand(SqlCommand command)
        {
            SqlDataAdapter adapt = new SqlDataAdapter();
            adapt.SelectCommand = command;
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            return ds.Tables[0];
        }

        public static int ExcuteQuery(string sql, params SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.Parameters.AddRange(parameters);
            cmd.Connection.Open();
            int numRows = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return numRows;
        }
    }
}