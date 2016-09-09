using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.DAL.Interfaces;
using LinkManagement.DAL.Repository.Generic;

namespace LinkManagement.DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LinkManagerContext context)
            : base(context)
        {
        }


        public IEnumerable<User> GetListByName(string fName)
        {
            return LinkManagerContext.Users.Where(c => c.FName == fName);
        }


        public LinkManagerContext LinkManagerContext
        {
            get { return context as LinkManagerContext; }
        }
    }
}