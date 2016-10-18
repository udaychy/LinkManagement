using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.DAL.Interfaces;

namespace LinkManagement.DAL.Repository
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(LinkManagerContext context)
            : base(context)
        { 
        }


        public void UpdateLinkStatus(int userID, int linkID)
        {
           
            LinkUserMapping linkToBeUpdated = LinkManagerContext.LinkUserMappings
                                        .Where(l => l.LinkID == linkID && l.UserID == userID)
                                        .FirstOrDefault();
            if (linkToBeUpdated != null)
            {
                linkToBeUpdated.Status = !linkToBeUpdated.Status;
            }
            else
            {
                LinkManagerContext.LinkUserMappings.Add(new LinkUserMapping() { 
                                        LinkID = linkID,
                                        Status = true,
                                        UserID = userID
                                      });
            }
            
        }


        public void AddNote(int userID, int linkID, string note)
        {
            LinkUserMapping linkToBeUpdated = LinkManagerContext.LinkUserMappings
                .Where(l => l.LinkID == linkID && l.UserID == userID)
                .FirstOrDefault();

            if (linkToBeUpdated != null)
            {
                linkToBeUpdated.Note = note;
            }
            else
            {
                LinkManagerContext.LinkUserMappings.Add(new LinkUserMapping()
                {
                    LinkID = linkID,
                    Note = note,
                    UserID = userID
                });
            }
            
        }


        public void AddRating(int userID, int linkID, int rating)
        {
            LinkUserMapping linkToBeUpdated = LinkManagerContext.LinkUserMappings
                .Where(l => l.LinkID == linkID && l.UserID == userID)
                .FirstOrDefault();

            if (linkToBeUpdated != null)
            {
                linkToBeUpdated.Rating = rating;
            }
            else
            {
                LinkManagerContext.LinkUserMappings.Add(new LinkUserMapping()
                {
                    LinkID = linkID,
                    Rating = rating,
                    UserID = userID
                });
            }
            
        }


        public void UpdateOverallRating(int linkID)
        {
            //only taking those links whose rating is not null
            var linkList = LinkManagerContext.LinkUserMappings
                .Where(l => l.LinkID == linkID && l.Rating != null);

            Link link = LinkManagerContext.Links
                    .Where(l => l.LinkID == linkID)
                    .FirstOrDefault();
            var sum = (Double)linkList.Sum(r => r.Rating);
            var count = linkList.Count();
            link.OverallRating = (Double)linkList.Sum(r => r.Rating) / linkList.Count(); 
        }


        public void CountOneMoreVisitor(int linkID)
        {
            Link linkToBeUpdated = LinkManagerContext.Links
                .Where(l => l.LinkID == linkID)
                .FirstOrDefault();

            if (linkToBeUpdated != null)
            {
                linkToBeUpdated.NoOfTimesVisited = linkToBeUpdated.NoOfTimesVisited + 1;
            }
            
        }
        //void UpdateLinkUserMappingRow(LinkUserMapping updatedData)
        //{
        //    LinkUserMapping linkToBeUpdated = LinkManagerContext.LinkUserMappings
        //        .Where(l => l.LinkID == updatedData.LinkID && l.UserID == updatedData.UserID)
        //        .FirstOrDefault();

        //    if (linkToBeUpdated != null)
        //    {
        //        linkToBeUpdated.Note = updatedData.Note ?? ;
        //        linkToBeUpdated.Rating = updatedData.Rating??;
        //        linkToBeUpdated.Status = 
        //    }
        //    else
        //    {

        //    }
        //}

        public LinkManagerContext LinkManagerContext
        {
            get { return context as LinkManagerContext; }
        }
    }
}