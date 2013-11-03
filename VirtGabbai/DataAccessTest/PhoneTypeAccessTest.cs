using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using DataTypes;
using System.Collections.Generic;
using DataCache;
using Framework;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            if (!Cache.CacheData.t_phone_types.Any(numberType => numberType.C_id == 1))
            {
                Cache.CacheData.t_phone_types.AddObject(t_phone_types.Createt_phone_types(1, "phonetype:1"));
            }
            for (int newPhoneTypeIndex = 2; newPhoneTypeIndex <= 10; newPhoneTypeIndex++)
            {
                Cache.CacheData.t_phone_types.AddObject(
                    t_phone_types.Createt_phone_types(newPhoneTypeIndex,
                                                "phonetype:" + newPhoneTypeIndex.ToString()));
            }
            Cache.CacheData.SaveChanges();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var test = (from ptype in Cache.CacheData.t_phone_types select ptype).ToList<t_phone_types>();

            for (int i = 0; i < test.Count; i++)
            {
                Cache.CacheData.t_phone_types.DeleteObject(test[i]);
            }
            Cache.CacheData.SaveChanges();
        }
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
            List<PhoneType> newPhoneTypeList = new List<PhoneType>();

            for (int newPhoneTypeIndex = 11; newPhoneTypeIndex <= 20; newPhoneTypeIndex++)
            {
                newPhoneTypeList.Add(new PhoneType(newPhoneTypeIndex, "phonetype:" + newPhoneTypeIndex.ToString()));
            }
            PhoneTypeAccess.AddMultipleNewPhoneTypes(newPhoneTypeList);

            List<PhoneType> actual = new List<PhoneType>();

            for (int actualIndex = 11; actualIndex <= 20; actualIndex++)
            {
                actual.Add(PhoneTypeAccess.GetPhoneTypeById(actualIndex));
            }

            for (int assertIndex = 0; assertIndex < newPhoneTypeList.Count; assertIndex++)
            {
                Assert.IsTrue(newPhoneTypeList[assertIndex].Equals(actual[assertIndex]));
            }
        }

        /// <summary>
        ///A test for AddNewPhoneType
        ///</summary>
        [TestMethod()]
        public void AddNewPhoneTypeTest()
        {
            PhoneType newPhoneType = new PhoneType(21, "phonetype:21");
            Enums.CRUDResults result = PhoneTypeAccess.AddNewPhoneType(newPhoneType);
            PhoneType actual = PhoneTypeAccess.GetPhoneTypeById(21);
            Assert.AreEqual(Enums.CRUDResults.CREATE_SUCCESS, result);
            Assert.IsTrue(newPhoneType.Equals(actual));
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
            t_phone_types dbTypePhoneType = new t_phone_types();
            dbTypePhoneType.C_id = 3;
            dbTypePhoneType.type_name = "cell phone";
            int expectedId = 3;
            string expectedTypeName = "cell phone";
            PhoneType actual;
            actual = PhoneTypeAccess_Accessor.ConvertSingleDbPhoneTypeToLocalType(dbTypePhoneType);
            Assert.AreEqual(expectedId, actual._Id);
            Assert.AreEqual(expectedTypeName, actual.PhoneTypeName);
        }

        /// <summary>
        ///A test for ConvertMultiplePhoneTypesToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultiplePhoneTypesToLocalTypeTest()
        {
            List<t_phone_types> dbTypePhoneTypeList = new List<t_phone_types>();
            List<PhoneType> expected = new List<PhoneType>();            
            for (int i = 0; i < 10; i++)
            {
                t_phone_types toAdd = new t_phone_types();
                toAdd.C_id = i;
                toAdd.type_name = "Type name number: " + i.ToString();
                dbTypePhoneTypeList.Add(toAdd);
                expected.Add(PhoneTypeAccess_Accessor.ConvertSingleDbPhoneTypeToLocalType(toAdd));
            }
            List<PhoneType> actual;
            actual = PhoneTypeAccess_Accessor.ConvertMultipleDbPhoneTypesToLocalType(dbTypePhoneTypeList);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(expected[i].Equals(actual[i]));
            }
        }

        /// <summary>
        ///A test for ConvertSinglePhoneTypeToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSinglePhoneTypeToDbTypeTest()
        {
            PhoneType localTypePhoneType = new PhoneType(1, "phonetype:1");
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = localTypePhoneType.PhoneTypeName;
            t_phone_types actual;
            actual = PhoneTypeAccess_Accessor.ConvertSingleLocalPhoneTypeToDbType(localTypePhoneType);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.type_name, actual.type_name);
        }

        /// <summary>
        ///A test for ConvertMultiplePhoneTypesToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultiplePhoneTypesToDbTypeTest()
        {
            List<PhoneType> dbTypePhoneTypeList = new List<PhoneType>();
            List<t_phone_types> expected = new List<t_phone_types>();
            for (int i = 0; i < 10; i++)
            {
                PhoneType toAdd = new PhoneType();
                toAdd._Id = i;
                toAdd.PhoneTypeName = "Type name number: " + i.ToString();
                dbTypePhoneTypeList.Add(toAdd);
                expected.Add(PhoneTypeAccess_Accessor.ConvertSingleLocalPhoneTypeToDbType(toAdd));
            }
            List<t_phone_types> actual;
            actual = PhoneTypeAccess_Accessor.ConvertMultipleLocalPhoneTypesToDbType(dbTypePhoneTypeList);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(expected[i].C_id == actual[i].C_id);
                Assert.IsTrue(expected[i].type_name == actual[i].type_name);
            }
        }

        #endregion

        #region Delete Tests

        /// <summary>
        ///Deleting a list of phone types that exist
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePhoneTypesTest()
        {
            List<PhoneType> deletedPhoneTypeList = new List<PhoneType>() 
            {
                new PhoneType(2, "phonetype:2"),
                new PhoneType(3, "phonetype:3")
            };
            PhoneTypeAccess.DeleteMultiplePhoneTypes(deletedPhoneTypeList);
            List<PhoneType> allPhoneTypes = PhoneTypeAccess.GetAllPhoneTypes();

            for (int i = 0; i < deletedPhoneTypeList.Count; i++)
            {
                Assert.IsFalse(allPhoneTypes.Contains(deletedPhoneTypeList[i]));
            }
        }

        /// <summary>
        ///Deleting a phone type that exists
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePhoneTypeTest()
        {
            PhoneType deletedPhoneType = new PhoneType(4,"phonetype:4");
            Enums.CRUDResults result = PhoneTypeAccess.DeleteSinglePhoneType(deletedPhoneType);
            List<PhoneType> allPhoneTypes = PhoneTypeAccess.GetAllPhoneTypes();
            Assert.AreEqual(Enums.CRUDResults.DELETE_SUCCESS, result);
            Assert.IsFalse(allPhoneTypes.Contains(deletedPhoneType));
        }

        /// <summary>
        ///Deleting a phone type that does not exist
        ///</summary>
        [TestMethod()]
        public void DeleteNotExistSinglePhoneTypeTest()
        {
            PhoneType deletedPhoneType = new PhoneType(50, "phonetype:50");
            Enums.CRUDResults result = PhoneTypeAccess.DeleteSinglePhoneType(deletedPhoneType);
            List<PhoneType> allPhoneTypes = PhoneTypeAccess.GetAllPhoneTypes();
            Assert.AreEqual(Enums.CRUDResults.DELETE_FAIL, result);
        }

        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllPhoneTypes
        ///</summary>
        [TestMethod()]
        public void GetAllPhoneTypesTest()
        {
            List<PhoneType> actual;
            actual = PhoneTypeAccess.GetAllPhoneTypes();

            Assert.IsInstanceOfType(actual, typeof(List<PhoneType>));
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for GetPhoneTypeById
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByIdTest()
        {
            int id = 1;
            PhoneType expected = new PhoneType(1, "phonetype:1");
            PhoneType actual;
            actual = PhoneTypeAccess.GetPhoneTypeById(id);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByTypeNameTest()
        {
            string typename = "phonetype:1";
            PhoneType expected = new PhoneType(1, "phonetype:1");
            PhoneType actual;
            actual = PhoneTypeAccess.GetPhoneTypeByTypeName(typename);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetPhoneTypeById
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByNonExistintIdTest()
        {
            int id = 50;
            PhoneType actual = PhoneTypeAccess.GetPhoneTypeById(id);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByNonExistintTypeNameTest()
        {
            string typeName = "phonetype:50";
            PhoneType actual = PhoneTypeAccess.GetPhoneTypeByTypeName(typeName);
            Assert.IsNull(actual);
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
            List<t_phone_types> actual;
            actual = PhoneTypeAccess_Accessor.LookupAllPhoneTypes();

            Assert.IsInstanceOfType(actual, typeof(List<t_phone_types>));
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for LookupPhoneTypById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneTypByIdTest()
        {
            int ID = 1;
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = "phonetype:1";
            t_phone_types actual;
            actual = PhoneTypeAccess_Accessor.LookupPhoneTypeById(ID);
            Assert.IsTrue(PhoneTypeAccess_Accessor.ConvertSingleDbPhoneTypeToLocalType(expected).
                Equals(PhoneTypeAccess_Accessor.ConvertSingleDbPhoneTypeToLocalType(actual)));
        }

        /// <summary>
        ///A test for LookupPhoneTypById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneTypByNonExistintIdTest()
        {
            int ID = 50;
            t_phone_types actual;
            actual = PhoneTypeAccess_Accessor.LookupPhoneTypeById(ID);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for LookupPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneTypeByTypeNameTest()
        {
            string typeName = "phonetype:1";
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = "phonetype:1";
            t_phone_types actual;
            actual = PhoneTypeAccess_Accessor.LookupPhoneTypeByTypeName(typeName);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.type_name, actual.type_name);
        }

        /// <summary>
        ///A test for LookupPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneTypeByNonExistintTypeNameTest()
        {
            string typeName = "phonetype:50";
            t_phone_types actual;
            actual = PhoneTypeAccess_Accessor.LookupPhoneTypeByTypeName(typeName);
            Assert.IsNull(actual);
        }
        
        #endregion

        #region Update Tests

        /// <summary>
        ///A test for UpdateMultiplePhoneTypes
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePhoneTypesTest()
        {
            List<PhoneType> updatedPhoneTypeList = new List<PhoneType>()
            {
                new PhoneType(5, "updatedphonetype:5"),
                new PhoneType(6, "updatedphonetype:6")
            };

            PhoneTypeAccess.UpdateMultiplePhoneTypes(updatedPhoneTypeList);

            List<PhoneType> actual = new List<PhoneType>()
            {
                PhoneTypeAccess.GetPhoneTypeById(5),
                PhoneTypeAccess.GetPhoneTypeById(6)
            };

            for (int i = 0; i < updatedPhoneTypeList.Count; i++)
            {
                Assert.IsTrue(updatedPhoneTypeList[i].Equals(actual[i]));
            }
        }

        /// <summary>
        ///A test for UpdateSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePhoneTypeTest()
        {
            PhoneType updatedPhoneType = new PhoneType(7, "updatedphonetype:7");
            Enums.CRUDResults result = PhoneTypeAccess.UpdateSinglePhoneType(updatedPhoneType);
            PhoneType actual = PhoneTypeAccess.GetPhoneTypeById(7);
            Assert.AreEqual(Enums.CRUDResults.UPDATE_SUCCESS, result);
            Assert.IsTrue(updatedPhoneType.Equals(actual));
        }

        /// <summary>
        ///A test for UpdateSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePhoneTypeFailTest()
        {
            PhoneType updatedPhoneType = new PhoneType(50, "updatedphonetype:7");
            Enums.CRUDResults result = PhoneTypeAccess.UpdateSinglePhoneType(updatedPhoneType);
            PhoneType actual = PhoneTypeAccess.GetPhoneTypeById(7);
            Assert.AreEqual(Enums.CRUDResults.UPDATE_FAIL, result);
        }

        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void UpsertUpdateSinglePhoneTypeTest()
        {
            PhoneType upsertedPhoneType = new PhoneType(8, "can you guess??");
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = PhoneTypeAccess.UpsertSinglePhoneType(upsertedPhoneType);
            Assert.AreEqual(expected, actual);
            PhoneType afterUpdate = PhoneTypeAccess.GetPhoneTypeById(8);
        }

        /// <summary>
        ///A test for UpsertSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void UpsertAddSinglePhoneTypeTest()
        {
            PhoneType upsertedPhoneType = new PhoneType(613, "all the best in the world");
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = PhoneTypeAccess.UpsertSinglePhoneType(upsertedPhoneType);
            Assert.AreEqual(expected, actual);
            List<PhoneType> afterUpdate = PhoneTypeAccess.GetAllPhoneTypes();
            Assert.IsTrue(afterUpdate.Contains(upsertedPhoneType));
        }
        
        #endregion
    }
}
