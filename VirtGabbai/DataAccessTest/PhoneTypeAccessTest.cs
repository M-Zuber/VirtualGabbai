using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            for (int newPhoneTypeIndex = 1; newPhoneTypeIndex <= 10; newPhoneTypeIndex++)
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
            PhoneTypeAccess target = new PhoneTypeAccess();
            List<PhoneType> newPhoneTypeList = new List<PhoneType>();

            for (int newPhoneTypeIndex = 11; newPhoneTypeIndex <= 20; newPhoneTypeIndex++)
            {
                newPhoneTypeList.Add(new PhoneType(newPhoneTypeIndex, "phonetype:" + newPhoneTypeIndex.ToString()));
            }
            target.AddMultipleNewPhoneTypes(newPhoneTypeList);

            List<PhoneType> actual = new List<PhoneType>();

            for (int actualIndex = 11; actualIndex <= 20; actualIndex++)
            {
                actual.Add(target.GetPhoneTypeById(actualIndex));
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
            PhoneTypeAccess target = new PhoneTypeAccess();
            PhoneType newPhoneType = new PhoneType(21, "phonetype:21");
            target.AddNewPhoneType(newPhoneType);
            PhoneType actual = target.GetPhoneTypeById(21);
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
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor();
            t_phone_types dbTypePhoneType = new t_phone_types();
            dbTypePhoneType.C_id = 3;
            dbTypePhoneType.type_name = "cell phone";
            int expectedId = 3;
            string expectedTypeName = "cell phone";
            PhoneType actual;
            actual = target.ConvertSingleDbPhoneTypeToLocalType(dbTypePhoneType);
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
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor();
            List<t_phone_types> dbTypePhoneTypeList = new List<t_phone_types>();
            List<PhoneType> expected = new List<PhoneType>();            
            for (int i = 0; i < 10; i++)
            {
                t_phone_types toAdd = new t_phone_types();
                toAdd.C_id = i;
                toAdd.type_name = "Type name number: " + i.ToString();
                dbTypePhoneTypeList.Add(toAdd);
                expected.Add(target.ConvertSingleDbPhoneTypeToLocalType(toAdd));
            }
            List<PhoneType> actual;
            actual = target.ConvertMultipleDbPhoneTypesToLocalType(dbTypePhoneTypeList);
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
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor();
            PhoneType localTypePhoneType = new PhoneType(1, "phonetype:1");
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = localTypePhoneType.PhoneTypeName;
            t_phone_types actual;
            actual = target.ConvertSingleLocalPhoneTypeToDbType(localTypePhoneType);
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
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor();
            List<PhoneType> dbTypePhoneTypeList = new List<PhoneType>();
            List<t_phone_types> expected = new List<t_phone_types>();
            for (int i = 0; i < 10; i++)
            {
                PhoneType toAdd = new PhoneType();
                toAdd._Id = i;
                toAdd.PhoneTypeName = "Type name number: " + i.ToString();
                dbTypePhoneTypeList.Add(toAdd);
                expected.Add(target.ConvertSingleLocalPhoneTypeToDbType(toAdd));
            }
            List<t_phone_types> actual;
            actual = target.ConvertMultipleLocalPhoneTypesToDbType(dbTypePhoneTypeList);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(expected[i].C_id == actual[i].C_id);
                Assert.IsTrue(expected[i].type_name == actual[i].type_name);
            }
        }

        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultiplePhoneTypes
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePhoneTypesTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess();
            List<PhoneType> deletedPhoneTypeList = new List<PhoneType>() 
            {
                new PhoneType(2, "phonetype:2"),
                new PhoneType(3, "phonetype:3")
            };
            target.DeleteMultiplePhoneTypes(deletedPhoneTypeList);
            List<PhoneType> allPhoneTypes = target.GetAllPhoneTypes();
            for (int i = 0; i < deletedPhoneTypeList.Count; i++)
            {
                Assert.IsFalse(allPhoneTypes.Contains(deletedPhoneTypeList[i]));
            }
        }

        /// <summary>
        ///A test for DeleteSinglePhoneType
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePhoneTypeTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess();
            PhoneType deletedPhoneType = new PhoneType(4,"phonetype:4");
            target.DeleteSinglePhoneType(deletedPhoneType);
            List<PhoneType> allPhoneTypes = target.GetAllPhoneTypes();
            Assert.IsFalse(allPhoneTypes.Contains(deletedPhoneType));
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllPhoneTypes
        ///</summary>
        [TestMethod()]
        public void GetAllPhoneTypesTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess();

            List<PhoneType> actual;
            actual = target.GetAllPhoneTypes();

            Assert.IsInstanceOfType(actual, typeof(List<PhoneType>));
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for GetPhoneTypeById
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByIdTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess();
            int id = 1;
            PhoneType expected = new PhoneType(1, "phonetype:1");
            PhoneType actual;
            actual = target.GetPhoneTypeById(id);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        public void GetPhoneTypeByTypeNameTest()
        {
            PhoneTypeAccess target = new PhoneTypeAccess();
            string typename = "phonetype:1";
            PhoneType expected = new PhoneType(1, "phonetype:1");
            PhoneType actual;
            actual = target.GetPhoneTypeByTypeName(typename);
            Assert.IsTrue(expected.Equals(actual));
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
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor();

            List<t_phone_types> actual;
            actual = target.LookupAllPhoneTypes();

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
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor();
            int ID = 1;
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = "phonetype:1";
            t_phone_types actual;
            actual = target.LookupPhoneTypeById(ID);
            Assert.IsTrue(target.ConvertSingleDbPhoneTypeToLocalType(expected).
                Equals(target.ConvertSingleDbPhoneTypeToLocalType(actual)));
        }

        /// <summary>
        ///A test for LookupPhoneTypeByTypeName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneTypeByTypeNameTest()
        {
            PhoneTypeAccess_Accessor target = new PhoneTypeAccess_Accessor();
            string typeName = "phonetype:1";
            t_phone_types expected = new t_phone_types();
            expected.C_id = 1;
            expected.type_name = "phonetype:1";
            t_phone_types actual;
            actual = target.LookupPhoneTypeByTypeName(typeName);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.type_name, actual.type_name);
        }
        
        #endregion

        #region Update Tests

        /// <summary>
        ///A test for UpdateMultiplePhoneTypes
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePhoneTypesTest()
        {//5,6
            PhoneTypeAccess target = new PhoneTypeAccess();
            List<PhoneType> updatedPhoneTypeList = new List<PhoneType>()
            {
                new PhoneType(5, "updatedphonetype:5"),
                new PhoneType(6, "updatedphonetype:6")
            };
            
            target.UpdateMultiplePhoneTypes(updatedPhoneTypeList);

            List<PhoneType> actual = new List<PhoneType>()
            {
                target.GetPhoneTypeById(5),
                target.GetPhoneTypeById(6)
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
            PhoneTypeAccess target = new PhoneTypeAccess();
            PhoneType updatedPhoneType = new PhoneType(7, "updatedphonetype:7");
            target.UpdateSinglePhoneType(updatedPhoneType);
            PhoneType actual = target.GetPhoneTypeById(7);
            Assert.IsTrue(updatedPhoneType.Equals(actual));
        }

        #endregion
    }
}
