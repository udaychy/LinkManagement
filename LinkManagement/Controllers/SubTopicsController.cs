using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkManagement.BL;
using LinkManagement.ViewModels;

namespace LinkManagement.Controllers
{
    public class SubTopicsController : Controller
    {
        public JsonResult GetImmediateChildren(int parentID)
        {
            return Json(new SubTopics().GetImmediateChildren(parentID), JsonRequestBehavior.AllowGet);
        }
    }
}