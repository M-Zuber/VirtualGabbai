using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCache.Tests
{
    [TestClass]
    public class PhoneNumberTest
    {
        private PhoneNumber _targetPhoneNumber;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _targetPhoneNumber = new PhoneNumber { Id = 1, Number = "0546137475", Type = new PhoneType { Id = 1, Name = "cell phone" } };
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _targetPhoneNumber = null;
        }

        #region Equals Test

        /// <summary>
        ///Comparing two phone numbers with no differences
        ///</summary>
        [TestMethod]
        public void PhoneNumber_Equals_NoDifferences()
        {
            var comparedNumber = new PhoneNumber { Id = 1, Number = "0546137475", Type = new PhoneType { Id = 1, Name = "cell phone" } };
            Assert.IsTrue(_targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in every field
        ///</summary>
        [TestMethod]
        public void PhoneNumber_Equals_DifferenceInEveryField()
        {
            var comparedNumber = new PhoneNumber { Id = 2, Number = "0546147485", Type = new PhoneType { Id = 2, Name = "house phone" } };
            Assert.IsFalse(_targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in the id
        ///</summary>
        [TestMethod]
        public void PhoneNumber_Equals_DifferenceInId()
        {
            var comparedNumber = new PhoneNumber { Id = 2, Number = "0546137475", Type = new PhoneType { Id = 1, Name = "cell phone" } };
            Assert.IsFalse(_targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in the numnber
        ///</summary>
        [TestMethod]
        public void PhoneNumber_Equals_DifferenceInNumber()
        {
            var comparedNumber = new PhoneNumber { Id = 1, Number = "0546147485", Type = new PhoneType { Id = 1, Name = "cell phone" } };
            Assert.IsFalse(_targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in the type
        ///</summary>
        [TestMethod]
        public void PhoneNumber_Equals_DifferenceInType()
        {
            var comparedNumber = new PhoneNumber { Id = 1, Number = "0546137475", Type = new PhoneType { Id = 2, Name = "house phone" } };
            Assert.IsFalse(_targetPhoneNumber.Equals(comparedNumber));
        }

        [TestMethod]
        public void PhoneNumber_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_targetPhoneNumber.Equals(null));
        }

        [TestMethod]
        public void PhoneNumber_Equals_Non_PhoneNumber_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_targetPhoneNumber.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void PhoneNumber_Equals_Same_Ref_Returns_True()
        {
            var other = _targetPhoneNumber;

            Assert.IsTrue(_targetPhoneNumber.Equals(other));
            Assert.IsTrue(other.Equals(_targetPhoneNumber));
        }

        #endregion

        #region ToStringTest

        /// <summary>
        ///PhoneNumber.ToString() test
        ///</summary>
        [TestMethod]
        public void PhoneNUmber_ToString()
        {
            var expected = "Number:\"" + _targetPhoneNumber.Number + "\" " +
                              _targetPhoneNumber.Type;
            var actual = _targetPhoneNumber.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
