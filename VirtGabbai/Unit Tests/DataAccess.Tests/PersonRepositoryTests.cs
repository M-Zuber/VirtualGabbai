using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Tests
{
    [TestClass]
    public class PersonRepositoryTests
    {
        [TestMethod]
        public void Get_Nothing_In_Database_Returns_Empty_List()
        {
            var mock = RepositoryMocks.GetMockPersonRepository();

            var all = mock.Get();

            Assert.IsNotNull(all);
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public void Get_Returns_All_Data()
        {
            var expected = new List<Person> { new Person(), new Person() };
            var mock = RepositoryMocks.GetMockPersonRepository(expected);

            var actual = mock.Get();

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPersonRepository();

            var actual = mock.GetById(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPersonRepository(new List<Person> { new Person() });

            var actual = mock.GetById(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new Person { Id = 1 };
            var mock = RepositoryMocks.GetMockPersonRepository(new List<Person> { expected });

            var actual = mock.GetById(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Exists_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPersonRepository();

            Assert.IsFalse(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPersonRepository(new List<Person> { new Person { Id = 1 } });

            Assert.IsTrue(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Item_Is_Null_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPersonRepository(new List<Person> { new Person() });

            Assert.IsFalse(mock.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPersonRepository(new List<Person> { new Person { Id = 1 } });

            Assert.IsFalse(mock.Exists(new Person { Id = 2 }));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPersonRepository(new List<Person> { new Person { Id = 1 } });

            Assert.IsTrue(mock.Exists(new Person { Id = 1 }));
        }
    }
}
