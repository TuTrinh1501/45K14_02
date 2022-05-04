using System;
using System.Collections.Generic;
using TechnoShop.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TechnoShop.DAO
{
    public class UserDAO : DataAcess
    {
        public static List<User> getAllUser()
        {
            List<User> list = new List<User>();
            string sql = "select * from Users";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable data = GetDataBySQLCommand(Command);

            foreach (DataRow dr in data.Rows)
            {
                int User_ID = Convert.ToInt32(dr["User_ID"].ToString());
                string First_Name = dr["First_Name"].ToString();
                string Last_Name = dr["Last_Name"].ToString();
                int Role_ID = Convert.ToInt32(dr["Role_ID"].ToString());
                string Username = dr["Username"].ToString();
                string Password = dr["Password"].ToString();
                string Phone = dr["Phone"].ToString();
                string Address = dr["Address"].ToString();
                bool Status = Convert.ToBoolean(dr["Status"].ToString());
                string Email = dr["Email"].ToString();
                list.Add(new User(User_ID, First_Name, Last_Name, Role_ID, Username, Password, Phone, Address,Email, Status));
            }
            return list;
        }
        
        public static User GetLogin(string username, string password)
        {
            User user = new User();
            string sql = "SELECT * FROM Users WHERE Username = @username and Password = @password";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            var p1 = new SqlParameter("@username", SqlDbType.VarChar)
            {
                Value = username
            };
            var p2 = new SqlParameter("@password", SqlDbType.VarChar)
            {
                Value = password
            };
            Command.Parameters.Add(p1);
            Command.Parameters.Add(p2);
            DataTable data = GetDataBySQLCommand(Command);
            if (data.Rows.Count> 0)
            {
                foreach (DataRow dr in data.Rows)
                {
                    int userID = Convert.ToInt32(dr["User_ID"].ToString());
                    string firstName = dr["First_Name"].ToString();
                    string lastName = dr["Last_Name"].ToString();
                    int roleID = Convert.ToInt32(dr["Role_ID"].ToString());
                    string userName = dr["Username"].ToString();
                    string passWord = dr["Password"].ToString();
                    string phone = dr["Phone"].ToString();
                    string address = dr["Address"].ToString();
                    bool status = Convert.ToBoolean(dr["Status"].ToString());
                    string Email = dr["Email"].ToString();
                    user = new User(userID, firstName, lastName, roleID, userName, passWord, phone, address, Email, status);
                }
            }
               return user;
        }

        public static List<User> GetUserRole()
        {
            List<User> listUserRole = new List<User>();
            string sql = "select * from Users join Roles on Users.Role_ID = Roles.Role_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            DataTable data = GetDataBySQLCommand(Command);

            int User_ID = 0;
            foreach (DataRow dr in data.Rows)
            {
                User_ID += 1;
                string First_Name = dr["First_Name"].ToString();
                string Last_Name = dr["Last_Name"].ToString();
                string Username = dr["Username"].ToString();
                string Password = dr["Password"].ToString();
                string Phone = dr["Phone"].ToString();
                string Address = dr["Address"].ToString();
                bool Status = Convert.ToBoolean(dr["Status"].ToString());
                string Role_Name = dr["Role_Name"].ToString();
                string Email = dr["Email"].ToString();
                listUserRole.Add(new User(User_ID, First_Name, Last_Name, Username, Password, Phone, Address, Status,Email, Role_Name));
            }
            return listUserRole;
        }

        public static List<User> getUserByUsername(string nameSearch)
        {
            List<User> listUserRole = new List<User>(); 
            string sql = "select * from Users join Roles on Users.Role_ID = Roles.Role_ID where Username LIKE @Username";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            var p = new SqlParameter("@Username", SqlDbType.VarChar)
            {
                Value = "%" + nameSearch + "%"
            };
            Command.Parameters.Add(p);
            DataTable data = GetDataBySQLCommand(Command);

            foreach (DataRow dr in data.Rows)
            {
                int User_ID = Convert.ToInt32(dr["User_ID"].ToString());
                string First_Name = dr["First_Name"].ToString();
                string Last_Name = dr["Last_Name"].ToString();
                string Username = dr["Username"].ToString();
                string Password = dr["Password"].ToString();
                string Phone = dr["Phone"].ToString();
                string Address = dr["Address"].ToString();
                bool Status = Convert.ToBoolean(dr["Status"].ToString());
                string Role_Name = dr["Role_Name"].ToString();
                string Email = dr["Email"].ToString();
                listUserRole.Add(new User(User_ID, First_Name, Last_Name, Username, Password, Phone, Address, Status,Email, Role_Name));
            }
            return listUserRole;
        }
        //Ham Add User
        public static void Regsiter(User user)
        { 
            String sql = "INSERT INTO Users VALUES(@First_Name,@Last_Name,@Role_ID,@Username,@Password,@Phone,@Address,@Email,@Status)";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@First_Name", SqlDbType.VarChar);
                p1.Value = user.First_Name;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Last_Name", SqlDbType.VarChar);
                p2.Value = user.Last_Name;
                Command.Parameters.Add(p2);
                SqlParameter p3 = new SqlParameter("@Username", SqlDbType.VarChar);
                p3.Value = user.Username;
                Command.Parameters.Add(p3);
                SqlParameter p4 = new SqlParameter("@Password", SqlDbType.VarChar);
                p4.Value = user.Password;
                Command.Parameters.Add(p4);
                SqlParameter p5 = new SqlParameter("@Phone", SqlDbType.VarChar);
                p5.Value = user.Phone;
                Command.Parameters.Add(p5);
                SqlParameter p6 = new SqlParameter("@Address", SqlDbType.VarChar);
                p6.Value = user.Address;
                Command.Parameters.Add(p6);
                SqlParameter p7 = new SqlParameter("@Role_ID", SqlDbType.Int);
                p7.Value = 4;
                Command.Parameters.Add(p7);
                SqlParameter p8 = new SqlParameter("@Status", SqlDbType.Bit);
                p8.Value =true;
                Command.Parameters.Add(p8);
                SqlParameter p9 = new SqlParameter("@Email", SqlDbType.VarChar);
                p9.Value = user.Email;
                Command.Parameters.Add(p9);
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
        public static User GetUserbyID(int ID)
        {
            User user = new User();
            String sql = "select First_Name,Last_Name,Phone,Address from Users where User_ID = @User_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@User_ID", SqlDbType.Int);
                p1.Value = ID;
                Command.Parameters.Add(p1);
                Command.Connection.Open();
                SqlDataReader dataReader = Command.ExecuteReader();
                if(dataReader.Read() != null)
                {
                    user.First_Name = dataReader.GetString(0);
                    user.Last_Name = dataReader.GetString(1);
                    user.Phone = dataReader.GetString(2);
                    user.Address = dataReader.GetString(3);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                Command.Connection.Close();
            }
            return user;
        } 

        // update user 
        public static void UpdateUser(User user)
        {
            String sql = "Update Users set First_Name=@First_Name,Last_Name=@Last_Name,Phone=@Phone,Address=@Address Where User_ID = @User_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                SqlParameter p1 = new SqlParameter("@First_Name", SqlDbType.VarChar);
                p1.Value = user.First_Name;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Last_Name", SqlDbType.VarChar);
                p2.Value = user.Last_Name;
                Command.Parameters.Add(p2);
                SqlParameter p3 = new SqlParameter("@Phone", SqlDbType.VarChar);
                p3.Value = user.Phone;
                Command.Parameters.Add(p3);
                SqlParameter p4 = new SqlParameter("@Address", SqlDbType.VarChar);
                p4.Value = user.Address;
                Command.Parameters.Add(p4);
                SqlParameter p5 = new SqlParameter("@User_ID", SqlDbType.Int);
                p5.Value = user.User_ID;
                Command.Parameters.Add(p5);
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
        //update password
        public static void UpdatePassword(String pass,String email)
        {
            String sql = "Update Users set Password=@Password  Where Email=@Email";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {
                
                SqlParameter p1 = new SqlParameter("@Email", SqlDbType.VarChar);
                p1.Value = email;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Password", SqlDbType.VarChar);
                p2.Value =pass;
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
        //RePassword
        public static void RePassword(String pass, int ID)
        {
            String sql = "Update Users set Password=@Password  Where User_ID=@User_ID";
            SqlCommand Command = new SqlCommand(sql, GetConnection());
            try
            {

                SqlParameter p1 = new SqlParameter("@User_ID", SqlDbType.VarChar);
                p1.Value = ID;
                Command.Parameters.Add(p1);
                SqlParameter p2 = new SqlParameter("@Password", SqlDbType.VarChar);
                p2.Value = pass;
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
    }
}
