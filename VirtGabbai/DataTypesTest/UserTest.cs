using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LocalTypesTest
{
    
    
    /// <summary>
    ///This is a test class for UserTest and is intended
    ///to contain all UserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserTest
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

        #region Setup

        private static int id = 1;
        private static string userName = "mez613";
        private static string password = "*******";
        private static string email = "jack@jingle.high";
        private static PrivilegesGroup userGroup = new PrivilegesGroup(1, "admin",
            new List<Privilege>()
            {
                new Privilege(1,"privilege:1"),
                new Privilege(2,"privilege:2")
            }
        );

        private User target = new User(id, userName, password, email, userGroup);

        #endregion

        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            int _id = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            PrivilegesGroup userGroup = null;
            User target = new User(_id, userName, password, email, userGroup);
            object obj = null;
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
            int _id = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            PrivilegesGroup userGroup = null;
            User target = new User(_id, userName, password, email, userGroup);
            object obj = null;
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
            int _id = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            PrivilegesGroup userGroup = null;
            User target = new User(_id, userName, password, email, userGroup);
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffUserNameEqualsTest()
        {
            int _id = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            PrivilegesGroup userGroup = null;
            User target = new User(_id, userName, password, email, userGroup);
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPasswordEqualsTest()
        {
            int _id = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            PrivilegesGroup userGroup = null;
            User target = new User(_id, userName, password, email, userGroup);
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffEmailEqualsTest()
        {
            int _id = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            PrivilegesGroup userGroup = null;
            User target = new User(_id, userName, password, email, userGroup);
            object obj = null;
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffUserGroupEqualsTest()
        {
            int _id = 0;
            string userName = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            PrivilegesGroup userGroup = null;
            User target = new User(_id, userName, password, email, userGroup);
            object obj = null;
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
            int _id = 0; // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            string email = string.Empty; // TODO: Initialize to an appropriate value
            PrivilegesGroup userGroup = null; // TODO: Initialize to an appropriate value
            User target = new User(_id, userName, password, email, userGroup); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
