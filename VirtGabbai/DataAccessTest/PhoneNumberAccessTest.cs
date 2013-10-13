using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using DataCache;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for PhoneNumberAccessTest and is intended
    ///to contain all PhoneNumberAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhoneNumberAccessTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddMultipleNewPhoneTypes
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewPhoneTypesTest()
        {
            List<PhoneNumber> newPhoneNumberList = null; // TODO: Initialize to an appropriate value
            PhoneNumberAccess.AddMultipleNewPhoneTypes(newPhoneNumberList);
        }

        /// <summary>
        ///A test for AddNewPhoneNumber
        ///</summary>
        [TestMethod()]
        public void AddNewPhoneNumberTest()
        {
            PhoneNumber newPhoneNumber = null; // TODO: Initialize to an appropriate value
            PhoneNumberAccess.AddNewPhoneNumber(newPhoneNumber);
        }

        /// <summary>
        ///A test for ConvertMultipleDbPhoneNumbersToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleDbPhoneNumbersToLocalTypeTest()
        {
            List<t_phone_numbers> dbTypePhoneNumberList = null; // TODO: Initialize to an appropriate value
            List<PhoneNumber> expected = null; // TODO: Initialize to an appropriate value
            List<PhoneNumber> actual;
            actual = PhoneNumberAccess_Accessor.ConvertMultipleDbPhoneNumbersToLocalType(dbTypePhoneNumberList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleLocalPhoneNumbersToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleLocalPhoneNumbersToDbTypeTest()
        {
            List<PhoneNumber> localTypePhoneNumberList = null; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> expected = null; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> actual;
            actual = PhoneNumberAccess_Accessor.ConvertMultipleLocalPhoneNumbersToDbType(localTypePhoneNumberList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbPhoneNumberToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleDbPhoneNumberToLocalTypeTest()
        {
            t_phone_numbers dbTypePhoneNumber = null; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = PhoneNumberAccess_Accessor.ConvertSingleDbPhoneNumberToLocalType(dbTypePhoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalPhoneNumberToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleLocalPhoneNumberToDbTypeTest()
        {
            PhoneNumber localTypePhoneNumber = null; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.ConvertSingleLocalPhoneNumberToDbType(localTypePhoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteMultiplePhoneNumbers
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePhoneNumbersTest()
        {
            List<PhoneNumber> deletedPhoneNumberList = null; // TODO: Initialize to an appropriate value
            PhoneNumberAccess.DeleteMultiplePhoneNumbers(deletedPhoneNumberList);
        }

        /// <summary>
        ///A test for DeleteSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePhoneNumberTest()
        {
            PhoneNumber deletedPhoneNumber = null; // TODO: Initialize to an appropriate value
            PhoneNumberAccess.DeleteSinglePhoneNumber(deletedPhoneNumber);
        }

        /// <summary>
        ///A test for GetAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        public void GetAllPhoneNumbersTest()
        {
            int personId = 0; // TODO: Initialize to an appropriate value
            List<PhoneNumber> expected = null; // TODO: Initialize to an appropriate value
            List<PhoneNumber> actual;
            actual = PhoneNumberAccess.GetAllPhoneNumbers(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPhoneNumberById
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = PhoneNumberAccess.GetPhoneNumberById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPhoneNumberByType
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByTypeTest()
        {
            PhoneType searchedType = null; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = PhoneNumberAccess.GetPhoneNumberByType(searchedType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetSpecificPhoneNumberTest()
        {
            string phoneNumber = string.Empty; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = PhoneNumberAccess.GetSpecificPhoneNumber(phoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPhoneNumbersTest()
        {
            int personId = 0; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> expected = null; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> actual;
            actual = PhoneNumberAccess_Accessor.LookupAllPhoneNumbers(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPhoneNumberById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.LookupPhoneNumberById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPhoneNumberByType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByTypeTest()
        {
            PhoneType searchedType = null; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.LookupPhoneNumberByType(searchedType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificPhoneNumberTest()
        {
            string phoneNumber = string.Empty; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.LookupSpecificPhoneNumber(phoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateMultiplePhoneNumbers
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePhoneNumbersTest()
        {
            List<PhoneNumber> updatedPhoneNumberList = null; // TODO: Initialize to an appropriate value
            PhoneNumberAccess.UpdateMultiplePhoneNumbers(updatedPhoneNumberList);
        }

        /// <summary>
        ///A test for UpdateSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePhoneNumberTest()
        {
            PhoneNumber updatedPhoneNumber = null; // TODO: Initialize to an appropriate value
            PhoneNumberAccess.UpdateSinglePhoneNumber(updatedPhoneNumber);
        }
    }
}
