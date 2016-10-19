using System.Collections.Generic;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class Home : UnitOfWorkInitializer
    {
        public IEnumerable<Topic> GetRootTopicList()
        {
            return ( UnitOfWork.topic.GetImmediateChildren(0, null) );
        }

    }
}