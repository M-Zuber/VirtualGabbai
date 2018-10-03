using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCache.Tests
{
    [TestClass]
    public class PhoneTypeTest
    {
        private PhoneType _targetPhoneType;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _targetPhoneType = new PhoneType { Id = 1, Name = "one" };
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _targetPhoneType = null;
        }

        #region Equals

        /// <summary>
        ///Comparing two phone types with no differences
        ///</summary>
        [TestMethod]
        public void PhoneType_Equals_NoDifferences()
        {
            var otherPhoneType =
                new PhoneType { Id = _targetPhoneType.Id, Name = _targetPhoneType.Name };
            Assert.IsTrue(_targetPhoneType.Equals(otherPhoneType));
        }

        /// <summary>
        ///Comparing two phone types with a difference in the id
        ///</summary>
        [TestMethod]
        public void PhoneType_Equals_DifferenceInId()
        {
            var otherPhoneType =
                new PhoneType { Id = _targetPhoneType.Id * 2, Name = _targetPhoneType.Name };
            Assert.IsFalse(_targetPhoneType.Equals(otherPhoneType));
        }

        /// <summary>
        ///Comparing two phone types with a difference in the type
        ///</summary>
        [TestMethod]
        public void PhoneType_Equals_DifferenceInType()
        {
            var otherPhoneType =
                new PhoneType
                {
                    Id = _targetPhoneType.Id,
                    Name =
                    _targetPhoneType.Name + _targetPhoneType.Name
                };
            Assert.IsFalse(_targetPhoneType.Equals(otherPhoneType));
        }

        /// <summary>
        ///Comparing two phone types with a difference in every field
        ///</summary>
        [TestMethod]
        public void PhoneType_Equals_DifferenceInEveryField()
        {
            var otherPhoneType =
                new PhoneType
                {
                    Id = _targetPhoneType.Id * 2,
                    Name =
                    _targetPhoneType.Name + _targetPhoneType.Name
                };
            Assert.IsFalse(_targetPhoneType.Equals(otherPhoneType));
        }

        [TestMethod]
        public void PhoneType_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_targetPhoneType.Equals(null));
        }

        [TestMethod]
        public void PhoneType_Equals_Non_PhoneType_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_targetPhoneType.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void PhoneType_Equals_Same_Ref_Returns_True()
        {
            var other = _targetPhoneType;

            Assert.IsTrue(other.Equals(_targetPhoneType));
            Assert.IsTrue(_targetPhoneType.Equals(other));
        }

        #endregion

        #region ToString

        /// <summary>
        ///PhoneType,ToString() Test
        ///</summary>
        [TestMethod]
        public void PhoneType_ToString()
        {
            const string expected = "Type:\"one\"";
            var actual = _targetPhoneType.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
