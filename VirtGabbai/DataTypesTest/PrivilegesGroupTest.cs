using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LocalTypesTest
{
    
    
    /// <summary>
    ///This is a test class for PrivilegesGroupTest and is intended
    ///to contain all PrivilegesGroupTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PrivilegesGroupTest
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
            int id = 0; // TODO: Initialize to an appropriate value
            List<Privilege> privileges = null; // TODO: Initialize to an appropriate value
            PrivilegesGroup target = new PrivilegesGroup(id, privileges); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = true; // TODO: Initialize to an appropriate value
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
            int id = 0; // TODO: Initialize to an appropriate value
            List<Privilege> privileges = null; // TODO: Initialize to an appropriate value
            PrivilegesGroup target = new PrivilegesGroup(id, privileges); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
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
            int id = 0; // TODO: Initialize to an appropriate value
            List<Privilege> privileges = null; // TODO: Initialize to an appropriate value
            PrivilegesGroup target = new PrivilegesGroup(id, privileges); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPrivilegesEqualsTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            List<Privilege> privileges = null; // TODO: Initialize to an appropriate value
            PrivilegesGroup target = new PrivilegesGroup(id, privileges); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region GroupsPrivilegesToDbString Tests

        /// <summary>
        ///A test for GroupsPrivilegesToDbString
        ///</summary>
        [TestMethod()]
        public void GroupsPrivilegesToDbStringTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            List<Privilege> privileges = null; // TODO: Initialize to an appropriate value
            PrivilegesGroup target = new PrivilegesGroup(id, privileges); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GroupsPrivilegesToDbString();
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
            int id = 0; // TODO: Initialize to an appropriate value
            List<Privilege> privileges = null; // TODO: Initialize to an appropriate value
            PrivilegesGroup target = new PrivilegesGroup(id, privileges); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
