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
    }
}