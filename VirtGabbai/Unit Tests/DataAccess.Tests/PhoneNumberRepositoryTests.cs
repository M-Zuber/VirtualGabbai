﻿using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tests
{
    [TestClass()]
    public class PhoneNumberRepositoryTests
    {
        [TestMethod]
        public void Get_Nothing_In_Database_Returns_Empty_List()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository();

            var all = mock.Get();

            Assert.IsNotNull(all);
            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public void Get_Returns_All_Data()
        {
            var expected = new List<PhoneNumber> { new PhoneNumber(), new PhoneNumber() };
            var mock = RepositoryMocks.GetMockPhoneNumberRepository(expected);

            var actual = mock.Get();

            Assert.IsNotNull(actual);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository();

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository(new List<PhoneNumber> { new PhoneNumber() });

            var actual = mock.GetByID(1);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetByID_Returns_The_Item_With_The_Given_ID()
        {
            var expected = new PhoneNumber { ID = 1 };
            var mock = RepositoryMocks.GetMockPhoneNumberRepository(new List<PhoneNumber> { expected });

            var actual = mock.GetByID(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Exists_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository();

            Assert.IsFalse(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository(new List<PhoneNumber> { new PhoneNumber { ID = 1 } });

            Assert.IsTrue(mock.Exists(1));
        }

        [TestMethod]
        public void Exists_Item_Is_Null_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository(new List<PhoneNumber> { new PhoneNumber() });

            Assert.IsFalse(mock.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository(new List<PhoneNumber> { new PhoneNumber { ID = 1 } });

            Assert.IsFalse(mock.Exists(new PhoneNumber { ID = 2 }));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var mock = RepositoryMocks.GetMockPhoneNumberRepository(new List<PhoneNumber> { new PhoneNumber { ID = 1 } });

            Assert.IsTrue(mock.Exists(new PhoneNumber { ID = 1 }));
        }
    }
}
