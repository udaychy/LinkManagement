using LinkManagement.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkManagement.BL
{
    public class Link : UnitOfWorkInitializer
    {

        public void UpdateLinkStatus(int userID, int linkID)
        {
            UnitOfWork.link.UpdateLinkStatus(userID, linkID);
            UnitOfWork.Commit();
        }


        public void AddNote(int userID, int linkID, string note)
        {
            UnitOfWork.link.AddNote(userID, linkID, note);
            UnitOfWork.Commit();
        }


        public void AddRating(int userID, int linkID, int rating)
        {
            UnitOfWork.link.AddRating(userID, linkID, rating);
            UnitOfWork.Commit();
            UnitOfWork.link.UpdateOverallRating(linkID);
            UnitOfWork.Commit();
        }


        public void CountOneMoreVisitor(int linkID)
        {
            UnitOfWork.link.CountOneMoreVisitor(linkID);
            UnitOfWork.Commit();
        }
    }
}