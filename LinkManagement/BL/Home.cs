using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.ViewModels;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class Home : UnitOfWorkInitializer
    {
      
        public Home()
        {
        }


        public IEnumerable<Topic> GetTopicList()
        {
            return(unitOfWork.topic.GetAll());
        }


        public IEnumerable<TopicNode> GetTopicListTree()
        {
         
            var topicNodeList = unitOfWork.topic.GetAll()
                                .Select( x => new TopicNode()
                                    {
                                        TopicID = x.TopicID,
                                        TopicName = x.TopicName,
                                        ParentID = (int)x.ParentID,
                                        UserID = x.UserID
                                    })
                                    .ToList();
           
            return (CreateTopicTree(topicNodeList)); 
        }


        public List<TopicNode> CreateTopicTree(List<TopicNode> topicNodeList)
        {
            Action<TopicNode> setChildren = null;

            setChildren = parent =>
            {
                parent.subTopics = topicNodeList
                    .Where(childItem => childItem.ParentID == parent.TopicID)
                    .ToList();

                parent.subTopics
                    .ForEach(setChildren);
            };

            List<TopicNode> topicNodeTree = topicNodeList
                .Where(rootItem => rootItem.ParentID == 0)
                .ToList();

            topicNodeTree.ForEach(setChildren);

            return topicNodeTree;
        }


        public IEnumerable<TopicNode> GetRootTopicList()
        {
            return CastTopicToTopicNodeList(unitOfWork.topic.GetAllRootNode());
        }


        public List<TopicNode> CastTopicToTopicNodeList(IEnumerable<Topic> topicList)
        {
            return topicList.Select( x => new TopicNode()
                                    {
                                        TopicID = x.TopicID,
                                        TopicName = x.TopicName,
                                        ParentID = (int)x.ParentID,
                                        UserID = x.UserID,
                                        Icon = x.Icon,
                                        About = x.About
                                    })
                                    .ToList();
        }
    }
}