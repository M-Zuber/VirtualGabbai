using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass]
    public class UserTest
    {
        private readonly int _id = 1;
        private readonly string _userName = "mez613";
        private readonly string _password = "*******";
        private readonly string _email = "jack@jingle.high";
        private PrivilegesGroup _privilegeGroup;

        private User _target;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _privilegeGroup = new PrivilegesGroup(1, "admin",
                new List<Privilege>
                {
                    new Privilege(1,"privilege:1"),
                    new Privilege(2,"privilege:2")
                });

            _target = new User(_id, _userName, _password, _email, _privilegeGroup);
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _privilegeGroup = null;
            _target = null;
        }

        #region Setup

        #endregion

        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void AllSameEqualsTest()
        {
            var other = new User(_id, _userName, _password, _email, _privilegeGroup);

            Assert.IsTrue(_target.Equals(other));
            Assert.IsTrue(other.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void AllDiffEqualsTest()
        {
            const int diffId = 0;
            const string diiUserName = "different";
            const string diffPassword = "different";
            const string diffEmail = "blah@maz.blah";
            var diffUserGroup = new PrivilegesGroup(325, "DSATGE", new List<Privilege>
            {
                    new Privilege(456, "3254235")
                });
            var other = new User(diffId, diiUserName, diffPassword, diffEmail, diffUserGroup);

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffIdEqualsTest()
        {
            const int id = 0;
            var other = new User(id, _userName, _password, _email, _privilegeGroup);

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffUserNameEqualsTest()
        {
            const string diffUserName = "blah";
            var other = new User(_id, diffUserName, _password, _email, _privilegeGroup);

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffPasswordEqualsTest()
        {
            const string diffPassword = "123432";
            var other = new User(_id, _userName, diffPassword, _email, _privilegeGroup);

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffEmailEqualsTest()
        {
            const string diffEmail = "blah@blah.iter";
            var other = new User(_id, _userName, _password, diffEmail, _privilegeGroup);

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod]
        public void DiffUserGroupEqualsTest()
        {
            var diffUserGroup = new PrivilegesGroup(325, "DSATGE", new List<Privilege>
            {
                    new Privilege(456, "3254235")
                });
            var other = new User(_id, _userName, _password, _email, diffUserGroup);

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        [TestMethod]
        public void User_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_target.Equals(null));
        }

        [TestMethod]
        public void User_Equals_Non_User_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_target.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void User_Equals_Same_Ref_Returns_True()
        {
            var other = _target;

            Assert.IsTrue(other.Equals(_target));
            Assert.IsTrue(_target.Equals(other));
        }

        #endregion

        #region ToString Tests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod]
        public void ToStringTest()
        {
            var expected = "UserName: mez613\nEmail: jack@jingle.high\n";
            expected += _privilegeGroup.ToString();

            Assert.AreEqual(expected, _target.ToString());
        }

        #endregion
    }
}
