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
    internal static class RepositoryMocks
    {
        internal static PhoneTypeRepository GetMockPhoneTypeRepository(List<PhoneType> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<PhoneType>>().SetupData(data ?? new List<PhoneType>());
            mockContext.Setup(c => c.PhoneTypes).Returns(mockSet.Object);
            return new PhoneTypeRepository(mockContext.Object);
        }

        internal static DonationRepository GetMockDonationRepository(List<Donation> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<Donation>>().SetupData(data ?? new List<Donation>());
            mockContext.Setup(c => c.Donations).Returns(mockSet.Object);
            return new DonationRepository(mockContext.Object);
        }

        internal static YahrtziehtRepository GetMockYahrtziehtRepository(List<Yahrtzieht> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<Yahrtzieht>>().SetupData(data ?? new List<Yahrtzieht>());
            mockContext.Setup(c => c.Yahrtziehts).Returns(mockSet.Object);
            return new YahrtziehtRepository(mockContext.Object);
        }

        internal static UserRepository GetMockUserRepository(List<User> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<User>>().SetupData(data ?? new List<User>());
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);
            return new UserRepository(mockContext.Object);
        }

        internal static PhoneNumberRepository GetMockPhoneNumberRepository(List<PhoneNumber> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<PhoneNumber>>().SetupData(data ?? new List<PhoneNumber>());
            mockContext.Setup(c => c.PhoneNumbers).Returns(mockSet.Object);
            return new PhoneNumberRepository(mockContext.Object);
        }

        internal static AccountRepository GetMockAccountRepository(List<Account> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<Account>>().SetupData(data ?? new List<Account>());
            mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);
            return new AccountRepository(mockContext.Object);
        }

        internal static PrivilegeGroupRepository GetMockPrivilegesGroupRepository(List<PrivilegesGroup> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<PrivilegesGroup>>().SetupData(data ?? new List<PrivilegesGroup>());
            mockContext.Setup(c => c.PrivilegesGroups).Returns(mockSet.Object);
            return new PrivilegeGroupRepository(mockContext.Object);
        }

        internal static PrivilegeRepository GetMockPrivilegeRepository(List<Privilege> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<Privilege>>().SetupData(data ?? new List<Privilege>());
            mockContext.Setup(c => c.Privileges).Returns(mockSet.Object);
            return new PrivilegeRepository(mockContext.Object);
        }

        internal static PersonRepository GetMockPersonRepository(List<Person> data = null)
        {
            var mockContext = new Mock<ZeraLeviContext>();
            var mockSet = new Mock<DbSet<Person>>().SetupData(data ?? new List<Person>());
            mockContext.Setup(c => c.People).Returns(mockSet.Object);
            return new PersonRepository(mockContext.Object);
        }
    }
}
