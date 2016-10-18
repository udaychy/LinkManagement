using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkManagement.BL;
using LinkManagement.Model;
using System.Web.Security;

namespace LinkManagement.Controllers
{
    public class UserController : Controller
    {
        public JsonResult AuthenticateUser(User data)
        {    
            return Json(new Users().AuthenticateUser(data), JsonRequestBehavior.AllowGet);
        }


        public void LogOut()
        {
             FormsAuthentication.SignOut();
             Session["UserID"] = null;
        }


        public JsonResult IsLogedIn()
        {
            return Json(User.Identity.IsAuthenticated && (Session["UserID"] != null), JsonRequestBehavior.AllowGet);
        }
    }
}
