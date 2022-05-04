using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnoShop.Models;

namespace TechnoShop.Models.ViewModel
{
    public class ProductListViewModel : Product
    {
        public IPagedList<Product> PageList;
    }
}