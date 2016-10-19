
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
            }
        }
    }
}