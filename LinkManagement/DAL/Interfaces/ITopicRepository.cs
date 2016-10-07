using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagement.DAL.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
         IEnumerable<Topic> GetImmediateChildren(int parentID);
         int ChildCount(int id);

         void UpdateLinkStatus(int topicID, int linkID, bool isChecked);
    }
}
