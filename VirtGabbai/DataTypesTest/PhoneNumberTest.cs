using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for PhoneNumberTest and is intended
    ///to contain all PhoneNumberTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhoneNumberTest
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

        #region Equals Test

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            PhoneNumber target = new PhoneNumber(1, "0546137475", new PhoneType(1,"cell phone"));
            PhoneNumber comparedNumber = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            bool expected = true;
            bool actual;
            actual = target.Equals(comparedNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            PhoneNumber target = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            PhoneNumber comparedNumber = new PhoneNumber(2, "0546147485", new PhoneType(2, "house phone"));
            bool expected = false;
            bool actual;
            actual = target.Equals(comparedNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            PhoneNumber target = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            PhoneNumber comparedNumber = new PhoneNumber(2, "0546137475", new PhoneType(1, "cell phone"));
            bool expected = false;
            bool actual;
            actual = target.Equals(comparedNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffNumberEqualsTest()
        {
            PhoneNumber target = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            PhoneNumber comparedNumber = new PhoneNumber(1, "0546147485", new PhoneType(1, "cell phone"));
            bool expected = false;
            bool actual;
            actual = target.Equals(comparedNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffNumberTypeEqualsTest()
        {
            PhoneNumber target = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            PhoneNumber comparedNumber = new PhoneNumber(1, "0546137475", new PhoneType(2, "house phone"));
            bool expected = false;
            bool actual;
            actual = target.Equals(comparedNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void SomeDiffEqualsTest()
        {
            PhoneNumber target = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            PhoneNumber comparedNumber = new PhoneNumber(1, "0546147485", new PhoneType(2, "cell phone"));
            bool expected = false;
            bool actual;
            actual = target.Equals(comparedNumber);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ToStringTest

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            PhoneNumber target = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            string expected = "Number:\"" + target.Number + "\" " +
                              target.NumberType.ToString();
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
