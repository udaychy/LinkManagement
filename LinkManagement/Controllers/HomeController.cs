using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkManagement.BL;
using LinkManagement.ViewModels;
using LinkManagement.Models;

namespace LinkManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult FlatLinkView()
        {
            ViewBag.Message = "Flat view of all the links";

            return View( new Home().GetTopicList());
        }


        public JsonResult GetTopicListTree()
        {
            List<TopicNode> topicNodeList = new List<TopicNode>();
            var topicList = new Home().GetTopicList().ToList();

            foreach(var topic in topicList)
            {
                topicNodeList.Add(
                    new TopicNode() { 

                        TopicID = topic.TopicID,
                        TopicName = topic.TopicName,
                        ParentID = (int)topic.ParentID,
                        UserID = topic.UserID
                    
                 });
            }

            /////////////////
            Action<TopicNode> SetChildren = null;
            SetChildren = parent =>
            {
                parent.subTopics = topicNodeList
                    .Where(childItem => childItem.ParentID == parent.TopicID)
                    .ToList();

                //Recursively call the SetChildren method for each child.
                parent.subTopics
                    .ForEach(SetChildren);
            };

            //Initialize the hierarchical list to root level items
            List<TopicNode> TopicNodeTree = topicNodeList
                .Where(rootItem => rootItem.ParentID == 0)
                .ToList();

            //Call the SetChildren method to set the children on each root level item.
            TopicNodeTree.ForEach(SetChildren);
            ///////////////////
            return Json(TopicNodeTree, JsonRequestBehavior.AllowGet);
        }


        public List<TopicNode> CreateTopicHierarchy()
        {
            return null;
        }


        public ActionResult Index()
        {
            Home homeData = new Home();

            return View(new Home().GetTopicList());
        }
    }
}
