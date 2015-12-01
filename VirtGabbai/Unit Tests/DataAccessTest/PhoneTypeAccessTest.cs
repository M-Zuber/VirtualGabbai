using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using LocalTypes;
using System.Collections.Generic;
using DataCache;
using Framework;
using Helpers.UnitTests.Extensions;
using DataCache.Models;

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
                Cache.CacheData.t_phone_types.Add(t_phone_types.Createt_phone_types(1, "phonetype:1"));
            }
            for (int newPhoneTypeIndex = 2; newPhoneTypeIndex <= 10; newPhoneTypeIndex++)
            {
                Cache.CacheData.t_phone_types.Add(
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
            Cache.CacheData.clear_database();
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
            PhoneTypeAccess access = new PhoneTypeAccess();
            List<PhoneType> newPhoneTypeList = new List<PhoneType>();

            for (int newPhoneTypeIndex = 11; newPhoneTypeIndex <= 20; newPhoneTypeIndex++)
            {
                newPhoneTypeList.Add(new PhoneType(newPhoneTypeIndex, "phonetype:" + newPhoneTypeIndex.ToString()));
            }
            access.AddMultiple(newPhoneTypeList);

            List<PhoneType> actual = new List<PhoneType>();

            for (int actualIndex = 11; actualIndex <= 20; actualIndex++)
            {
                actual.Add(access.GetByID(actualIndex));
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
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType newPhoneType = new PhoneType(21, "phonetype:21");
            Enums.CRUDResults result = access.AddSingle(newPhoneType);
            PhoneType actual = access.GetByID(21);
            Assert.AreEqual(Enums.CRUDResults.CREATE_SUCCESS, result);
            Assert.IsTrue(newPhoneType.Equals(actual));
        }

        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConverSinglePhoneTypeToLocalType
        ///</summary>
        [TestMethod()]

        public void ConverSinglePhoneTypeToLocalTypeTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            t_phone_types dbTypePhoneType = new t_phone_types();
            dbTypePhoneType.C_id = 3;
            dbTypePhoneType.type_name = "cell phone";
            int expectedId = 3;
            string expectedTypeName = "cell phone";
            PhoneType actual =
                (PhoneType)access.InvokeStaticPrivateMethod("ConvertSingleToLocalType", dbTypePhoneType);
            Assert.AreEqual(expectedId, actual._Id);
            Assert.AreEqual(expectedTypeName, actual.PhoneTypeName);
        }

        /// <summary>
        ///A test for ConvertMultiplePhoneTypesToLocalType
        ///</summary>
        [TestMethod()]

        public void ConvertMultiplePhoneTypesToLocalTypeTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            List<t_phone_types> dbTypePhoneTypeList = new List<t_phone_types>();
            List<PhoneType> expected = new List<PhoneType>();
            for (int i = 0; i < 10; i++)
            {
                t_phone_types toAdd = new t_phone_types();
                toAdd.C_id = i;
                toAdd.type_name = "Type name number: " + i.ToString();
                dbTypePhoneTypeList.Add(toAdd);
                expected.Add((PhoneType)access.InvokeStaticPrivateMethod("ConvertSingleToLocalType", toAdd));
            }
            List<PhoneType> actual;
            actual =
                (List<PhoneType>)access.InvokeStaticPrivateMethod("ConvertMultipleToLocalType", dbTypePhoneTypeList);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(expected[i].Equals(actual[i]));
            }
        }

        /// <summary>
        ///A test for ConvertSinglePhoneTypeToDbType
        ///</summary>
        [TestMethod()]

        public void ConvertSinglePhoneTypeToDbTypeTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType localTypePhoneType = new PhoneType(1, "phonetype:1");
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = localTypePhoneType.PhoneTypeName;
            t_phone_types actual;
            actual = (t_phone_types)access.InvokeStaticPrivateMethod("ConvertSingleToDBType", localTypePhoneType);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.type_name, actual.type_name);
        }

        /// <summary>
        ///A test for ConvertMultiplePhoneTypesToDbType
        ///</summary>
        [TestMethod()]

        public void ConvertMultiplePhoneTypesToDbTypeTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            List<PhoneType> dbTypePhoneTypeList = new List<PhoneType>();
            List<t_phone_types> expected = new List<t_phone_types>();
            for (int i = 0; i < 10; i++)
            {
                PhoneType toAdd = new PhoneType(i, "Type name number: " + i);
                dbTypePhoneTypeList.Add(toAdd);
                expected.Add((t_phone_types)access.InvokeStaticPrivateMethod("ConvertSingleToDBType", toAdd));
            }
            List<t_phone_types> actual;
            actual =
                (List<t_phone_types>)access.InvokeStaticPrivateMethod("ConvertMultipleToDBType", dbTypePhoneTypeList);
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
            PhoneTypeAccess access = new PhoneTypeAccess();
            List<PhoneType> deletedPhoneTypeList = new List<PhoneType>() 
            {
                new PhoneType(2, "phonetype:2"),
                new PhoneType(3, "phonetype:3")
            };

            access.DeleteMultiple(deletedPhoneTypeList);
            IEnumerable<PhoneType> allPhoneTypes = access.GetAll();

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
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType deletedPhoneType = new PhoneType(4, "phonetype:4");
            Enums.CRUDResults result = access.DeleteSingle(deletedPhoneType);
            IEnumerable<PhoneType> allPhoneTypes = access.GetAll();
            Assert.AreEqual(Enums.CRUDResults.DELETE_SUCCESS, result);
            Assert.IsFalse(allPhoneTypes.Contains(deletedPhoneType));
        }

        /// <summary>
        ///Deleting a phone type that does not exist
        ///</summary>
        [TestMethod()]
        public void DeleteNotExistSinglePhoneTypeTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType deletedPhoneType = new PhoneType(50, "phonetype:50");
            Enums.CRUDResults result = access.DeleteSingle(deletedPhoneType);

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
            PhoneTypeAccess access = new PhoneTypeAccess();
            IEnumerable<PhoneType> actual = access.GetAll();

            Assert.IsInstanceOfType(actual, typeof(List<PhoneType>));
            Assert.IsTrue(actual.Count() > 0);
        }

        /// <summary>
        ///A test for GetPhoneTypeById
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByIdTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            int id = 1;
            PhoneType expected = new PhoneType(1, "phonetype:1");
            PhoneType actual = access.GetByID(id);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByTypeNameTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            string typename = "phonetype:1";
            PhoneType expected = new PhoneType(1, "phonetype:1");
            PhoneType actual;
            actual = access.GetByPhoneTypeName(typename);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetPhoneTypeById
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByNonExistintIdTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            int id = 50;
            PhoneType actual = access.GetByID(id);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByNonExistintTypeNameTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            string typeName = "phonetype:50";
            PhoneType actual = access.GetByPhoneTypeName(typeName);
            Assert.IsNull(actual);
        }

        #endregion

        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllPhoneTypes
        ///</summary>
        [TestMethod()]
        public void LookupAllPhoneTypesTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            IEnumerable<t_phone_types> actual;
            actual = (IEnumerable<t_phone_types>)access.InvokePrivateMethod("LookupAll");

            Assert.IsInstanceOfType(actual, typeof(IEnumerable<t_phone_types>));
            Assert.IsTrue(actual.Count() > 0);
        }

        /// <summary>
        ///A test for LookupPhoneTypById
        ///</summary>
        [TestMethod()]

        public void LookupPhoneTypByIdTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            int ID = 1;
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = "phonetype:1";
            t_phone_types actual;
            actual = (t_phone_types)new PhoneTypeAccess().InvokePrivateMethod("LookupByID", ID);
            Assert.IsTrue(access.InvokePrivateMethod("ConvertSingleToLocalType", expected).Equals(
                (PhoneType)access.InvokePrivateMethod("ConvertSingleToLocalType", actual)));
        }

        /// <summary>
        ///A test for LookupPhoneTypById
        ///</summary>
        [TestMethod()]

        public void LookupPhoneTypByNonExistintIdTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            int ID = 50;
            t_phone_types actual;
            actual = (t_phone_types)access.InvokePrivateMethod("LookupByID", ID);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for LookupPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void LookupPhoneTypeByTypeNameTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            string typeName = "phonetype:1";
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = "phonetype:1";
            t_phone_types actual;
            actual = (t_phone_types)access.InvokePrivateMethod("LookupByPhoneTypeName", typeName);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.type_name, actual.type_name);
        }

        /// <summary>
        ///A test for LookupPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void LookupPhoneTypeByNonExistintTypeNameTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            string typeName = "phonetype:50";
            t_phone_types actual;
            actual = (t_phone_types)access.InvokePrivateMethod("LookupByPhoneTypeName", typeName);
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
            PhoneTypeAccess access = new PhoneTypeAccess();
            List<PhoneType> updatedPhoneTypeList = new List<PhoneType>()
            {
                new PhoneType(5, "updatedphonetype:5"),
                new PhoneType(6, "updatedphonetype:6")
            };

            access.UpdateMultiple(updatedPhoneTypeList);

            List<PhoneType> actual = new List<PhoneType>()
            {
                access.GetByID(5),
                access.GetByID(6)
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
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType updatedPhoneType = new PhoneType(7, "updatedphonetype:7");
            Enums.CRUDResults result = access.UpdateSingle(updatedPhoneType);
            PhoneType actual = access.GetByID(7);
            Assert.AreEqual(Enums.CRUDResults.UPDATE_SUCCESS, result);
            Assert.IsTrue(updatedPhoneType.Equals(actual));
        }

        /// <summary>
        ///A test for UpdateSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePhoneTypeFailTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType updatedPhoneType = new PhoneType(50, "updatedphonetype:7");
            Enums.CRUDResults result = access.UpdateSingle(updatedPhoneType);
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
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType upsertedPhoneType = new PhoneType(8, "can you guess??");
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = access.UpsertSingle(upsertedPhoneType);
            Assert.AreEqual(expected, actual);
            PhoneType afterUpdate = access.GetByID(8);
        }

        /// <summary>
        ///A test for UpsertSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void UpsertAddSinglePhoneTypeTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            PhoneType upsertedPhoneType = new PhoneType(613, "all the best in the world");
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = access.UpsertSingle(upsertedPhoneType);
            Assert.AreEqual(expected, actual);
            IEnumerable<PhoneType> afterUpdate = access.GetAll();
            Assert.IsTrue(afterUpdate.Contains(upsertedPhoneType));
        }

        #endregion

        #region Helper Method Tests

        [TestMethod]
        public void GetMaxIDTest()
        {
            PhoneTypeAccess access = new PhoneTypeAccess();
            access.AddSingle(new PhoneType(1001,"max id test"));

            Assert.AreEqual(1001, access.GetMaxID());
        }

        #endregion
    }
}
