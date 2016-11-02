using System.Collections.Generic;
using System.Linq;
using LinkManagement.DAL.Interfaces;

namespace LinkManagement.DAL.Repository
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(LinkManagerContext context)
            : base(context)
        {
        }


        public LinkManagerContext LinkManagerContext
        {
            get { return context as LinkManagerContext; }
        }


        public int ChildCount(int id)
        {
            return LinkManagerContext.Topics
                    .Where(topics => topics.ParentID == id).Count();
                    
        }


        public IEnumerable<Topic> GetImmediateChildren(int parentID, int? userID)
        {
            var topicList = LinkManagerContext.Topics.Include("Links")
                    .Where(topic => topic.ParentID == parentID)
                        .OrderBy(topic => topic.Order).ToList();
            topicList.ForEach(t => t.Links = t.Links.OrderBy(l => l.Order).ToList());

            if (userID != null)
            {
                foreach (var topic in topicList)
                {
                    foreach (var link in topic.Links)
                    {
                        link.LinkUserMappings = LinkManagerContext.LinkUserMappings
                            .Where(l => l.UserID == userID && l.LinkID == link.LinkID).ToList();
                    }
                }
            }
            return topicList;
        }


        public Topic GetTopicContents(int topicID)
        {
             var topicContent = LinkManagerContext.Topics.Include("Links")
                .Where(topic => topic.TopicID == topicID).ToList();
             topicContent.ForEach(t => t.Links = t.Links.OrderBy(l => l.Order).ToList());

             return topicContent.FirstOrDefault();
        }


        public void AddTopic(Topic newTopic)
        {
            LinkManagerContext.Topics.Add(newTopic);
        }


        public void UpdateTopic(Topic updatedTopic)
        {
            var topicToBeUpdated = LinkManagerContext.Topics.Find(updatedTopic.TopicID);
            LinkManagerContext.Entry(topicToBeUpdated).CurrentValues.SetValues(updatedTopic);
        }

        public Topic GetTopicWithChildLinks(int topicID)
        {
            return LinkManagerContext.Topics.Include("Links").Where(t => t.TopicID == topicID).FirstOrDefault();
        }

        public void UpdateTopicOrder(List<Topic> topics)
        {
            topics.ForEach(topic =>
            {
              var topicToBeUpdated =  LinkManagerContext.Topics.Where(t => t.TopicID == topic.TopicID).FirstOrDefault();
              topicToBeUpdated.Order = topic.Order;
            }
            );
        }
    }
}