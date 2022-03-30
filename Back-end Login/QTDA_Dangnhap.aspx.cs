using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//B1: 
using System.Data.SqlClient;
using System.Data;

public partial class QTDA_Dangnhap : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-BAQ7TS24;Initial Catalog=QTDA_Dangnhap;Integrated Security=True");
        try
        {
            connect.Open();
            string tk = TextBox_TaiKhoan.Text;
            string mk = TextBox_MatKhau.Text;
            string sql = "select * from Dangnhap where TenDangNhap = '" + tk + "' and MatKhau = '" + mk + "'";
            SqlCommand cmd = new SqlCommand(sql, connect);
            SqlDataReader data = cmd.ExecuteReader();
            if (data.Read() == true)
            {
                Response.Write("<script>alert('Thành công');</script>");
            }
            else
            {
                Response.Write("<script>alert('Đéo thành công');</script>");
            }
        }
        catch
        {
            Response.Write("<script>alert('Lỗi kết nối');</script>");          
        }
    }   
}