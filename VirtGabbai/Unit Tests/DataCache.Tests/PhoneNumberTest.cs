using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataAccessTest
{
    [TestClass()]
    public class PhoneNumberTest
    {

        private PhoneNumber targetPhoneNumber = null;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            targetPhoneNumber = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
        }
        
        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetPhoneNumber = null;
        }

        #region Equals Test

        /// <summary>
        ///Comparing two phone numbers with no differences
        ///</summary>
        [TestMethod()]
        public void PhoneNumber_Equals_NoDifferences()
        {
            PhoneNumber comparedNumber = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            Assert.IsTrue(targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in every field
        ///</summary>
        [TestMethod()]
        public void PhoneNumber_Equals_DifferenceInEveryField()
        {
            PhoneNumber comparedNumber = new PhoneNumber(2, "0546147485", new PhoneType(2, "house phone"));
            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in the id
        ///</summary>
        [TestMethod()]
        public void PhoneNumber_Equals_DifferenceInId()
        {
            PhoneNumber comparedNumber = new PhoneNumber(2, "0546137475", new PhoneType(1, "cell phone"));
            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in the numnber
        ///</summary>
        [TestMethod()]
        public void PhoneNumber_Equals_DifferenceInNumber()
        {
            PhoneNumber comparedNumber = new PhoneNumber(1, "0546147485", new PhoneType(1, "cell phone"));
            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
        }

        /// <summary>
        ///Comparing two phone numbers with a difference in the type
        ///</summary>
        [TestMethod()]
        public void PhoneNumber_Equals_DifferenceInType()
        {
            PhoneNumber comparedNumber = new PhoneNumber(1, "0546137475", new PhoneType(2, "house phone"));
            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
        }

        [TestMethod]
        public void PhoneNumber_Equals_Null_Returns_False()
        {
            Assert.IsFalse(targetPhoneNumber.Equals(null));
        }

        [TestMethod]
        public void PhoneNumber_Equals_Non_PhoneNumber_Returns_False()
        {
            Assert.IsFalse(targetPhoneNumber.Equals(0));
        }

        [TestMethod]
        public void PhoneNumber_Equals_Same_Ref_Returns_True()
        {
            Assert.IsTrue(targetPhoneNumber.Equals(targetPhoneNumber));
        }

        #endregion

        #region ToStringTest

        /// <summary>
        ///PhoneNumber.ToString() test
        ///</summary>
        [TestMethod()]
        public void PhoneNUmber_ToString()
        {
            string expected = "Number:\"" + targetPhoneNumber.Number + "\" " +
                              targetPhoneNumber.Type.ToString();
            string actual = targetPhoneNumber.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
