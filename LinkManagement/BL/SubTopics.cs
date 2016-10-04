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
            var topicList = UnitOfWork.topic.GetImmediateChildren(parentID).ToList();

            topicList.ForEach(topics => topics.SubTopicCount = UnitOfWork.topic.ChildCount(topics.TopicID));
            return topicList;
            
        }

        public List<Topic> GetAllParents(int topicID)
        {

            var parentsList = new List<Topic>();
            var topic = UnitOfWork.topic.Get(topicID);
            var parentID = topic.ParentID;

            parentsList.Add(topic);

            while (parentID != 0)
            {
                topic = UnitOfWork.topic.Get(parentID);
                parentsList.Add(topic);
                parentID = topic.ParentID;  
            }
            return parentsList.OrderBy(topics => topics.TopicID).ToList();
        }
    }
}