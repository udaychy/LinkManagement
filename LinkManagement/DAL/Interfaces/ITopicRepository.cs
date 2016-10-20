using System.Collections.Generic;

namespace LinkManagement.DAL.Interfaces
{
    public interface ITopicRepository : IRepository<Topic>
    {
         IEnumerable<Topic> GetImmediateChildren(int parentID, int? userID);
         int ChildCount(int id);
         void AddTopic(Topic newTopic);
         Topic GetTopicContents(int topicID);
         void UpdateTopic(Topic updatedTopic);
    }
}
