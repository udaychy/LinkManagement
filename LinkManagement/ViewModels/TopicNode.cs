using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkManagement.ViewModels
{
    public class TopicNode
    {
        public int UserID { get; set; }
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public int ParentID { get; set; }
        public string Icon { get; set; }
        public string About { get; set; }

        public List<TopicNode> subTopics { get; set; }
    }
}