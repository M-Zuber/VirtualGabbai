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
            int id = 1;
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            PrivilegesGroup target = new PrivilegesGroup(id, groupName, privileges);
            object obj = new PrivilegesGroup(id, groupName, privileges);
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
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            PrivilegesGroup target = new PrivilegesGroup(id, groupName, privileges);
            object obj = new PrivilegesGroup(2, "OtherGroup" ,new List<Privilege>() { 
                                new Privilege(4, "privilege:4")});
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffGroupNameEqualsTest()
        {
            int id = 1;
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            PrivilegesGroup target = new PrivilegesGroup(id, groupName, privileges);
            object obj = new PrivilegesGroup(id, "OtherGroup", privileges);
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
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            PrivilegesGroup target = new PrivilegesGroup(id, groupName, privileges);
            object obj = new PrivilegesGroup(2, groupName, privileges);
            bool expected = false;
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
            int id = 1;
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            PrivilegesGroup target = new PrivilegesGroup(id, groupName, privileges);
            object obj = new PrivilegesGroup(id, groupName, new List<Privilege>() { 
                                new Privilege(4, "privilege:4")});
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
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            PrivilegesGroup target = new PrivilegesGroup(id, groupName, privileges);
            string expected = target.GroupName;

            foreach (Privilege item in target.Privileges)
            {
                expected += "\n" + item.ToString();
            }

            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
