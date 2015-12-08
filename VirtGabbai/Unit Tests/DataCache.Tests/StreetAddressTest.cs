using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataCache.Tests
{
    [TestClass()]
    public class StreetAddressTest
    {
        #region C'tor Tests

        /// <summary>
        ///A test for StreetAddress Constructor
        ///</summary>
        [TestMethod()]
        public void StreetAddressConstructorTest()
        {
            string address = ";1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            Assert.AreEqual("", target.ApartmentNumber);
            Assert.AreEqual("1894", target.House);
            Assert.AreEqual("beacon st", target.Street);
            Assert.AreEqual("brookline", target.City);
            Assert.AreEqual("MA", target.State);
            Assert.AreEqual("USA", target.Country);
            Assert.AreEqual("02445", target.Zipcode);
        }
        
        /// <summary>
        ///A test for StreetAddress Constructor with explicit parameters
        ///</summary>
        [TestMethod()]
        public void StreetAddressExplicitConstructorTest()
        {
            StreetAddress target = new StreetAddress("", "1894", "beacon st", "brookline", "MA", "USA", "02445");
            Assert.AreEqual("", target.ApartmentNumber);
            Assert.AreEqual("1894", target.House);
            Assert.AreEqual("beacon st", target.Street);
            Assert.AreEqual("brookline", target.City);
            Assert.AreEqual("MA", target.State);
            Assert.AreEqual("USA", target.Country);
            Assert.AreEqual("02445", target.Zipcode);
        }

        #endregion

        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";

            StreetAddress target = new StreetAddress(address);
            StreetAddress obj = new StreetAddress(address);

            Assert.IsTrue(target.Equals(obj));
            Assert.IsTrue(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffStateEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress("1;1894;beacon st;brookline;;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress(";;;;;;");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffAptNoEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress(";1894;beacon st;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffHouseNoEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress("1;;beacon st;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffStreetEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress("1;1894;;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffCityEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress("1;1894;beacon st;;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffCountryEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress("1;1894;beacon st;brookline;ma;;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void ZipcodeDiffEqualsTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            StreetAddress obj = new StreetAddress("1;1894;beacon st;brookline;ma;usa;");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        [TestMethod]
        public void StreetAddress_Equals_Null_Returns_False()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(null));
        }

        [TestMethod]
        public void StreetAddress_Equals_Non_StreetAddress_Returns_False()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(0));
        }

        [TestMethod]
        public void StreetAddress_Equals_Same_Ref_Returns_True()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var other = target;

            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
        }
        #endregion

        #region ToString Tests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ApartmentAndStateToStringTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            string expected = "1894 beacon st\tApartment #1\n" +
                              "brookline MA USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }


        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ApartmentNoStateToStringTest()
        {
            StreetAddress target = new StreetAddress("1;1894;beacon st;brookline;;usa;02445");
            string expected = "1894 beacon st\tApartment #1\n" +
                              "brookline USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void StateNoApartmentToStringTest()
        {
            StreetAddress target = new StreetAddress(";1894;beacon st;brookline;ma;usa;02445");
            string expected = "1894 beacon st\n" +
                              "brookline MA USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void NoStateOrApartmentToStringTest()
        {
            StreetAddress target = new StreetAddress(";1894;beacon st;brookline;;usa;02445");
            string expected = "1894 beacon st\n" +
                              "brookline USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }

        #endregion
        
        #region ToDbString Tests

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod()]
        public void AptAndStateToDbStringTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";

            StreetAddress target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod()]
        public void NoAptYesStateToDbStringTest()
        {
            string address = ";1894;beacon st;brookline;ma;usa;02445";

            StreetAddress target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod()]
        public void NoStateYesAptToDbStringTest()
        {
            string address = "1;1894;beacon st;brookline;;usa;02445";

            StreetAddress target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod()]
        public void NoStateNoAptToDbStringTest()
        {
            string address = ";1894;beacon st;brookline;;usa;02445";

            StreetAddress target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }
        
        #endregion
    }
}
