using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass]
    public class PrivilegesGroupTest
    {
        private PrivilegesGroup _target;

        [TestInitialize]
        public void Setup()
        {
            const int id = 1;
            var privileges = new List<Privilege>
            {
                new Privilege(1,"privilege:1"),
                new Privilege(2, "privilege:2")
            };
            const string groupName = "firstGroup";
            _target = new PrivilegesGroup { Id = id, GroupName = groupName, Privileges = privileges };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _target = null;
        }

        #region Equals Test

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void AllSameEqualsTest()
        {
            const int id = 1;
            var privileges = new List<Privilege>
            {
                new Privilege(1,"privilege:1"),
                new Privilege(2, "privilege:2")
            };
            const string groupName = "firstGroup";
            var obj = new PrivilegesGroup { Id = id, GroupName = groupName, Privileges = privileges };

            Assert.IsTrue(_target.Equals(obj));
            Assert.IsTrue(obj.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void AllDiffEqualsTest()
        {
            var obj = new PrivilegesGroup
            {
                Id = 2,
                GroupName = "OtherGroup",
                Privileges = new List<Privilege>
                {
                    new Privilege(4, "privilege:4")
                }
            };

            Assert.IsFalse(_target.Equals(obj));
            Assert.IsFalse(obj.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffGroupNameEqualsTest()
        {
            const int id = 1;
            var privileges = new List<Privilege>
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            var obj = new PrivilegesGroup { Id = id, GroupName = "Other Group", Privileges = privileges };

            Assert.IsFalse(_target.Equals(obj));
            Assert.IsFalse(obj.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffIdEqualsTest()
        {
            var privileges = new List<Privilege>
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            const string groupName = "firstGroup";
            var obj = new PrivilegesGroup { Id = 2, GroupName = groupName, Privileges = privileges };

            Assert.IsFalse(_target.Equals(obj));
            Assert.IsFalse(obj.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffPrivilegesEqualsTest()
        {
            const int id = 1;
            const string groupName = "firstGroup";

            var obj = new PrivilegesGroup
            {
                Id = id,
                GroupName = groupName,
                Privileges = new List<Privilege>
                {
                    new Privilege(4, "privilege:4")
                }
            };

            Assert.IsFalse(_target.Equals(obj));
            Assert.IsFalse(obj.Equals(_target));
        }

        [TestMethod]
        public void PrivilegesGroup_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_target.Equals(null));
        }

        [TestMethod]
        public void PrivilegesGroup_Equals_Non_PrivilegesGroup_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_target.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void PrivilegesGroup_Equals_Same_Ref_Returns_True()
        {
            var other = _target;

            Assert.IsTrue(_target.Equals(other));
            Assert.IsTrue(other.Equals(_target));
        }

        #endregion

        #region ToString Tests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            const int id = 1;
            var privileges = new List<Privilege>
            {
                new Privilege(1, "privilege:1"),
                new Privilege(2, "privilege:2")
            };
            const string groupName = "firstGroup";
            var target = new PrivilegesGroup { Id = id, GroupName = groupName, Privileges = privileges };
            var expected = target.GroupName;

            foreach (var item in target.Privileges)
            {
                expected += "\n" + item;
            }

            Assert.AreEqual(expected, target.ToString());
        }

        #endregion
    }
}
