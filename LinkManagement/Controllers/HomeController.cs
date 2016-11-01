using System.Web.Mvc;
using LinkManagement.BL;
using Newtonsoft.Json;

namespace LinkManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public string GetRootTopicList()
        {
            return JsonConvert.SerializeObject(new Home().GetRootTopicList(), Formatting.Indented,
                                    new JsonSerializerSettings
                                    {
                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                    });
        }
    }
}
