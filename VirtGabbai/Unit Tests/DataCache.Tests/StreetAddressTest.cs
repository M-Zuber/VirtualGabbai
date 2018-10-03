using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCache.Tests
{
    [TestClass]
    public class StreetAddressTest
    {
        #region C'tor Tests

        /// <summary>
        ///A test for StreetAddress Constructor
        ///</summary>
        [TestMethod]
        public void StreetAddressConstructorTest()
        {
            const string address = ";1894;beacon st;brookline;ma;usa;02445";
            var target = new StreetAddress(address);
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
        [TestMethod]
        public void StreetAddressExplicitConstructorTest()
        {
            var target = new StreetAddress("", "1894", "beacon st", "brookline", "MA", "USA", "02445");
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
        [TestMethod]
        public void AllSameEqualsTest()
        {
            const string address = "1;1894;beacon st;brookline;ma;usa;02445";

            var target = new StreetAddress(address);
            var obj = new StreetAddress(address);

            Assert.IsTrue(target.Equals(obj));
            Assert.IsTrue(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffStateEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress("1;1894;beacon st;brookline;;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void AllDiffEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress(";;;;;;");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffAptNoEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress(";1894;beacon st;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffHouseNoEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress("1;;beacon st;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffStreetEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress("1;1894;;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffCityEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress("1;1894;beacon st;;ma;usa;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffCountryEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress("1;1894;beacon st;brookline;ma;;02445");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void ZipcodeDiffEqualsTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var obj = new StreetAddress("1;1894;beacon st;brookline;ma;usa;");

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        [TestMethod]
        public void StreetAddress_Equals_Null_Returns_False()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");

            Assert.IsFalse(target.Equals(null));
        }

        [TestMethod]
        public void StreetAddress_Equals_Non_StreetAddress_Returns_False()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(target.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void StreetAddress_Equals_Same_Ref_Returns_True()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            var other = target;

            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
        }
        #endregion

        #region ToString Tests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ApartmentAndStateToStringTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;ma;usa;02445");
            const string expected = "1894 beacon st\tApartment #1\n" +
                              "brookline MA USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ApartmentNoStateToStringTest()
        {
            var target = new StreetAddress("1;1894;beacon st;brookline;;usa;02445");
            const string expected = "1894 beacon st\tApartment #1\n" +
                              "brookline USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void StateNoApartmentToStringTest()
        {
            var target = new StreetAddress(";1894;beacon st;brookline;ma;usa;02445");
            const string expected = "1894 beacon st\n" +
                              "brookline MA USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void NoStateOrApartmentToStringTest()
        {
            var target = new StreetAddress(";1894;beacon st;brookline;;usa;02445");
            const string expected = "1894 beacon st\n" +
                              "brookline USA\n02445";

            Assert.AreEqual(expected, target.ToString());
        }

        #endregion

        #region ToDbString Tests

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod]
        public void AptAndStateToDbStringTest()
        {
            const string address = "1;1894;beacon st;brookline;ma;usa;02445";

            var target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod]
        public void NoAptYesStateToDbStringTest()
        {
            const string address = ";1894;beacon st;brookline;ma;usa;02445";

            var target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod]
        public void NoStateYesAptToDbStringTest()
        {
            const string address = "1;1894;beacon st;brookline;;usa;02445";

            var target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod]
        public void NoStateNoAptToDbStringTest()
        {
            const string address = ";1894;beacon st;brookline;;usa;02445";

            var target = new StreetAddress(address);

            Assert.AreEqual(address, target.ToDbString(), true);
        }

        #endregion
    }
}
