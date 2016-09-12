using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.ViewModels;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class Home
    {
        readonly private UnitOfWork unitOfWork;

        public Home()
        {
            unitOfWork = new UnitOfWork(new LinkManagerContext());
        }


        public IEnumerable<Topic> GetTopicList()
        {
            return(unitOfWork.topic.GetAll());
        }


        public IEnumerable<TopicNode> GetTopicListTree()
        {
           
            //change the TopicTree name to SeededTopicList
            return null;
        }
    }
}