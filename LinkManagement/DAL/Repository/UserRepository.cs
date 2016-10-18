using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.Model;
using LinkManagement.DAL.Interfaces;

namespace LinkManagement.DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LinkManagerContext context)
            : base(context)
        {
        }


        //public bool IsAuthenticatedToken(Guid token)
        //{
        //    return LinkManagerContext.Users
        //        .Where(u=> u.AccessToken == token).Select(u=> u.UserID).FirstOrDefault() > 0;

        //}

        //public User AddAccessToken(User logedInUser)
        //{
        //    logedInUser.AccessToken = Guid.NewGuid();
        //    return logedInUser;
        //}

        
        public User GetUser(User loginCredential)
        {
        
            return LinkManagerContext.Users
                    .Where(u => u.UserName == loginCredential.UserName && u.Password == loginCredential.Password ).FirstOrDefault();
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