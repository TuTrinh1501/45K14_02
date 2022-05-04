using TechnoShop.Models;
using TechnoShop.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoShop.Models.ViewModel;
using PagedList;

namespace TechnoShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(ProductListViewModel model, int? page, string searchString)
        {   
            List<Product> listProduct = new List<Product>();
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                listProduct = ProductDAO.SearchProductByName(searchString.Trim());
            }
            else
            {
                listProduct = ProductDAO.GetList();
            }
            List<Category> listCategory = CategoryDAO.GetList();
            ViewBag.ListCategory = listCategory.Take(3);
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            ViewBag.ListProduct = listProduct.Count;
            var r = new Random();
            ViewBag.ListRandomProduct = listProduct.OrderBy(u => r.Next()).Take(8);
            model.PageList = listProduct.ToList().ToPagedList(pageNumber, pageSize);

            ViewBag.Dropdown = new SelectList(CategoryDAO.GetList(), "Category_ID", "Category_Name");
            ViewBag.Max = ProductDAO.GetMax();
            ViewBag.Min = ProductDAO.GetMin();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}