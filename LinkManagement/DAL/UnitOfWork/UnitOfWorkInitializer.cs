using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinkManagement.DAL.UnitOfWork
{
    public class UnitOfWorkInitializer
    {
        UnitOfWork _unitOfWork;

        public UnitOfWork UnitOfWork
        {
            get 
            {
                return _unitOfWork ?? (_unitOfWork = new UnitOfWork(new LinkManagerContext()));
                //return (_unitOfWork == null) ? (_unitOfWork = new UnitOfWork(new LinkManagerContext())) : _unitOfWork;
                //return _unitOfWork = new UnitOfWork(new LinkManagerContext());
            }
        }
    }
}