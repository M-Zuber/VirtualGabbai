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
    public class PrivilegeRepositoryTests
    {
        [TestMethod]
        public void Get_Nothing_In_Database_Returns_Empty_List()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository();

            var all = mock.Get();

            Assert.IsNotNull(all);
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public void Get_Returns_All_Data()
        {
            var expected = new List<Privilege> { new Privilege { Name = "first" }, new Privilege { Name = "second" } };
            var mock = RepositoryMocks.GetMockPrivilegeRepository(expected);

            var actual = mock.Get();

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository();

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(2, "second") });

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new Privilege(1, "First");
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { expected });

            var actual = mock.GetByID(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetByName_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository();

            var actual = mock.GetByName("first");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByName_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(2, "second") });

            var actual = mock.GetByName("first");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByName_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new Privilege(1, "First");
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { expected });

            var actual = mock.GetByName("first");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameExists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(1, "First") });

            Assert.IsTrue(mock.NameExists("first"));
        }

        [TestMethod]
        public void NameExists_No_Match_Found_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(1, "First") });

            Assert.IsFalse(mock.NameExists("Second"));
        }

        [TestMethod]
        public void NameExists_No_Data_Found_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository();

            Assert.IsFalse(mock.NameExists("first"));
        }

        [TestMethod]
        public void Exists_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository();

            Assert.IsFalse(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(1, "Admin") });

            Assert.IsTrue(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Item_Is_Null_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(1, "Admin") });

            Assert.IsFalse(mock.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(1, "Admin") });

            Assert.IsFalse(mock.Exists(new Privilege { ID = 2 }));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPrivilegeRepository(new List<Privilege> { new Privilege(1, "Admin") });

            Assert.IsTrue(mock.Exists(new Privilege { ID = 1 }));
        }
    }
}
