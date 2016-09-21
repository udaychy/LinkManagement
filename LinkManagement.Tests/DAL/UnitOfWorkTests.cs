using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkManagement.DAL.UnitOfWork;

namespace LinkManagement.Tests.DAL
{
    [TestClass]
    public class UnitOfWorkTests : UnitOfWorkInitializer
    {
        [TestMethod]
        public void GetAllRootNode_ShouldReturnAllTopicsOfParentIdZero()
        {
            
            var RootTopicList = unitOfWork.topic.GetAllRootNode();
            Assert.AreEqual(8, 8);
        }
    }
}
