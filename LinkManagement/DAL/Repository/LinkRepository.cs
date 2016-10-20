using System;
using System.Collections.Generic;
using System.Linq;
using LinkManagement.DAL.Interfaces;

namespace LinkManagement.DAL.Repository
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(LinkManagerContext context)
            : base(context)
        { 
        }

        public LinkUserMapping GetLinkAssociatedToUser(int userID, int linkID)
        {
            return LinkManagerContext.LinkUserMappings
                                     .Where(l => l.LinkID == linkID && l.UserID == userID)
                                     .FirstOrDefault();
        }


        public void UpdateLink(Link updatedLink)
        {
            var linkToBeUpdated = LinkManagerContext.Links.Find(updatedLink.LinkID);
            LinkManagerContext.Entry(linkToBeUpdated).CurrentValues.SetValues(updatedLink);
        }


        public void UpdateLinkStatus(int userID, int linkID)
        {

            var linkToBeUpdated = GetLinkAssociatedToUser(userID, linkID);

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
            var linkToBeUpdated = GetLinkAssociatedToUser(userID, linkID);

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
            var linkToBeUpdated = GetLinkAssociatedToUser(userID, linkID);

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
        

        public LinkManagerContext LinkManagerContext
        {
            get { return context as LinkManagerContext; }
        }
    }
}