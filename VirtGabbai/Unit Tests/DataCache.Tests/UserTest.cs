using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass()]
    public class UserTest
    {
        private int id = 1;
        private string userName = "mez613";
        private string password = "*******";
        private string email = "jack@jingle.high";
        private PrivilegesGroup privilegeGroup;

        private User target;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            privilegeGroup = new PrivilegesGroup(1, "admin",
                new List<Privilege>()
                {
                    new Privilege(1,"privilege:1"),
                    new Privilege(2,"privilege:2")
                });

            target = new User(id, userName, password, email, privilegeGroup);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            privilegeGroup = null;
            target = null;
        }

        #region Setup


        #endregion

        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            var other = new User(id, userName, password, email, privilegeGroup);

            Assert.IsTrue(target.Equals(other));
            Assert.IsTrue(other.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            int diff_id = 0;
            string diiUserName = "different";
            string diffPassword = "different";
            string diffEmail = "blah@maz.blah";
            PrivilegesGroup diffUserGroup = new PrivilegesGroup(325, "DSATGE", new List<Privilege>()
                {
                    new Privilege(456, "3254235")
                });
            var  other = new User(diff_id, diiUserName, diffPassword, diffEmail, diffUserGroup); ;

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            int _id = 0;
            var  other = new User(_id, userName, password, email, privilegeGroup);

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffUserNameEqualsTest()
        {
            string diffUserName = "blah";
            var  other = new User(id, diffUserName, password, email, privilegeGroup);

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPasswordEqualsTest()
        {
            string diffPassword = "123432";
            var  other = new User(id, userName, diffPassword, email, privilegeGroup);

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffEmailEqualsTest()
        {
            string diffEmail = "blah@blah.iter";
            var  other = new User(id, userName, password, diffEmail, privilegeGroup);

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffUserGroupEqualsTest()
        {
            PrivilegesGroup diffUserGroup = new PrivilegesGroup(325, "DSATGE", new List<Privilege>()
                {
                    new Privilege(456, "3254235")
                });
            var  other = new User(id, userName, password, email, diffUserGroup);

            Assert.IsFalse(target.Equals(other));
            Assert.IsFalse(other.Equals(target));
        }

        [TestMethod]
        public void User_Equals_Null_Returns_False()
        {
            Assert.IsFalse(target.Equals(null));
        }

        [TestMethod]
        public void User_Equals_Non_User_Returns_False()
        {
            Assert.IsFalse(target.Equals(0));
        }

        [TestMethod]
        public void User_Equals_Same_Ref_Returns_True()
        {
            var other = target;

            Assert.IsTrue(other.Equals(target));
            Assert.IsTrue(target.Equals(other));
        }

        #endregion

        #region ToString Tests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string expected = "UserName: mez613\nEmail: jack@jingle.high\n";
            expected += privilegeGroup.ToString();

            Assert.AreEqual(expected, target.ToString());
        }

        #endregion
    }
}
