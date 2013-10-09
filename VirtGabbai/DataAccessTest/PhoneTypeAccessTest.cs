using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using DataCache;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for PhoneTypeAccessTest and is intended
    ///to contain all PhoneTypeAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PhoneTypeAccessTest
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

        #region Add Tests

        /// <summary>
        ///A test for AddMultipleNewPhoneTypes
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewPhoneTypesTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            List<PhoneType> newPhoneTypeList = null; // l: Initialize to an appropriate value
            target.AddMultipleNewPhoneTypes(newPhoneTypeList);
        }

        /// <summary>
        ///A test for AddNewPhoneType
        ///</summary>
        [TestMethod()]
        public void AddNewPhoneTypeTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            PhoneType newPhoneType = null; // l: Initialize to an appropriate value
            target.AddNewPhoneType(newPhoneType);
        }
        
        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConverSinglePhoneTypeToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConverSinglePhoneTypeToLocalTypeTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor(); // l: Initialize to an appropriate value
            t_phone_types dbTypePhoneType = null; // l: Initialize to an appropriate value
            PhoneType expected = null; // l: Initialize to an appropriate value
            PhoneType actual;
            actual = target.ConverSinglePhoneTypeToLocalType(dbTypePhoneType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultiplePhoneTypesToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultiplePhoneTypesToDbTypeTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor(); // l: Initialize to an appropriate value
            List<PhoneType> localTypePhoneTypeList = null; // l: Initialize to an appropriate value
            List<t_phone_types> expected = null; // l: Initialize to an appropriate value
            List<t_phone_types> actual;
            actual = target.ConvertMultiplePhoneTypesToDbType(localTypePhoneTypeList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultiplePhoneTypesToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultiplePhoneTypesToLocalTypeTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor(); // l: Initialize to an appropriate value
            List<t_phone_types> dbTypePhoneTypeList = null; // l: Initialize to an appropriate value
            List<PhoneType> expected = null; // l: Initialize to an appropriate value
            List<PhoneType> actual;
            actual = target.ConvertMultiplePhoneTypesToLocalType(dbTypePhoneTypeList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSinglePhoneTypeToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSinglePhoneTypeToDbTypeTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor(); // l: Initialize to an appropriate value
            PhoneType localTypePhoneType = null; // l: Initialize to an appropriate value
            t_phone_types expected = null; // l: Initialize to an appropriate value
            t_phone_types actual;
            actual = target.ConvertSinglePhoneTypeToDbType(localTypePhoneType);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultiplePhoneTypes
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePhoneTypesTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            List<PhoneType> deletedPhoneTypeList = null; // l: Initialize to an appropriate value
            target.DeleteMultiplePhoneTypes(deletedPhoneTypeList);
        }

        /// <summary>
        ///A test for DeleteSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePhoneTypeTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            PhoneType deletedPhoneType = null; // l: Initialize to an appropriate value
            target.DeleteSinglePhoneType(deletedPhoneType);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllPhoneTypes
        ///</summary>
        [TestMethod()]
        public void GetAllPhoneTypesTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            List<PhoneType> expected = null; // l: Initialize to an appropriate value
            List<PhoneType> actual;
            actual = target.GetAllPhoneTypes();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPhoneTypeById
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByIdTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            int id = 0; // l: Initialize to an appropriate value
            PhoneType expected = null; // l: Initialize to an appropriate value
            PhoneType actual;
            actual = target.GetPhoneTypeById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByTypeNameTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            string typeName = string.Empty; // l: Initialize to an appropriate value
            PhoneType expected = null; // l: Initialize to an appropriate value
            PhoneType actual;
            actual = target.GetPhoneTypeByTypeName(typeName);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllPhoneTypes
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPhoneTypesTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor(); // l: Initialize to an appropriate value
            List<t_phone_types> expected = null; // l: Initialize to an appropriate value
            List<t_phone_types> actual;
            actual = target.LookupAllPhoneTypes();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPhoneTypById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneTypByIdTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor(); // l: Initialize to an appropriate value
            int ID = 0; // l: Initialize to an appropriate value
            t_phone_types expected = null; // l: Initialize to an appropriate value
            t_phone_types actual;
            actual = target.LookupPhoneTypById(ID);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneTypeByTypeNameTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor(); // l: Initialize to an appropriate value
            string typeName = string.Empty; // l: Initialize to an appropriate value
            t_phone_types expected = null; // l: Initialize to an appropriate value
            t_phone_types actual;
            actual = target.LookupPhoneTypeByTypeName(typeName);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Update Tests

        /// <summary>
        ///A test for UpdateMultiplePhoneTypes
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePhoneTypesTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            List<PhoneType> updatedPhoneTypeList = null; // l: Initialize to an appropriate value
            target.UpdateMultiplePhoneTypes(updatedPhoneTypeList);
        }

        /// <summary>
        ///A test for UpdateSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePhoneTypeTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess(); // l: Initialize to an appropriate value
            PhoneType updatedPhoneType = null; // l: Initialize to an appropriate value
            target.UpdateSinglePhoneType(updatedPhoneType);
        }

        #endregion
    }
}
