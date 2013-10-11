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
        ///A test for GetPhoneNumberByType
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByTypeTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            PhoneType searchedType = null; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = target.GetPhoneNumberByType(searchedType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddMultipleNewPhoneTypes
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewPhoneTypesTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            List<PhoneNumber> newPhoneNumberList = null; // TODO: Initialize to an appropriate value
            target.AddMultipleNewPhoneTypes(newPhoneNumberList);
        }

        /// <summary>
        ///A test for AddNewPhoneNumber
        ///</summary>
        [TestMethod()]
        public void AddNewPhoneNumberTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            PhoneNumber newPhoneNumber = null; // TODO: Initialize to an appropriate value
            target.AddNewPhoneNumber(newPhoneNumber);
        }

        /// <summary>
        ///A test for ConvertMultipleDbPhoneNumbersToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleDbPhoneNumbersToLocalTypeTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            List<t_phone_numbers> dbTypePhoneNumberList = null; // TODO: Initialize to an appropriate value
            List<PhoneNumber> expected = null; // TODO: Initialize to an appropriate value
            List<PhoneNumber> actual;
            actual = target.ConvertMultipleDbPhoneNumbersToLocalType(dbTypePhoneNumberList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleLocalPhoneNumbersToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleLocalPhoneNumbersToDbTypeTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            List<PhoneNumber> localTypePhoneNumberList = null; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> expected = null; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> actual;
            actual = target.ConvertMultipleLocalPhoneNumbersToDbType(localTypePhoneNumberList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbPhoneNumberToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleDbPhoneNumberToLocalTypeTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            t_phone_numbers dbTypePhoneNumber = null; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = target.ConvertSingleDbPhoneNumberToLocalType(dbTypePhoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalPhoneNumberToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleLocalPhoneNumberToDbTypeTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            PhoneNumber localTypePhoneNumber = null; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = target.ConvertSingleLocalPhoneNumberToDbType(localTypePhoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteMultiplePhoneNumbers
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePhoneNumbersTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            List<PhoneNumber> deletedPhoneNumberList = null; // TODO: Initialize to an appropriate value
            target.DeleteMultiplePhoneNumbers(deletedPhoneNumberList);
        }

        /// <summary>
        ///A test for DeleteSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePhoneNumberTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            PhoneNumber deletedPhoneNumber = null; // TODO: Initialize to an appropriate value
            target.DeleteSinglePhoneNumber(deletedPhoneNumber);
        }

        /// <summary>
        ///A test for GetAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        public void GetAllPhoneNumbersTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            List<PhoneNumber> expected = null; // TODO: Initialize to an appropriate value
            List<PhoneNumber> actual;
            actual = target.GetAllPhoneNumbers(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPhoneNumberById
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByIdTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = target.GetPhoneNumberById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetSpecificPhoneNumberTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            string phoneNumber = string.Empty; // TODO: Initialize to an appropriate value
            PhoneNumber expected = null; // TODO: Initialize to an appropriate value
            PhoneNumber actual;
            actual = target.GetSpecificPhoneNumber(phoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPhoneNumbersTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> expected = null; // TODO: Initialize to an appropriate value
            List<t_phone_numbers> actual;
            actual = target.LookupAllPhoneNumbers(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPhoneNumberById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByIdTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = target.LookupPhoneNumberById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPhoneNumberByType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByTypeTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            PhoneType searchedType = null; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = target.LookupPhoneNumberByType(searchedType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificPhoneNumberTest()
        {
            PhoneNumberAccess_Accessor target = new PhoneNumberAccess_Accessor(); // TODO: Initialize to an appropriate value
            string phoneNumber = string.Empty; // TODO: Initialize to an appropriate value
            t_phone_numbers expected = null; // TODO: Initialize to an appropriate value
            t_phone_numbers actual;
            actual = target.LookupSpecificPhoneNumber(phoneNumber);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateMultiplePhoneNumbers
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePhoneNumbersTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            List<PhoneNumber> updatedPhoneNumberList = null; // TODO: Initialize to an appropriate value
            target.UpdateMultiplePhoneNumbers(updatedPhoneNumberList);
        }

        /// <summary>
        ///A test for UpdateSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePhoneNumberTest()
        {
            PhoneNumberAccess target = new PhoneNumberAccess(); // TODO: Initialize to an appropriate value
            PhoneNumber updatedPhoneNumber = null; // TODO: Initialize to an appropriate value
            target.UpdateSinglePhoneNumber(updatedPhoneNumber);
        }
    }
}
