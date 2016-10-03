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
            return LinkManagerContext.Topics.Include("Links")
                    .Where(topics => topics.ParentID == parentID)
                        .OrderBy(topic => topic.Order);
        }


        public IEnumerable<Topic> GetImmediateParent(int topicID)
        {
        //    int parentTopicID = (int)LinkManagerContext.Topics
        //                .Select(topics => topics.ParentID).Last();

        //    return LinkManagerContext.Topics.Where(topic => topic.TopicID == parentTopicID);


        //   /* List<Topic> parentList = new List<Topic>();
        //    //return LinkManagerContext.Topics
        //    //        .Where(topics => topics.TopicID == topicID);
        //    if (topicID == 0)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        int parentTopicID = (int)LinkManagerContext.Topics
        //                .Select(topics => topics.ParentID).Last();

        //        return ((IEnumerable<Topic>)parentList.Add((Topic)GetAllParents(parentTopicID)));

        //        parentList.Add((Topic)LinkManagerContext.Topics.Where(topics => topics.TopicID == topicID));
        //        GetAllParents(parentTopicID);
        //    }*/
            return null;
        }

    }
}