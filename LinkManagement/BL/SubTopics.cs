using System.Collections.Generic;
using System.Linq;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class SubTopics : UnitOfWorkInitializer
    {
        
        public List<Topic> GetImmediateChildrenWithSubTopicCount(int parentID, int? userID)
        {
            var topicList = UnitOfWork.topic.GetImmediateChildren(parentID, userID).ToList();
            topicList.ForEach(t => t.Links = t.Links.OrderBy(l => l.Order).ToList());
            topicList.ForEach(topics => topics.SubTopicCount = UnitOfWork.topic.ChildCount(topics.TopicID));
            
            return topicList;
        }


        public List<Topic> GetBreadCrumbs(int topicID)
        {
            var parentsList = GetAllParents(topicID);
            parentsList.Add(UnitOfWork.topic.Get(topicID));
            return parentsList;
        }


        public List<Topic> GetAllParents(int topicID)
        {

            var parentsList = new List<Topic>();
            var topic = UnitOfWork.topic.Get(topicID);
            var parentID = topic.ParentID;

            while (parentID != 0)
            {
                topic = UnitOfWork.topic.Get(parentID);
                parentsList.Add(topic);
                parentID = topic.ParentID;  
            }
            parentsList.Reverse();
            return parentsList;
        }

    }
}