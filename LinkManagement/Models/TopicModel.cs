using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkManagement.Models
{
    public class TopicModel
    {
        public int UserID { get; set; }
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public int ParentID { get; set; }
    }
}