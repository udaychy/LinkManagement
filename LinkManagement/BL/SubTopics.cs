using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class SubTopics : UnitOfWorkInitializer
    {
        
        public List<Topic> GetImmediateChildren(int parentID)
        {
            return (UnitOfWork.topic.GetImmediateChildren(parentID)).ToList();
        }

        //public List<TopicNode> GetAllParents(int topicID)
        //{
        //    return (new CastModel().TopicToTopicNodeList(UnitOfWork.topic.GetImmediateParent(topicID))).ToList();
        //}
    }
}