using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Tests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        [TestMethod]
        public void Get_Nothing_In_Database_Returns_Empty_List()
        {
            var mock = RepositoryMocks.GetMockAccountRepository();

            var all = mock.Get();

            Assert.IsNotNull(all);
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public void Get_Returns_All_Data()
        {
            var expected = new List<Account> { new Account(), new Account() };
            var mock = RepositoryMocks.GetMockAccountRepository(expected);

            var actual = mock.Get();

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockAccountRepository();

            var actual = mock.GetById(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockAccountRepository(new List<Account> { new Account() });

            var actual = mock.GetById(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new Account { Id = 1 };
            var mock = RepositoryMocks.GetMockAccountRepository(new List<Account> { expected });

            var actual = mock.GetById(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Exists_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockAccountRepository();

            Assert.IsFalse(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockAccountRepository(new List<Account> { new Account { Id = 1 } });

            Assert.IsTrue(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Item_Is_Null_Returns_False()
        {
            var mock = RepositoryMocks.GetMockAccountRepository(new List<Account> { new Account() });

            Assert.IsFalse(mock.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockAccountRepository(new List<Account> { new Account { Id = 1 } });

            Assert.IsFalse(mock.Exists(new Account { Id = 2 }));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockAccountRepository(new List<Account> { new Account { Id = 1 } });

            Assert.IsTrue(mock.Exists(new Account { Id = 1 }));
        }
    }
}
