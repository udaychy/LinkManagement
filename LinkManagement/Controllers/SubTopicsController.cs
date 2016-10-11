using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkManagement.BL;
using Newtonsoft.Json;

namespace LinkManagement.Controllers
{
    public class SubTopicsController : Controller
    {

        public String GetImmediateChildren(int parentID)
        {
             return JsonConvert.SerializeObject(new SubTopics().GetImmediateChildren(parentID), Formatting.Indented,
                                    new JsonSerializerSettings
                                    {
                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                    });
             
        }

        public JsonResult GetAllParents(int topicID)
        {
            return Json(new SubTopics().GetAllParents(topicID), JsonRequestBehavior.AllowGet);
        }


        public void UpdateLinkStatus(int linkID)
        {
            if (Session["UserID"] != null)
            {
                new SubTopics().UpdateLinkStatus((int)Session["UserID"], linkID);
            }
        }


        public void AddNote(int linkID, string note)
        {
            if (Session["UserID"] != null)
            {
                new SubTopics().AddNote((int)Session["UserID"], linkID, note);
            }
        }


        public void AddRating(int linkID, int rating)
        {
            if (Session["UserID"] != null)
            {
                new SubTopics().AddRating((int)Session["UserID"], linkID, rating);
            }
        }
    }
}