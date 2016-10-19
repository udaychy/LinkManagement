
namespace LinkManagement.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(User loginCredential);
    }
}
