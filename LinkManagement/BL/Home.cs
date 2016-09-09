using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkManagement.ViewModels;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.BL
{
    public class Home
    {
        readonly private UnitOfWork unitOfWork;

        public Home()
        {
            unitOfWork = new UnitOfWork(new LinkManagerContext());
        }


        public HomePageModel GetContents()
        {
            return(new HomePageModel()
                         {
                             users = unitOfWork.user.GetAll(),
                             topics = unitOfWork.topic.GetAll(),
                             links = unitOfWork.link.GetAll()
                         }
                  ); 
        }


        public IEnumerable<Topic> GetTopicList()
        {
            return(unitOfWork.topic.GetAll());
        }
    }
}