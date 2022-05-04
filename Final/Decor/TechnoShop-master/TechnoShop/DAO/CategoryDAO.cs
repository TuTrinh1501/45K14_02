using TechnoShop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections;

namespace TechnoShop.DAO
{
    public class CategoryDAO : DataAcess
    {
        public static List<Category> GetList()
        {
            List<Category> list = new List<Category>();
            string sql = "SELECT * FROM Categories";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable data = GetDataBySQLCommand(Command);

            foreach (DataRow dr in data.Rows)
            {
                int Category_ID = Convert.ToInt32(dr["Category_ID"].ToString());
                string Category_Name = dr["Category_Name"].ToString();
                string Image = dr["Image"].ToString();
                list.Add(new Category(Category_ID, Category_Name, Image));
            }
            return list;
        }

        public Category GetCategoryById(int? id)
        {
            Category cate = new Category();
            string sql = "SELECT * FROM Categories WHERE Category_ID = " + id;
            DataTable data = GetDataBySQL(sql);
            foreach (DataRow dr in data.Rows)
            {
                int Category_ID = Convert.ToInt32(dr["Category_ID"].ToString());
                string Category_Name = dr["Category_Name"].ToString();
                string Image = dr["Image"].ToString();
                cate = new Category(Category_ID, Category_Name, Image);
            }
            return cate;
        }

        public int DeleteById(int? id)
        {
            string sql = "Delete from Categories where Category_ID = @id";
            SqlParameter parameter = new SqlParameter("@id", SqlDbType.Int)
            {
                Value = id
            };
            return ExcuteQuery(sql, parameter);
        }

        public int CategoryAddOrUpdate(Category cate)
        {
            ArrayList array;
            string sql;
            SqlParameter[] parameters;
            if (cate.Category_ID == 0)
            {
                array = new ArrayList { cate.Category_Name ?? (object)DBNull.Value, cate.Image ?? (object)DBNull.Value };
                sql = "INSERT INTO Categories VALUES(@cartName,@img)";
                parameters = new SqlParameter[] {
                new SqlParameter("@cartName",SqlDbType.NChar),
                new SqlParameter("@img",SqlDbType.NVarChar)
                };
            }
            else
            {
                array = new ArrayList { cate.Category_ID, cate.Category_Name ?? (object)DBNull.Value, cate.Image ?? (object)DBNull.Value };
                sql = "UPDATE Categories SET Category_Name = @cartName,Image = @img where Category_ID = @id";
                parameters = new SqlParameter[] {
                new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@cartName",SqlDbType.NChar),
                new SqlParameter("@img",SqlDbType.NVarChar)
                };
            }
            for (int i = 0; i < array.Count; i++)
            {
                parameters[i].Value = array[i];
            }
            return ExcuteQuery(sql, parameters);
        }
    }
}