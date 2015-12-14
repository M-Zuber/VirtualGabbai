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
    public class YahrtziehtRepositoryTests
    {
        [TestMethod]
        public void Get_Nothing_In_Database_Returns_Empty_List()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository();

            var all = mock.Get();

            Assert.IsNotNull(all);
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public void Get_Returns_All_Data()
        {
            var expected = new List<Yahrtzieht> { new Yahrtzieht(), new Yahrtzieht() };
            var mock = RepositoryMocks.GetMockYahrtziehtRepository(expected);

            var actual = mock.Get();

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository();

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository(new List<Yahrtzieht> { new Yahrtzieht() });

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new Yahrtzieht { ID = 1 };
            var mock = RepositoryMocks.GetMockYahrtziehtRepository(new List<Yahrtzieht> { expected });

            var actual = mock.GetByID(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Exists_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository();

            Assert.IsFalse(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository(new List<Yahrtzieht> { new Yahrtzieht { ID = 1 } });

            Assert.IsTrue(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Item_Is_Null_Returns_False()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository(new List<Yahrtzieht> { new Yahrtzieht() });

            Assert.IsFalse(mock.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository(new List<Yahrtzieht> { new Yahrtzieht { ID = 1 } });

            Assert.IsFalse(mock.Exists(new Yahrtzieht { ID = 2 }));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockYahrtziehtRepository(new List<Yahrtzieht> { new Yahrtzieht { ID = 1 } });

            Assert.IsTrue(mock.Exists(new Yahrtzieht { ID = 1 }));
        }
    }
}
