using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkManagement.ViewModels
{
    public class TopicTree
    {
        public int seed { get; set; }
        public IEnumerable<Topic> topics { get; set; }
    }
}