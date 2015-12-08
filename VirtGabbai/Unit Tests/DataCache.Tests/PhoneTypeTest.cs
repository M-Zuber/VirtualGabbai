using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocalTypesTest
{
    [TestClass()]
    public class PhoneTypeTest
    {
        private PhoneType targetPhoneType = null;
        #region Test Data Members

        //Target Data Members

        #endregion

        [TestInitialize()]
        public void MyTestInitialize()
        {
            targetPhoneType = new PhoneType(1, "one");
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetPhoneType = null;
        }

        #region Equals

        /// <summary>
        ///Comparing two phone types with no differences
        ///</summary>
        [TestMethod()]
        public void PhoneType_Equals_NoDifferences()
        {
            PhoneType otherPhoneType =
                new PhoneType(targetPhoneType.ID, targetPhoneType.Name);
            Assert.IsTrue(targetPhoneType.Equals(otherPhoneType));
        }

        /// <summary>
        ///Comparing two phone types with a difference in the id
        ///</summary>
        [TestMethod()]
        public void PhoneType_Equals_DifferenceInId()
        {
            PhoneType otherPhoneType =
                new PhoneType((targetPhoneType.ID * 2), targetPhoneType.Name);
            Assert.IsFalse(targetPhoneType.Equals(otherPhoneType));
        }

        /// <summary>
        ///Comparing two phone types with a difference in the type
        ///</summary>
        [TestMethod()]
        public void PhoneType_Equals_DifferenceInType()
        {
            PhoneType otherPhoneType =
                new PhoneType(targetPhoneType.ID,
                    targetPhoneType.Name + targetPhoneType.Name);
            Assert.IsFalse(targetPhoneType.Equals(otherPhoneType));
        }

        /// <summary>
        ///Comparing two phone types with a difference in every field
        ///</summary>
        [TestMethod()]
        public void PhoneType_Equals_DifferenceInEveryField()
        {
            PhoneType otherPhoneType =
                new PhoneType((targetPhoneType.ID * 2),
                    targetPhoneType.Name + targetPhoneType.Name);
            Assert.IsFalse(targetPhoneType.Equals(otherPhoneType));
        }

        [TestMethod]
        public void PhoneType_Equals_Null_Returns_False()
        {
            Assert.IsFalse(targetPhoneType.Equals(null));
        }

        [TestMethod]
        public void PhoneType_Equals_Non_PhoneType_Returns_False()
        {
            Assert.IsFalse(targetPhoneType.Equals(0));
        }

        [TestMethod]
        public void PhoneType_Equals_Same_Ref_Returns_True()
        {
            Assert.IsTrue(targetPhoneType.Equals(targetPhoneType));
        }

        #endregion

        #region ToString

        /// <summary>
        ///PhoneType,ToString() Test
        ///</summary>
        [TestMethod()]
        public void PhoneType_ToString()
        {
            string expected = "Type:\"one\"";
            string actual = targetPhoneType.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
