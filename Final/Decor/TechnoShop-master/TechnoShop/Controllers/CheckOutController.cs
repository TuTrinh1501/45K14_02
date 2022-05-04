using MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoShop.Common;
using TechnoShop.DAO;
using TechnoShop.Models;

namespace TechnoShop.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult Index()
        {
            UserLogin session = (UserLogin)Session[CommonConstants.User_Session];
            var user = UserDAO.GetUserbyID(session.UserID);
            return View(user);
        } 
        // ham check out
        public ActionResult Checkout(float total)
        {
            UserLogin session = (UserLogin)Session[CommonConstants.User_Session]; // lay session 
            int orderID = OrdersDAO.getOrderIDbyUser(session.UserID); // lay order ID
            Order order = new Order();
            order.Order_ID = orderID;
            order.Status = 2;
            order.Created_Time = DateTime.Now;
            order.Total = total;
            DAO.OrdersDAO.UpdateOrders(order); // add order
            return RedirectToAction("Index", "OrderUser");
        }
        // cancel 
        public ActionResult Cancel(int id)
        { 
            //updtae Stt Orders lai
            DAO.OrdersDAO.UpdateSTTOrders(id, 0);
            return RedirectToAction("Index", "OrderUser");
        }
        public ActionResult Payment()
        {
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            string partnerCode = "MOMO7K7G20211228";
            string accessKey = "SVbtyysg2FxvUfvz";
            string serectkey = "BSmAXqo05I8q2OsR6nQY6KRkNIIy2nT1";
            string orderInfo = "Thanh Toán Mua Hàng Le Van Thang";
            string returnUrl = "https://webhook.site/b3088a6a-2d17-4f8d-a383-71389a6c600b";
            string notifyurl = "https://webhook.site/b3088a6a-2d17-4f8d-a383-71389a6c600b"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test
            string redirectUrl = "https://localhost:44390/CheckOut";
            string ipnUrl = "https://localhost:44390/CheckOut";
            string requestType = "captureWallet";
            string amount = "1000";
            string orderId = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            string rawHash = "accessKey=" + accessKey +
                  "&amount=" + amount +
                  "&extraData=" + extraData +
                  "&ipnUrl=" + ipnUrl +
                  "&orderId=" + orderId +
                  "&orderInfo=" + orderInfo +
                  "&partnerCode=" + partnerCode +
                  "&redirectUrl=" + redirectUrl +
                  "&requestId=" + requestId +
                  "&requestType=" + requestType
                  ;
            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //Khi thanh toán xong ở cổng thanh toán Momo, Momo sẽ trả về một số thông tin, trong đó có errorCode để check thông tin thanh toán
        //errorCode = 0 : thanh toán thành công (Request.QueryString["errorCode"])
        //Tham khảo bảng mã lỗi tại: https://developers.momo.vn/#/docs/aio/?id=b%e1%ba%a3ng-m%c3%a3-l%e1%bb%97i
        public ActionResult ConfirmPaymentClient()
        {
            //hiển thị thông báo cho người dùng
            return View();
        }

        public ActionResult SavePayment(float total)
        {
            string url = Request.Url.PathAndQuery;
            if (!url.Contains("message=Success"))
            {
                return RedirectToAction("Index");
            }
            else
            {
                UserLogin session = (UserLogin)Session[CommonConstants.User_Session]; // lay session 
                int orderID = OrdersDAO.getOrderIDbyUser(session.UserID); // lay order ID
                Order order = new Order();
                order.Order_ID = orderID;
                order.Status = 2;
                order.Created_Time = DateTime.Now;
                order.Total = total;
                DAO.OrdersDAO.UpdateOrders(order); // add order
                return RedirectToAction("Index", "OrderUser");
            }
               
        }
    }
}