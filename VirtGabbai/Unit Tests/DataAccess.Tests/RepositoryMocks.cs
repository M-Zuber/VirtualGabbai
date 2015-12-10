using DataCache.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    //TODO move this into a seperate project
    public static class RepositoryMocks
    {
        public static PhoneTypeRepository GetMockPhoneTypeRepository(List<PhoneType> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<PhoneType>>().SetupData(data ?? new List<PhoneType>());
            mockContext.Setup(c => c.PhoneTypes).Returns(mockSet.Object);
            return new PhoneTypeRepository(mockContext.Object);
        }
    }
}
