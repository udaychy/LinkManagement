using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class Home : UnitOfWorkInitializer
    {
 
        public IEnumerable<Topic> GetTopicList()
        {
            return( UnitOfWork.topic.GetAll() );
        }

        public IEnumerable<Topic> GetRootTopicList()
        {
            return ( UnitOfWork.topic.GetImmediateChildren(0) );
        }

    }
}