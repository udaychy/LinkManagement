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
            var topicList = UnitOfWork.topic.GetImmediateChildren(parentID, (int?)HttpContext.Current.Session["UserID"]).ToList();
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