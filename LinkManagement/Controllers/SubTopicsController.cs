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
        
        public JsonResult GetBreadCrumbsList(int topicID)
        {
            return Json(new SubTopics().GetBreadCrumbs(topicID), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult UpdateLinkStatus(int linkID)
        {
            if (Session["UserID"] != null)
            {
                new BL.Link().UpdateLinkStatus((int)Session["UserID"], linkID);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult AddNote(int linkID, string note)
        {
            if (Session["UserID"] != null)
            {
                new BL.Link().AddNote((int)Session["UserID"], linkID, note);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult AddRating(int linkID, int rating)
        {
            if (Session["UserID"] != null)
            {
                new BL.Link().AddRating((int)Session["UserID"], linkID, rating);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public void CountOneMoreVisitor(int linkID)
        {
            new BL.Link().CountOneMoreVisitor(linkID);   
        }
    }
}