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
            return( unitOfWork.topic.GetAll() );
        }


        public IEnumerable<TopicNode> GetTopicListTree()
        {         
            return ( CreateTopicTree( new CastModel().TopicToTopicNodeList( unitOfWork.topic.GetAll() ) ) ); 
        }


        public IEnumerable<TopicNode> GetRootTopicList()
        {
            return ( new CastModel().TopicToTopicNodeList( unitOfWork.topic.GetAllRootNode() ) );
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

    }
}