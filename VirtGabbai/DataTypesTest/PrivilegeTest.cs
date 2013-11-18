using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocalTypesTest
{
    
    
    /// <summary>
    ///This is a test class for PrivilegeTest and is intended
    ///to contain all PrivilegeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PrivilegeTest
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

        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            int id = 1;
            string privilegeName = "administrator";
            Privilege target = new Privilege(id, privilegeName);
            object obj = new Privilege(id, privilegeName);
            bool expected = true;
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
            int id = 1;
            string privilegeName = "administrator";
            Privilege target = new Privilege(id, privilegeName);
            object obj = new Privilege(2, "guest");
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            int id = 1;
            string privilegeName = "administrator";
            Privilege target = new Privilege(id, privilegeName);
            object obj = new Privilege(2, privilegeName);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPrivilegeNameEqualsTest()
        {
            int id = 1;
            string privilegeName = "administrator";
            Privilege target = new Privilege(id, privilegeName);
            object obj = new Privilege(id, "guest");
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
        public void ToStringTest()
        {
            int id = 1;
            string privilegeName = "administrator";
            Privilege target = new Privilege(id, privilegeName);
            string expected = "administrator";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
