using System.Web.Mvc;
using LinkManagement.BL;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LinkManagement.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        public string GetSubTopics(int topicID)
        {
            return JsonConvert.SerializeObject(new Editor().GetSubTopics(topicID), Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                     });
        }


        public JsonResult AddNewTopic(Topic newTopic)
        {
            return Json(new Editor().AddNewTopic(newTopic), JsonRequestBehavior.AllowGet);
        }


        public string GetTopicContents(int topicID)
        {
            return JsonConvert.SerializeObject(new Editor().GetTopicContents(topicID), Formatting.Indented,
                                     new JsonSerializerSettings
                                     {
                                         PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                     });
        }
    
        [ValidateInput(false)]
        public void UpdateTopicContent(string updatedTopic)
        {
            var updatedTopicObject = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Topic>(updatedTopic);
            new Editor().UpdateTopicContents(updatedTopicObject);
        }

        public void DeleteTopic(int topicID)
        {
            new Editor().DeleteTopic(topicID);
        }

        public void UpdateTopicOrder(string[] topicOrder)
        {
            List<Topic> topicList = new List<Topic>();
            foreach (var topic in topicOrder)
            {
                topicList.Add(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Topic>(topic));
            }

            new Editor().UpdateTopicOrder(topicList);
        }
    }
}