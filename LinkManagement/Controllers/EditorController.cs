using System;
using System.Web;
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
    }
}