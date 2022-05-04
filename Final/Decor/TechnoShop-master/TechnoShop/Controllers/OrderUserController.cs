using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoShop.Common;
using TechnoShop.DAO;

namespace TechnoShop.Controllers
{
    public class OrderUserController : Controller
    {
        // GET: OrderUser
        public ActionResult Index()
        {
            UserLogin session = (UserLogin)Session[CommonConstants.User_Session];
            return View(OrdersDAO.GetListOrder(session.UserID));
        }

        // GET: OrderUser/Details/5
        public ActionResult Details(int id)
        {
            return View(OrderDetailsDAO.GetListOrder(id));
        }

        // GET: OrderUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderUser/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
