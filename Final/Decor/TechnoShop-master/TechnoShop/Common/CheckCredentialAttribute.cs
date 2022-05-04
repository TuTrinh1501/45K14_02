using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnoShop.Common
{
    public class CheckCredentialAttribute : AuthorizeAttribute
    {
        public string Role_ID { set; get; }
        int checkSession = 0;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            List<string> Admin = new List<string> { "1", "2", "3", "4" };
            List<string> Manager = new List<string> { "2", "3", "4" };
            List<string> Staff = new List<string> { "3", "4" };
            List<string> Customer = new List<string> { "4" };
            UserLogin session = (UserLogin)HttpContext.Current.Session[CommonConstants.User_Session];
            RoleLogin role = (RoleLogin)HttpContext.Current.Session[CommonConstants.ROLE_Session];
            if (session == null)
            {
                checkSession = 1;
                return false;
            }
            if (role.Role == 1)
            {
                if (Admin.Contains(this.Role_ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (role.Role == 2)
            {
                if (Manager.Contains(this.Role_ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (role.Role == 3)
            {
                if (Staff.Contains(this.Role_ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (role.Role == 4)
            {
                if (Customer.Contains(this.Role_ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (checkSession == 1)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Login/Login.cshtml"
                };

            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml"
                };
            }

        }
    }
}