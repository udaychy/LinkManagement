using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagement.DAL.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
         IEnumerable<Topic> GetAllRootNode();
         IEnumerable<Topic> GetImmediateChildren(int parentID);
         IEnumerable<Topic> GetImmediateParent(int topicID);
    }
}
