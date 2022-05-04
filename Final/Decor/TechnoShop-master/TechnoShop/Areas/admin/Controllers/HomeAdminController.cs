using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnoShop.Models;

namespace TechnoShop.Areas.admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private TechnoShopEntities db = new TechnoShopEntities();
        // GET: admin/HomeAdmin
        public ActionResult Index()
        {
         
            return View();
        }
    }
}