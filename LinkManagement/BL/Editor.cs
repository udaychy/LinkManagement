using System;
using System.Collections.Generic;
using System.Linq;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class Editor : UnitOfWorkInitializer
    {
        public List<Topic> GetSubTopics(int topicID)
        {
            return UnitOfWork.topic.GetImmediateChildren(topicID, null).ToList();
        }

        public int AddNewTopic(Topic newTopic)
        {
            //ToDo:
            //ToDo: validate newTopic data
            UnitOfWork.topic.AddTopic(newTopic);
            UnitOfWork.Commit();
            return newTopic.TopicID;
        }


        public Topic GetTopicContents(int topicID)
        {
             return UnitOfWork.topic.GetTopicContents(topicID);
        }

        public void UpdateTopicContents(Topic updatedTopic)
        {
            UnitOfWork.topic.UpdateTopic(updatedTopic);
            
            if (updatedTopic.Links != null)
            {
                foreach (var link in updatedTopic.Links)
                {
                    UnitOfWork.link.UpdateLink(link);
                }
            }
            UnitOfWork.Commit();
        }
    }
}