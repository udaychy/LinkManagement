using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


        public IEnumerable<Topic> GetAllRootNode()
        {
            return LinkManagerContext.Topics
                    .Where(topics => topics.ParentID == 0)
                        .OrderByDescending(topic => topic.Order);
        }


        public IEnumerable<Topic> GetImmediateChildren(int parentID)
        {
            return LinkManagerContext.Topics
                    .Where(topics => topics.ParentID == parentID)
                        .OrderByDescending(topic => topic.Order);
        }
    }
}