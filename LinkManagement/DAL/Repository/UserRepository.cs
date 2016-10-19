using System.Linq;
using LinkManagement.DAL.Interfaces;

namespace LinkManagement.DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LinkManagerContext context)
            : base(context)
        {
        }


        public User GetUser(User loginCredential)
        {
            return LinkManagerContext.Users
                    .Where(u => u.UserName == loginCredential.UserName && u.Password == loginCredential.Password ).FirstOrDefault();
        }


        public LinkManagerContext LinkManagerContext
        {
            get { return context as LinkManagerContext; }
        }
    }
}