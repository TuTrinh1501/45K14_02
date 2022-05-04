using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TechnoShop.Common;
using TechnoShop.DAO;
using TechnoShop.Models;
using TechnoShop.Models.ViewModel;

namespace TechnoShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult LogIn()
        {
            var UserLogin = (UserLogin)HttpContext.Session[CommonConstants.User_Session];
            if (UserLogin == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session[CommonConstants.ROLE_Session] = null;
            Session[CommonConstants.User_Session] = null;
            return RedirectToAction("LogIn", "User");
        }

        //GET: User/ListUser
        public ActionResult ListUser(UserViewModel model, int? page, string nameSearch)
        {
            List<User> listUserRole = new List<User>();
            if (!String.IsNullOrWhiteSpace(nameSearch))
            {
                listUserRole = UserDAO.getUserByUsername(nameSearch.Trim());
            }
            else
            {
                listUserRole = UserDAO.GetUserRole();
            }
            ViewBag.listUserRole = listUserRole.Count;
            var pageNumber = page ?? 1;
            int pageSize = 10;
            model.PageList = listUserRole.ToList().ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        public ActionResult index()
        {
            return View();
        }

        public ActionResult CreateHtml()
        {
            var model = new User();
            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            string username = Request["user"];
            string password = Request["pass"];
            User checkUser = UserDAO.GetLogin(username, password);
            if (checkUser != null && checkUser.Status == true)
            {
                var RoleSession = new RoleLogin();
                RoleSession.Role = checkUser.Role_ID;
                var UserSession = new UserLogin();
                UserSession.UserID = checkUser.User_ID;
                UserSession.UserName = checkUser.Username;
                // add session
                Session.Add(CommonConstants.ROLE_Session, RoleSession);
                Session.Add(CommonConstants.User_Session, UserSession);
               switch (RoleSession.Role)
                {
                    case 1:
                        return RedirectToAction("Index", "admin/HomeAdmin");
                        break;
                    case 2:
                        return RedirectToAction("Index", "Home");
                        break;
                    case 3:
                        return RedirectToAction("Index", "Home");
                        break;
                    case 4:
                        return RedirectToAction("Index", "Home");
                        break;
                }
                   
            }
            ViewBag.Message = "Vui Lòng Kiểm tra lại tên đăng nhập";
            return View();
        }

        public bool isUser(string login)
        {
            if (login.Length <= 5) return false;
            string numPart = login.Substring(login.Length - 5);
            foreach (Char c in numPart)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Regsiter([Bind(Include = "First_Name,Last_Name,Username,Password,Phone,Address,Email")] User user)
        {
            
                UserDAO.Regsiter(user);
            TempData["AlertType"] = "alert-success";
            TempData["AlertMessage"] = "Đăng Kí Thành Công";
            return RedirectToAction("LogIn", "User");
        }
        public ActionResult Edit()
        {
            UserLogin session = (UserLogin)Session[CommonConstants.User_Session];
            var user = UserDAO.GetUserbyID(session.UserID);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "First_Name,Last_Name,Phone,Address")] User user)
        {
            UserLogin session = (UserLogin)Session[CommonConstants.User_Session];
            user.User_ID = session.UserID;
            UserDAO.UpdateUser(user);
            TempData["AlertType"] = "alert-success";
            TempData["AlertMessage"] = "Thành Công";
            return View(user);
        }
        [HttpGet]
        public ActionResult ForgotPassword(String email)
        {

            String pass = CreatePassword(8);
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 25;
            string subject = "Forgot Password";
            string body = string.Format("Bạn vừa nhận được liên hê từ: TechNoShop <b></b><br/>Mật Khẩu Mới Của Bạn Là: {0}<br/>Bạn Vui Lòng Đăng Nhập Lại Và Đổi Lại Mật Khẩu</br>", pass);

            EmailService service = new EmailService();
            bool kq = service.Send(smtpHost, smtpPort, email, subject, body);
            if (kq)
            {
                UserDAO.UpdatePassword(pass, email);
                return RedirectToAction("LogIn", "User");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult changePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changePass(string pass,string repass)
        {
            if (pass.Equals(repass))
            {
                UserLogin session = (UserLogin)Session[CommonConstants.User_Session];
                int User = session.UserID;
                UserDAO.RePassword(pass, User);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("changePass");
            }
           
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}