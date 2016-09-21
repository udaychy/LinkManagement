using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.ViewModels;

namespace LinkManagement.BL
{
    public class CastModel
    {
        public List<TopicNode> TopicToTopicNodeList(IEnumerable<Topic> topicList)
        {
            return topicList.Select(topic => new TopicNode()
                                     {
                                         TopicID = topic.TopicID,
                                         TopicName = topic.TopicName,
                                         ParentID = (int)topic.ParentID,
                                         UserID = topic.UserID,
                                         Icon = topic.Icon,
                                         About = topic.About
                                     })
                                      .ToList();
        }
    }
}