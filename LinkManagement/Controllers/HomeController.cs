using System.Web.Mvc;
using LinkManagement.BL;

namespace LinkManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetRootTopicList()
        {
            return Json(new Home().GetRootTopicList(), JsonRequestBehavior.AllowGet);
        }
    }
}
