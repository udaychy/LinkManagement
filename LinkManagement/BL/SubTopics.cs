using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.ViewModels;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class SubTopics : UnitOfWorkInitializer
    {
        public SubTopics()
        {
        }


        public List<TopicNode> GetImmediateChildren(int parentID)
        {
            return( new CastModel().TopicToTopicNodeList( unitOfWork.topic.GetImmediateChildren(parentID) )).ToList();
        }
    }
}