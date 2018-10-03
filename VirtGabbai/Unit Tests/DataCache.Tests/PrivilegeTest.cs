using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCache.Tests
{
    /// <summary>
    ///This is a test class for PrivilegeTest and is intended
    ///to contain all PrivilegeTest Unit Tests
    ///</summary>
    [TestClass]
    public class PrivilegeTest
    {
        private const int Id = 1;
        private const string PrivilegeName = "admin";
        private Privilege _target;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _target = new Privilege(Id, PrivilegeName);
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _target = null;
        }

        #region Equals Tests

        /// <summary>
        ///Comparing two privileges with no differences
        ///</summary>
        [TestMethod]
        public void Privilege_Equals_NoDifferences()
        {
            var other = new Privilege(Id, PrivilegeName);
            Assert.IsTrue(_target.Equals(other));
            Assert.IsTrue(other.Equals(_target));
        }

        /// <summary>
        ///Comparing two privileges with a difference in every field
        ///</summary>
        [TestMethod]
        public void Privilege_Equals_DifferenceInEveryField()
        {
            var other = new Privilege(Id * 2, PrivilegeName + PrivilegeName);
            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///Comparing two privileges with a difference in the id
        ///</summary>
        [TestMethod]
        public void Privilege_Equals_DifferenceInId()
        {
            var other = new Privilege(Id * 2, PrivilegeName);
            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        /// <summary>
        ///Comparing two privileges with a difference in the privilege name
        ///</summary>
        [TestMethod]
        public void Privilege_Equals_DifferenceInPrivilegeName()
        {
            var other = new Privilege(Id, PrivilegeName + PrivilegeName);

            Assert.IsFalse(_target.Equals(other));
            Assert.IsFalse(other.Equals(_target));
        }

        [TestMethod]
        public void Privilege_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_target.Equals(null));
        }

        [TestMethod]
        public void Privilege_Equals_Non_Privilege_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_target.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void Privilege_Equals_Same_Ref_Returns_True()
        {
            var other = _target;

            Assert.IsTrue(_target.Equals(other));
            Assert.IsTrue(other.Equals(_target));
        }

        #endregion

        #region ToString Tests

        /// <summary>
        ///Privilege.ToString() Test
        ///</summary>
        [TestMethod]
        public void Privilege_ToString()
        {
            const string expected = "admin";
            Assert.AreEqual(expected, _target.ToString());
        }

        #endregion
    }
}
