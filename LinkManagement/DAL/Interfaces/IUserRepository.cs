using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkManagement.Model;
using System.Threading.Tasks;

namespace LinkManagement.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(User loginCredential);
    }
}
