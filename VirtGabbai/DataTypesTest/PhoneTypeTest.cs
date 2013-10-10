using DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataTypesTest
{
    
    
    /// <summary>
    ///This is a test class for PhoneTypeTest and is intended
    ///to contain all PhoneTypeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhoneTypeTest
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

        #region Equals

        /// <summary>
        ///Everything is the same
        ///</summary>
        [TestMethod()]
        public void AllIsEqualsTest()
        {
            PhoneType target = new PhoneType(1,"one");
            object phoneTypeToCompare = new PhoneType(1, "one");
            bool expected = true;
            bool actual = target.Equals(phoneTypeToCompare);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///The id is different
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            PhoneType target = new PhoneType(1, "one");
            object phoneTypeToCompare = new PhoneType(12, "one");
            bool expected = false;
            bool actual = target.Equals(phoneTypeToCompare);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///The type name is different
        ///</summary>
        [TestMethod()]
        public void DiffTypeNameEqualsTest()
        {
            PhoneType target = new PhoneType(1, "one");
            object phoneTypeToCompare = new PhoneType(1, "one:1");
            bool expected = false;
            bool actual = target.Equals(phoneTypeToCompare);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Everything is different
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            PhoneType target = new PhoneType(1, "one");
            object phoneTypeToCompare = new PhoneType(12, "one:1");
            bool expected = false;
            bool actual = target.Equals(phoneTypeToCompare);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ToString

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            PhoneType target = new PhoneType(1, "cell phone");
            string expected = "Phone Type Name:\"cell phone\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
