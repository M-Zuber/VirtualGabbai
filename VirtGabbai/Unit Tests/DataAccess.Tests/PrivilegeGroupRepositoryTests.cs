using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    [TestClass()]
    public class PrivilegesGroupRepositoryTests
    {
        [TestMethod]
        public void Get_Nothing_In_Database_Returns_Empty_List()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository();

            var all = mock.Get();

            Assert.IsNotNull(all);
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public void Get_Returns_All_Data()
        {
            var expected = new List<PrivilegesGroup> { new PrivilegesGroup { GroupName = "first" }, new PrivilegesGroup { GroupName = "second" } };
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(expected);

            var actual = mock.Get();

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository();

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(2, "second", new List<Privilege> { new Privilege(1, "admin")}) });

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new PrivilegesGroup(1, "First", new List<Privilege> { new Privilege(1, "admin") });
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { expected });

            var actual = mock.GetByID(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetByGroupName_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository();

            var actual = mock.GetByGroupName("first");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByGroupName_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(2, "second", new List<Privilege> { new Privilege(1, "admin") }) });

            var actual = mock.GetByGroupName("first");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByGroupName_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new PrivilegesGroup(1, "First", new List<Privilege> { new Privilege(1, "admin") });
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { expected });

            var actual = mock.GetByGroupName("first");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GroupNameExists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(1, "First", new List<Privilege> { new Privilege(1, "admin") }) });

            Assert.IsTrue(mock.GroupNameExists("first"));
        }

        [TestMethod]
        public void GroupNameExists_No_Match_Found_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(1, "First", new List<Privilege> { new Privilege(1, "admin") }) });

            Assert.IsFalse(mock.GroupNameExists("Second"));
        }

        [TestMethod]
        public void GroupNameExists_No_Data_Found_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository();

            Assert.IsFalse(mock.GroupNameExists("first"));
        }

        [TestMethod]
        public void Exists_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository();

            Assert.IsFalse(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(1, "Admin", new List<Privilege>()) });

            Assert.IsTrue(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Item_Is_Null_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(1, "Admin", new List<Privilege>()) });

            Assert.IsFalse(mock.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(1, "Admin", new List<Privilege>()) });

            Assert.IsFalse(mock.Exists(new PrivilegesGroup { ID = 2}));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPrivilegesGroupRepository(new List<PrivilegesGroup> { new PrivilegesGroup(1, "Admin", new List<Privilege>()) });

            Assert.IsTrue(mock.Exists(new PrivilegesGroup { ID = 1}));
        }
    }
}
