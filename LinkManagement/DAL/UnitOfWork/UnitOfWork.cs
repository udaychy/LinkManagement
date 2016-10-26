using LinkManagement.DAL.Interfaces;
using LinkManagement.DAL.Repository;
using System;
using System.Data.Entity.Validation;

namespace LinkManagement.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LinkManagerContext context;
        private IUserRepository _user;
        private ILinkRepository _link;
        private ITopicRepository _topic;

        public UnitOfWork(LinkManagerContext contextInitializer)
        {
            context = contextInitializer;
        }


        public IUserRepository user
        {
            get 
            {
                return (_user == null) ? (_user = new UserRepository(context)) : _user;   
            }
        }


        public ILinkRepository link
        {
            get
            {
                return (_link == null) ? (_link = new LinkRepository(context)) : _link;   
            }
        }


        public ITopicRepository topic
        {
            get
            {
                return (_topic == null) ? (_topic = new TopicRepository(context)) : _topic;   
            }
        }


        public int Commit()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (DbEntityValidationException  e){
                Console.WriteLine(e);

            }
            return 0;
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}