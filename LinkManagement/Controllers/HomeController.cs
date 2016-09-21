using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkManagement.BL;
using LinkManagement.ViewModels;

namespace LinkManagement.Controllers
{
    public class HomeController : Controller
    {
        
        public JsonResult GetRootTopicList()
        {
            return Json(new Home().GetRootTopicList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View(new Home().GetTopicList());
        }
    }
}
