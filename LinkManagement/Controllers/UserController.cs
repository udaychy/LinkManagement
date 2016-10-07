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
        public JsonResult AuthenticateUser(LoginData data)
        {
            
            return Json(new Users().AuthenticateUser(data));
        }


        public bool AuthenticateToken(string token)
        {
            return new Users().IsAuthenticatedToken(token);
        }
    }
}
