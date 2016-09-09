using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkManagement.ViewModels
{
    public class HomePageModel
    {
        public IEnumerable<User> users { get; set; }
        public IEnumerable<Topic> topics { get; set; }
        public IEnumerable<Link> links { get; set; }
    }
}