using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Tests
{
    [TestClass]
    public class PhoneTypeRepositoryTests
    {
        [TestMethod]
        public void Get_Nothing_In_Database_Returns_Empty_List()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository();

            var all = mock.Get();

            Assert.IsNotNull(all);
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public void Get_Returns_All_Data()
        {
            var expected = new List<PhoneType> { new PhoneType { Name = "first" }, new PhoneType { Name = "second" } };
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(expected);

            var actual = mock.Get();

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository();

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(2, "second") });

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new PhoneType(1, "First");
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { expected });

            var actual = mock.GetByID(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetByName_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository();

            var actual = mock.GetByName("first");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByName_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(2, "second") });

            var actual = mock.GetByName("first");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByName_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new PhoneType(1, "First");
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { expected });

            var actual = mock.GetByName("first");

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NameExists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(1, "First") });

            Assert.IsTrue(mock.NameExists("first"));
        }

        [TestMethod]
        public void NameExists_No_Match_Found_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(1, "First") });

            Assert.IsFalse(mock.NameExists("Second"));
        }

        [TestMethod]
        public void NameExists_No_Data_Found_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository();

            Assert.IsFalse(mock.NameExists("first"));
        }

        [TestMethod]
        public void Exists_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository();

            Assert.IsFalse(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(1, "house") });

            Assert.IsTrue(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Item_Is_Null_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(1, "house") });

            Assert.IsFalse(mock.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(1, "house") });

            Assert.IsFalse(mock.Exists(new PhoneType(2, "cell")));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPhoneTypeRepository(new List<PhoneType> { new PhoneType(1, "house") });

            Assert.IsTrue(mock.Exists(new PhoneType(1, "house")));
        }
    }
}
