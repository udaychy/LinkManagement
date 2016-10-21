using System.Web.Mvc;
using LinkManagement.BL;
using Newtonsoft.Json;

namespace LinkManagement.Controllers
{
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
            var updatedTopicObject = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Topic>(updatedTopic.ToString());
            new Editor().UpdateTopicContents(updatedTopicObject);
        }
    }
}