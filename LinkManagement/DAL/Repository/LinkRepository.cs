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


        public LinkManagerContext LinkManagerContext
        {
            get { return context as LinkManagerContext; }
        }
    }
}