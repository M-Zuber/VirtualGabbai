using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass()]
    public class PrivilegesGroupTest
    {
        private PrivilegesGroup target;

        [TestInitialize()]
        public void Setup()
        {
            int id = 1;
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1,"privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            target = new PrivilegesGroup(id, groupName, privileges);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            target = null;
        }

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
                new Privilege(1,"privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            var obj = new PrivilegesGroup(id, groupName, privileges);

            Assert.IsTrue(target.Equals(obj));
            Assert.IsTrue(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            var obj = new PrivilegesGroup(2, "OtherGroup", new List<Privilege>() {
                                new Privilege(4, "privilege:4")});

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
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
            var obj = new PrivilegesGroup(id, "OtherGroup", privileges);

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            List<Privilege> privileges = new List<Privilege>()
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            string groupName = "firstGroup";
            var obj = new PrivilegesGroup(2, groupName, privileges);

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }


        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPrivilegesEqualsTest()
        {
            int id = 1;
            string groupName = "firstGroup";
            var obj = new PrivilegesGroup(id, groupName, new List<Privilege>() {
                                new Privilege(4, "privilege:4")});

            Assert.IsFalse(target.Equals(obj));
            Assert.IsFalse(obj.Equals(target));
        }

        [TestMethod]
        public void PrivilegesGroup_Equals_Null_Returns_False()
        {
            Assert.IsFalse(target.Equals(null));
        }

        [TestMethod]
        public void PrivilegesGroup_Equals_Non_PrivilegesGroup_Returns_False()
        {
            Assert.IsFalse(target.Equals(0));
        }

        [TestMethod]
        public void PrivilegesGroup_Equals_Same_Ref_Returns_True()
        {
            var other = target;

            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
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

            Assert.AreEqual(expected, target.ToString());
        }

        #endregion
    }
}
