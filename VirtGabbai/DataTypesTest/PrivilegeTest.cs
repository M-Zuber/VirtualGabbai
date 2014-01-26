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
        #region Test Data Members

        //Target Data Members
        int id = 1;
        string privilegeName = "admin";
        Privilege targetPrivilege = null;

        #endregion

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            targetPrivilege = new Privilege(id, privilegeName); ;
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetPrivilege = null;
        }
        //
        #endregion

        #region Equals Tests

        /// <summary>
        ///Comparing two privleges with no differences
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_NoDifferences()
        {
            Privilege otherPrivilege = new Privilege(id, privilegeName);
            Assert.IsTrue(targetPrivilege.Equals(otherPrivilege));
        }
        
        /// <summary>
        ///Comparing two privileges with a difference in every field
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_DifferenceInEveryField()
        {
            Privilege otherPrivilege = new Privilege((id * 2), privilegeName + privilegeName);
            Assert.IsFalse(targetPrivilege.Equals(otherPrivilege));
        }

        /// <summary>
        ///Comparing two privileges with a difference in the id
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_DifferenceInId()
        {
            Privilege otherPrivilege = new Privilege((id * 2), privilegeName);
            Assert.IsFalse(targetPrivilege.Equals(otherPrivilege));
        }

        /// <summary>
        ///Comparing two privleges with a difference in the privilege name
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_DifferenceInPrivilegeName()
        {
            Privilege otherPrivilege = new Privilege(id, privilegeName + privilegeName);
            Assert.IsFalse(targetPrivilege.Equals(otherPrivilege));
        }
        
        #endregion

        #region ToString Tests

        /// <summary>
        ///Privilege.ToString() Test
        ///</summary>
        [TestMethod()]
        public void Privilege_ToString()
        {
            string expected = "admin";
            string actual = targetPrivilege.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
