using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnoShop.Models;

namespace TechnoShop.Models.ViewModel
{
    public class UserViewModel : User
    {
        public IPagedList<User> PageList;
    }
}