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
        public ActionResult FlatLinkView()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(new Home().GetTopicList());
        }

        public ActionResult Index()
        {
            Home homeData = new Home();

            TopicTree topicTree = 
                new TopicTree()
                {
                    seed = 0,
                    topics = homeData.GetTopicList()
                };

            return View(topicTree);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
