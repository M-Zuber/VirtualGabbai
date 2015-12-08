using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataCache.Tests
{


    /// <summary>
    ///This is a test class for PrivilegeTest and is intended
    ///to contain all PrivilegeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PrivilegeTest
    {
        int id = 1;
        string privilegeName = "admin";
        Privilege target = null;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            target = new Privilege(id, privilegeName); ;
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            target = null;
        }

        #region Equals Tests

        /// <summary>
        ///Comparing two privleges with no differences
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_NoDifferences()
        {
            Privilege other = new Privilege(id, privilegeName);
            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
        }

        /// <summary>
        ///Comparing two privileges with a difference in every field
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_DifferenceInEveryField()
        {
            Privilege other = new Privilege((id * 2), privilegeName + privilegeName);
            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///Comparing two privileges with a difference in the id
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_DifferenceInId()
        {
            Privilege other = new Privilege((id * 2), privilegeName);
            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///Comparing two privleges with a difference in the privilege name
        ///</summary>
        [TestMethod()]
        public void Privilege_Equals_DifferenceInPrivilegeName()
        {
            Privilege other = new Privilege(id, privilegeName + privilegeName);

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        [TestMethod]
        public void Privilege_Equals_Null_Returns_False()
        {
            Assert.IsFalse(target.Equals(null));
        }

        [TestMethod]
        public void Privilege_Equals_Non_Privilege_Returns_False()
        {
            Assert.IsFalse(target.Equals(0));
        }

        [TestMethod]
        public void Privilege_Equals_Same_Ref_Returns_True()
        {
            var other = target;

            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
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
            Assert.AreEqual(expected, target.ToString());
        }

        #endregion
    }
}
