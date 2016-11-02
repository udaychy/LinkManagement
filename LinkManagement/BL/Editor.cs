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
                foreach (LinkManagement.Link link in updatedTopic.Links)
                {
                    
                    if (link.IsDeleted)
                    {
                        var linkToBeDeleted = UnitOfWork.link.Get(link.LinkID);
                        UnitOfWork.link.Remove(linkToBeDeleted);
                    }
                    else if (link.LinkID == 0 && !link.IsDeleted)
                    {
                        link.Link1 = link.Link1 ?? "";       
                        UnitOfWork.link.Add(link);
                    }
                    else
                    {
                        UnitOfWork.link.UpdateLink(link);
                    }
                }
            }
            UnitOfWork.Commit();
        }

        public void DeleteLinks(List<LinkManagement.Link> links)
        {
            links.ForEach(link =>
            {
                var linkToBeDeleted = UnitOfWork.link.Find(l => l.LinkID == link.LinkID).FirstOrDefault();
                UnitOfWork.link.Remove(linkToBeDeleted);
                return;
            });
        }

        public void DeleteTopic(int topicID)
        {

            var topicToBeDeleted = UnitOfWork.topic.GetTopicWithChildLinks(topicID);
            if (topicToBeDeleted.Links.Count > 0)
            {
                DeleteLinks(topicToBeDeleted.Links.ToList());
            }
            UnitOfWork.Commit();
            UnitOfWork.topic.Remove(topicToBeDeleted);
            UnitOfWork.Commit();
        }

        public void UpdateTopicOrder(List<Topic> topicList)
        {
            UnitOfWork.topic.UpdateTopicOrder(topicList);
            UnitOfWork.Commit();
        }
    }
}