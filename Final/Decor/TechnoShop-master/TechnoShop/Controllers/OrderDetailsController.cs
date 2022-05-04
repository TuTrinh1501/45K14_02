using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoShop.Common;
using TechnoShop.DAO;
using TechnoShop.Models;

namespace TechnoShop.Controllers
{
    public class OrderDetailsController : Controller
    {
        // GET: OrderDetails
        public ActionResult Index()
        {
            int User_Id = 0;
           UserLogin session = (UserLogin)Session[CommonConstants.User_Session];
            User_Id = session.UserID;
            int Order_ID = 0;
            Order_ID = OrdersDAO.getOrderIDbyUser(User_Id);
            ViewBag.listproduct = ProductDAO.GetList();
            return View(OrderDetailsDAO.GetListOrder(Order_ID));
        }
        [HttpGet]
        public ActionResult AddToCart(int Product_id, int quantity)
        {
           
            int User_Id = 0;
            UserLogin session = (UserLogin)Session[CommonConstants.User_Session]; // neu session null tra ve login
            if(session == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            User_Id = session.UserID; // else tra ve thuc hien tiep
            int Order_ID = 0;
            Order_ID = OrdersDAO.getOrderIDbyUser(User_Id);// lay order id
            Debug.Write("loi trong " + Order_ID);
            if(Order_ID == 0)  // neu orrd id = 0 thi tao moi 
            {
                Order orders = new Order();
                orders.User_ID = User_Id;
                orders.Status = 1;
                orders.Total = 0;
                orders.Created_Time =  DateTime.Now;
                Debug.Write(DateTime.Now);
                Order_ID = OrdersDAO.NewOrder(orders);
            } 
            // them vao order details
            Order_Detail order_Detail = new Order_Detail();
            order_Detail.Order_ID = Order_ID;
            order_Detail.Product_ID = Product_id;
            order_Detail.Quantity = quantity;
            OrderDetailsDAO.NewOrderDetails(order_Detail);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult deletebyID(int id)
        {
            OrderDetailsDAO.DeleteCartbyID(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddQuantity(int id,int quantity)
        {
            int update = quantity + 1;
            OrderDetailsDAO.UpdateOrdersDetails(id, update);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SubQuantity(int id,int quantity)
        {
            int update = quantity - 1;
            if(update == 0)
            {
                OrderDetailsDAO.DeleteCartbyID(id);
            }
            else
            {
                OrderDetailsDAO.UpdateOrdersDetails(id, update);
            }
           
            return RedirectToAction("Index");
        }
    }
}