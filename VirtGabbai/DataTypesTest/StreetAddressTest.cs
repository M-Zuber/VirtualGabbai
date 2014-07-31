using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocalTypesTest
{
    
    
    /// <summary>
    ///This is a test class for StreetAddressTest and is intended
    ///to contain all StreetAddressTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StreetAddressTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


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
            string address = ";1894;beacon st;brookline;ma;usa;02445";
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
            object obj = new StreetAddress(address);
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffStateEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = "1;1894;beacon st;brookline;;usa;02445";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = ";;;;;;";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffAptNoEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = ";1894;beacon st;brookline;ma;usa;02445";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffHouseNoEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = "1;;beacon st;brookline;ma;usa;02445";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffStreetEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = "1;1894;;brookline;ma;usa;02445";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffCityEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = "1;1894;beacon st;;ma;usa;02445";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffCountryEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = "1;1894;beacon st;brookline;ma;;02445";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void ZipcodeDiffEqualsTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string otherAddress = "1;1894;beacon st;brookline;ma;usa;";
            object obj = new StreetAddress(otherAddress);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ToString Tests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ApartmentAndStateToStringTest()
        {
            string address = "1;1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string expected = "1894 beacon st\tApartment #1\n" +
                              "brookline MA USA\n02445";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ApartmentNoStateToStringTest()
        {
            string address = "1;1894;beacon st;brookline;;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string expected = "1894 beacon st\tApartment #1\n" +
                              "brookline USA\n02445";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void StateNoApartmentToStringTest()
        {
            string address = ";1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string expected = "1894 beacon st\n" +
                              "brookline MA USA\n02445";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void NoStateOrApartmentToStringTest()
        {
            string address = ";1894;beacon st;brookline;;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string expected = "1894 beacon st\n" +
                              "brookline USA\n02445";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
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
            string actual;
            actual = target.ToDbString();
            Assert.AreEqual(address, actual, true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod()]
        public void NoAptYesStateToDbStringTest()
        {
            string address = ";1894;beacon st;brookline;ma;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string actual;
            actual = target.ToDbString();
            Assert.AreEqual(address, actual, true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod()]
        public void NoStateYesAptToDbStringTest()
        {
            string address = "1;1894;beacon st;brookline;;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string actual;
            actual = target.ToDbString();
            Assert.AreEqual(address, actual, true);
        }

        /// <summary>
        ///A test for ToDbString
        ///</summary>
        [TestMethod()]
        public void NoStateNoAptToDbStringTest()
        {
            string address = ";1894;beacon st;brookline;;usa;02445";
            StreetAddress target = new StreetAddress(address);
            string actual;
            actual = target.ToDbString();
            Assert.AreEqual(address, actual, true);
        }
        
        #endregion
    }
}
