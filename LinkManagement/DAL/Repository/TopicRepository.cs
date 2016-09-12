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
    }
}