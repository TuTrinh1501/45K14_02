using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoShop.DAO;
using TechnoShop.Models;
using PagedList;
using TechnoShop.Models.ViewModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using TechnoShop.Common;
using System.Diagnostics;

namespace TechnoShop.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index(ProductListViewModel model, int? page, string searchString,int? category,string price)
        {
            
            List<Product> listProduct = new List<Product>();
            //check null or white space when search
            if (!String.IsNullOrWhiteSpace(searchString) && category.GetValueOrDefault(0) == 0 && String.IsNullOrWhiteSpace(price))
            {
                listProduct = ProductDAO.SearchProductByName(searchString.Trim());
            }
            else if(String.IsNullOrWhiteSpace(searchString) && category.GetValueOrDefault(0) != 0 && String.IsNullOrWhiteSpace(price))
            {
              
                listProduct = ProductDAO.SearchProductByCategory(category.Value);
            }else if (String.IsNullOrWhiteSpace(searchString) && category.GetValueOrDefault(0) == 0 && !String.IsNullOrWhiteSpace(price))
            {
                
                string[] arrListStr = price.Split(',');
                Debug.WriteLine(arrListStr[0] +"   "+ arrListStr[1]);
                listProduct = ProductDAO.SearchProductByPrice(float.Parse(arrListStr[0].ToString()), float.Parse(arrListStr[1].ToString()));
            }
            else {
                listProduct = ProductDAO.GetList();
            }
            List<Category> listCategory = CategoryDAO.GetList();
            ViewBag.ListCategory = listCategory;
            //show paged list
            int pageNumber = (page ?? 1);
            int pageSize = 12;

            ViewBag.ListProduct = listProduct.Count;
            ViewBag.Max = ProductDAO.GetMax();
            ViewBag.Min = ProductDAO.GetMin();
            model.PageList = listProduct.ToList().ToPagedList(pageNumber,pageSize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Product product = ProductDAO.GetProductByID(id);
            return View(product);
        }

        [CheckCredential(Role_ID = "1")]
        public ActionResult Create()
        {
            //show list category 
            List<Category> listCategory = CategoryDAO.GetList();
            ViewBag.listCategoryOption = new SelectList(listCategory, "Category_ID", "Category_Name");
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileWrapper postedFile, String Description) 
        {
            var listCategory = CategoryDAO.GetList();
            string fileName = "";
            string newFileName = "";
            string fileExtension = "";
            string[] acceptedExtensions = { ".jpg", ".jpeg", ".png"};
            string savePath = "/Images/product/";
            string dirPath = Server.MapPath(savePath);
            //Create a directory if the directory does not exist
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            if (postedFile != null)
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                //Get image file name, file extension and user name
                fileName = Path.GetFileName(postedFile.FileName);
                fileExtension = Path.GetExtension(postedFile.FileName);
            }
            // Validation input text and upload file
            if (!ModelState.IsValid && postedFile == null)
            {
                ViewBag.listCategoryOption = new SelectList(listCategory, "Category_ID", "Category_Name");
                return View(product);
            }
            else if (!ModelState.IsValid && postedFile != null)
            {
                //check extension 
                if (!acceptedExtensions.Contains(fileExtension))
                {
                    ViewBag.error = "Extension of your chosen file is not correct";
                }
                ViewBag.listCategoryOption = new SelectList(listCategory, "Category_ID", "Category_Name");
                return View(product);
            }
            else if (ModelState.IsValid && postedFile != null)
            {
                //check extension 
                if (!acceptedExtensions.Contains(fileExtension))
                {
                    ViewBag.error = "Extension of your chosen file is not correct";
                    ViewBag.listCategoryOption = new SelectList(listCategory, "Category_ID", "Category_Name");
                    return View(product);
                }
                else
                {
                    //Use user name, time, extension to generate a new file name and save
                    newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExtension;
                    postedFile.SaveAs(dirPath + "/" + newFileName);
                }
            }
            //Validation exists
            if (ProductDAO.IsExistProduct(product.Product_Name))
            {
                ViewBag.error = "This product exists!!!";
                ViewBag.listCategoryOption = new SelectList(listCategory, "Category_ID", "Category_Name");
                return View(product);
            }
            //create product model
            string productName = Request["Product_Name"];
            String description = Description;
            int categoryID = Convert.ToInt32(Request["Category_Name"]);
            double price = Convert.ToDouble(Request["Price"]);
            int quantity = Convert.ToInt32(Request["Quantity"]);
            string image = postedFile.FileName;
            double percentOfDiscount = Convert.ToDouble(Request["PercentOfDiscount"]);
            Product p = new Product(productName, description, categoryID, price, quantity, image, percentOfDiscount);
            //add
            ProductDAO productDAO = new ProductDAO();
            productDAO.AddProduct(p);
            //list category 
            ViewBag.listCategoryOption = new SelectList(listCategory, "Category_ID", "Category_Name");
            ViewBag.mess = "Create successfully!";
            return View();
        }

        [HttpPost]
        public JsonResult CkeditorSaveImage(HttpPostedFileWrapper upload)
        {
            string savePath = "/Images/product/";
            string dirPath = Server.MapPath(savePath);
            //Create a directory if the directory does not exist
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            //Get image file name, extensions and user name
            var fileName = Path.GetFileName(upload.FileName);
            string fileExt = Path.GetExtension(fileName).ToLower();
            //Use user name, time, extension to generate a new file name and save
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            upload.SaveAs(dirPath + "/" + newFileName);
            //After the upload is successful, we also need to return to the JSON format response
            return Json(new
            {
                uploaded = 1,
                fileName = newFileName,
                url = savePath + newFileName
            });
        }
    }
}