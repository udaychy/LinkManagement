using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkManagement.DAL.Interfaces
{
    public interface ILinkRepository : IRepository<Link>
    {
        void UpdateLinkStatus(int userID, int linkID);
        void AddNote(int userID, int linkID, string note);
        void AddRating(int userID, int linkID, int rating);
        void CountOneMoreVisitor(int linkID);
        void UpdateOverallRating(int linkID);
    }
}
