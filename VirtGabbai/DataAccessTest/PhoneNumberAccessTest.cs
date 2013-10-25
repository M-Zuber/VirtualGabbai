using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using DataCache;
using System.Linq;
using Framework;

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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            if (!Cache.CacheData.t_people.Any(person => person.C_id == 1))
            {
                Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            }

            if (!Cache.CacheData.t_phone_types.Any(numberType => numberType.C_id == 1))
            {
                Cache.CacheData.t_phone_types.AddObject(t_phone_types.Createt_phone_types(1, "phonetype:1"));
            }
            for (int newPhoneNumberIndex = 1; newPhoneNumberIndex <= 10; newPhoneNumberIndex++)
            {
                var newPhoneNumber = t_phone_numbers.Createt_phone_numbers(
                    1,"phone number:" + newPhoneNumberIndex.ToString(), 1, newPhoneNumberIndex);
                Cache.CacheData.t_phone_numbers.AddObject(newPhoneNumber);
            }
            Cache.CacheData.SaveChanges();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var allCurrentPeople = (from currP in Cache.CacheData.t_people select currP).ToList<t_people>();
            for (int i = 0; i < allCurrentPeople.Count; i++)
            {
                Cache.CacheData.DeleteObject(allCurrentPeople[i]);
            }

            var allCurrentPhoneTypes = (from currPT in Cache.CacheData.t_phone_types select currPT).ToList<t_phone_types>();
            for (int i = 0; i < allCurrentPhoneTypes.Count; i++)
            {
                Cache.CacheData.DeleteObject(allCurrentPhoneTypes[i]);
            }
            
            var allCurrentPhoneNumbers = (from currPN in Cache.CacheData.t_phone_numbers select currPN).ToList<t_phone_numbers>();
            for (int i = 0; i < allCurrentPhoneNumbers.Count; i++)
            {
                Cache.CacheData.DeleteObject(allCurrentPhoneNumbers[i]);
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
            List<PhoneNumber> newPhoneNumberList = new List<PhoneNumber>();

            for (int i = 11; i <= 20; i++)
            {
                PhoneNumber newNumber = new PhoneNumber(i, "phone number:" + i.ToString(), new PhoneType(1, "phonetype:1"));
                newPhoneNumberList.Add(newNumber);
            }
            PhoneNumberAccess.AddMultipleNewPhoneTypes(newPhoneNumberList, 1);

            List<PhoneNumber> actual = new List<PhoneNumber>();

            for (int i = 11; i <= 20; i++)
            {
                actual.Add(PhoneNumberAccess.GetPhoneNumberById(i));
            }

            CollectionAssert.AreEqual(newPhoneNumberList, actual);
        }

        /// <summary>
        ///A test for AddNewPhoneNumber
        ///</summary>
        [TestMethod()]
        public void AddNewPhoneNumberTest()
        {
            PhoneNumber newPhoneNumber = new PhoneNumber(21, "phone number:1", new PhoneType(1, "phonetype:1"));
            Enums.CRUDResults result = PhoneNumberAccess.AddNewPhoneNumber(newPhoneNumber, 1);
            PhoneNumber actual = PhoneNumberAccess.GetPhoneNumberById(21);
            Assert.AreEqual(Enums.CRUDResults.CREATE_SUCCESS, result);
            Assert.AreEqual(newPhoneNumber, actual);
        }

        /// <summary>
        ///A test for AddNewPhoneNumber
        ///</summary>
        [TestMethod()]
        public void AddNewPhoneNumberWithNonExistintTypeTest()
        {
            PhoneNumber newPhoneNumber = new PhoneNumber(22, "phone number:1", new PhoneType(100, "phonetype:1"));
            Enums.CRUDResults result = PhoneNumberAccess.AddNewPhoneNumber(newPhoneNumber, 1);
            PhoneNumber actual = PhoneNumberAccess.GetPhoneNumberById(22);
            Assert.AreEqual(Enums.CRUDResults.CREATE_SUCCESS, result);
            Assert.AreEqual(newPhoneNumber, actual);
        }
        
        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConvertMultipleDbPhoneNumbersToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleDbPhoneNumbersToLocalTypeTest()
        {
            List<t_phone_numbers> dbTypePhoneNumberList = new List<t_phone_numbers>();
            List<PhoneNumber> expected = new List<PhoneNumber>();
            for (int i = 0; i < 5; i++)
            {
                t_phone_numbers toAdd = t_phone_numbers.Createt_phone_numbers(1, "phone number:" + i.ToString(), 1, 1);
                dbTypePhoneNumberList.Add(toAdd);
                expected.Add(PhoneNumberAccess_Accessor.ConvertSingleDbPhoneNumberToLocalType(toAdd));
            }
            List<PhoneNumber> actual;
            actual = PhoneNumberAccess_Accessor.ConvertMultipleDbPhoneNumbersToLocalType(dbTypePhoneNumberList);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(expected[i].Equals(actual[i]));
            }
        }

        /// <summary>
        ///A test for ConvertMultipleLocalPhoneNumbersToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleLocalPhoneNumbersToDbTypeTest()
        {
            List<PhoneNumber> localTypePhoneNumberList = new List<PhoneNumber>();
            List<t_phone_numbers> expected = new List<t_phone_numbers>();
            for (int i = 0; i < 5; i++)
            {
                t_phone_numbers toAdd = t_phone_numbers.Createt_phone_numbers(1, "phone number:" + i.ToString(), 1, 1);
                localTypePhoneNumberList.Add(PhoneNumberAccess_Accessor.ConvertSingleDbPhoneNumberToLocalType(toAdd));
                expected.Add(toAdd);
            }
            List<t_phone_numbers> actual;
            actual = PhoneNumberAccess_Accessor.ConvertMultipleLocalPhoneNumbersToDbType(localTypePhoneNumberList, 1);
            List<PhoneNumber> localExpected = PhoneNumberAccess_Accessor.ConvertMultipleDbPhoneNumbersToLocalType(expected);
            List<PhoneNumber> localActual = PhoneNumberAccess_Accessor.ConvertMultipleDbPhoneNumbersToLocalType(actual);

            for (int i = 0; i < localExpected.Count; i++)
            {
                Assert.IsTrue(localExpected[i].Equals(localActual[i]));
            }
        }

        /// <summary>
        ///A test for ConvertSingleDbPhoneNumberToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleDbPhoneNumberToLocalTypeTest()
        {
            t_phone_numbers dbTypePhoneNumber = t_phone_numbers.Createt_phone_numbers(1, "phone number:1", 1, 1); 
            PhoneNumber expected = new PhoneNumber(1, "phone number:1", new PhoneType(1, "phonetype:1"));
            PhoneNumber actual;
            actual = PhoneNumberAccess_Accessor.ConvertSingleDbPhoneNumberToLocalType(dbTypePhoneNumber);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for ConvertSingleLocalPhoneNumberToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleLocalPhoneNumberToDbTypeTest()
        {
            PhoneNumber localTypePhoneNumber = new PhoneNumber(1, "phone number:1", new PhoneType(1, "phonetype:1"));
            t_phone_numbers expected = t_phone_numbers.Createt_phone_numbers(1, "phone number:1", 1, 1);
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.ConvertSingleLocalPhoneNumberToDbType(localTypePhoneNumber, 1);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.number, actual.number);
            Assert.AreEqual(expected.number_type, actual.number_type);
            Assert.AreEqual(expected.person_id, actual.person_id);
        }
        
        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultiplePhoneNumbers
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePhoneNumbersTest()
        {
            List<PhoneNumber> deletedPhoneNumberList = new List<PhoneNumber>() 
            {
                new PhoneNumber(2,"phone number:2", new PhoneType(1, "phonetype:1")),
                new PhoneNumber(3,"phone number:3", new PhoneType(1, "phonetype:1"))
            };

            PhoneNumberAccess.DeleteMultiplePhoneNumbers(deletedPhoneNumberList);

            List<PhoneNumber> allNumbers = PhoneNumberAccess.GetAllPhoneNumbers(1);
            for (int i = 0; i < deletedPhoneNumberList.Count; i++)
			{
			 Assert.IsFalse(allNumbers.Contains(deletedPhoneNumberList[i]));
			}
        }

        /// <summary>
        ///A test for DeleteSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePhoneNumberTest()
        {
            PhoneNumber deletedPhoneNumber = new PhoneNumber(4, "phone number:4", new PhoneType(1, "phonetype:1"));
            Enums.CRUDResults result = PhoneNumberAccess.DeleteSinglePhoneNumber(deletedPhoneNumber);
            List<PhoneNumber> allPhoneNumbers = PhoneNumberAccess.GetAllPhoneNumbers(1);
            Assert.AreEqual(Enums.CRUDResults.DELETE_SUCCESS, result);
            Assert.IsFalse(allPhoneNumbers.Contains(deletedPhoneNumber));
        }

        /// <summary>
        ///A test for DeleteSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExistintPhoneNumberTest()
        {
            PhoneNumber deletedPhoneNumber = new PhoneNumber(50, "phone number:4", new PhoneType(1, "phonetype:1"));
            Enums.CRUDResults result = PhoneNumberAccess.DeleteSinglePhoneNumber(deletedPhoneNumber);
            Assert.AreEqual(Enums.CRUDResults.DELETE_FAIL, result);
        }

        #endregion

        #region Get Tests
        
        /// <summary>
        ///A test for GetAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        public void GetAllPhoneNumbersTest()
        {
            int personId = 1;
            List <t_phone_numbers> expected = (from currNum in Cache.CacheData.t_phone_numbers
                                                 select currNum).ToList<t_phone_numbers>();
            List<PhoneNumber> localExpected = PhoneNumberAccess_Accessor.ConvertMultipleDbPhoneNumbersToLocalType(expected);
            List<PhoneNumber> actual;
            actual = PhoneNumberAccess.GetAllPhoneNumbers(personId);
            List<t_phone_numbers> dbActual = PhoneNumberAccess_Accessor.ConvertMultipleLocalPhoneNumbersToDbType(actual, 1);
            for (int i = 0; i < expected.Count; i++)
            {
                if ((expected[i].person_id == personId) && (!actual.Contains(localExpected[i])))
                {
                    Assert.Fail();
                }
                if ((expected[i].person_id != personId) && (actual.Contains(localExpected[i])))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        ///A test for GetAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        public void GetAllPhoneNumbersOfNonExistintPersonTest()
        {
            int personId = 50;
            List<PhoneNumber> actual = PhoneNumberAccess.GetAllPhoneNumbers(personId);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetPhoneNumberById
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByIdTest()
        {
            int id = 1;
            PhoneNumber expected = new PhoneNumber(1, "phone number:1", new PhoneType(1, "phonetype:1"));
            PhoneNumber actual;
            actual = PhoneNumberAccess.GetPhoneNumberById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPhoneNumberById
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByNonExistintIdTest()
        {
            int id = 50;
            PhoneNumber actual;
            actual = PhoneNumberAccess.GetPhoneNumberById(id);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetPhoneNumberByType
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByTypeTest()
        {
            PhoneType searchedType = new PhoneType(1, "phonetype:1");
            var allNumbers = (from currNum in Cache.CacheData.t_phone_numbers
                              select currNum).ToList<t_phone_numbers>();
            List<PhoneNumber> expected = PhoneNumberAccess_Accessor.ConvertMultipleDbPhoneNumbersToLocalType(allNumbers);
            List<PhoneNumber> actual;
            actual = PhoneNumberAccess.GetPhoneNumberByType(searchedType);

            for (int i = 0; i < expected.Count; i++)
            {
                if ((expected[i].NumberType.Equals(searchedType)) && (!actual.Contains(expected[i])))
                {
                    Assert.Fail();
                }
                if ((!expected[i].NumberType.Equals(searchedType)) && (actual.Contains(expected[i])))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        ///A test for GetPhoneNumberByType
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByNonExistintTypeTest()
        {
            PhoneType searchedType = new PhoneType(50, "phonetype:50");
            List<PhoneNumber> actual = PhoneNumberAccess.GetPhoneNumberByType(searchedType);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetSpecificPhoneNumberTest()
        {
            string phoneNumber = "phone number:1";
            PhoneType wantedType = new PhoneType(1, "phonetype:1");
            PhoneNumber expected = new PhoneNumber(1, "phone number:1", new PhoneType(1, "phonetype:1"));
            PhoneNumber actual;
            actual = PhoneNumberAccess.GetSpecificPhoneNumber(phoneNumber, wantedType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetSpecificNonExistintPhoneNumberTest()
        {
            string phoneNumber = "phone number:50";
            PhoneType wantedType = new PhoneType(1, "phonetype:1");
            PhoneNumber actual = PhoneNumberAccess.GetSpecificPhoneNumber(phoneNumber, wantedType);
            Assert.IsNull(actual);
        }

        #endregion        

        #region Lookup Tests
        
        /// <summary>
        ///A test for LookupAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPhoneNumbersTest()
        {
            int personId = 1;
            List<t_phone_numbers> expected = (from pNumber in Cache.CacheData.t_phone_numbers
                                              select pNumber).ToList<t_phone_numbers>();
            List<t_phone_numbers> actual;
            actual = PhoneNumberAccess_Accessor.LookupAllPhoneNumbers(personId);

            for (int i = 0; i < expected.Count; i++)
            {
                if ((expected[i].person_id == personId) && (!actual.Contains(expected[i])))
                {
                    Assert.Fail();
                }
                if ((expected[i].person_id != personId) && (actual.Contains(expected[i])))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        ///A test for LookupAllPhoneNumbers
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPhoneNumbersOfNonExistintPersonTest()
        {
            int personId = 2;
            List<t_phone_numbers> actual;
            actual = PhoneNumberAccess_Accessor.LookupAllPhoneNumbers(personId);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for LookupPhoneNumberById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByIdTest()
        {
            int id = 1;
            t_phone_numbers expected = t_phone_numbers.Createt_phone_numbers(1,"phone number:1",1,1);
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.LookupPhoneNumberById(id);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.number, actual.number);
            Assert.AreEqual(expected.number_type, actual.number_type);
            Assert.AreEqual(expected.person_id, actual.person_id);
        }

        /// <summary>
        ///A test for LookupPhoneNumberById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByNonExistintIdTest()
        {
            int id = 50;
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.LookupPhoneNumberById(id);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for LookupPhoneNumberByType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByTypeTest()
        {
            PhoneType searchedType = new PhoneType(1, "phonetype:1");
            List<t_phone_numbers> expected = (from pNumber in Cache.CacheData.t_phone_numbers
                                              select pNumber).ToList<t_phone_numbers>();
            List<t_phone_numbers> actual;
            actual = PhoneNumberAccess_Accessor.LookupPhoneNumberByType(searchedType);

            for (int i = 0; i < expected.Count; i++)
            {
                if ((expected[i].number_type == searchedType._Id) && (!actual.Contains(expected[i])))
                {
                    Assert.Fail();
                }
                if ((expected[i].number_type != searchedType._Id) && (actual.Contains(expected[i])))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        ///A test for LookupPhoneNumberByType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByTypeTestNonExistint()
        {
            PhoneType searchedType = new PhoneType(50, "phonetype:50");
            List<t_phone_numbers> actual;
            actual = PhoneNumberAccess_Accessor.LookupPhoneNumberByType(searchedType);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for LookupSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificPhoneNumberTest()
        {
            string phoneNumber = "phone number:1";
            int numberType = 1;
            t_phone_numbers expected = t_phone_numbers.Createt_phone_numbers(1,"phone number:1", 1, 1);
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.LookupSpecificPhoneNumber(phoneNumber, numberType);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.number, actual.number);
            Assert.AreEqual(expected.number_type, actual.number_type);
            Assert.AreEqual(expected.person_id, actual.person_id);
        }

        /// <summary>
        ///A test for LookupSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificNonExistintPhoneNumberTest()
        {
            string phoneNumber = "phone number:50";
            int numberType = 1;
            t_phone_numbers actual = PhoneNumberAccess_Accessor.LookupSpecificPhoneNumber(phoneNumber, numberType);
            Assert.IsNull(actual);
        }
     
        #endregion        

        #region Update Tests
        
        /// <summary>
        ///A test for UpdateMultiplePhoneNumbers
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePhoneNumbersTest()
        {
            List<PhoneNumber> updatedPhoneNumberList = new List<PhoneNumber>()
            {
                new PhoneNumber(5, "updated phone number:5", new PhoneType(1, "phonetype:1")),
                new PhoneNumber(6, "updated phone number:6", new PhoneType(1, "phonetype:1"))
            };
            PhoneNumberAccess.UpdateMultiplePhoneNumbers(updatedPhoneNumberList, 1);

            List<PhoneNumber> actual = new List<PhoneNumber>()
            {
                PhoneNumberAccess.GetPhoneNumberById(5),
                PhoneNumberAccess.GetPhoneNumberById(6)
            };

            CollectionAssert.AreEqual(updatedPhoneNumberList, actual);
        }

        /// <summary>
        ///A test for UpdateSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePhoneNumberTest()
        {
            PhoneNumber updatedPhoneNumber = new PhoneNumber(7, "updated phone number:7", new PhoneType(1, "phonetype:1"));
            Enums.CRUDResults result = PhoneNumberAccess.UpdateSinglePhoneNumber(updatedPhoneNumber, 1);
            PhoneNumber actual = PhoneNumberAccess.GetPhoneNumberById(7);
            Assert.AreEqual(Enums.CRUDResults.UPDATE_SUCCESS, result);
            Assert.AreEqual(updatedPhoneNumber, actual);
        }

        /// <summary>
        ///A test for UpdateSinglePhoneNumber
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNonExistintPhoneNumberTest()
        {
            PhoneNumber updatedPhoneNumber = new PhoneNumber(50, "updated phone number:7", new PhoneType(1, "phonetype:1"));
            Enums.CRUDResults result = PhoneNumberAccess.UpdateSinglePhoneNumber(updatedPhoneNumber, 1);
            Assert.AreEqual(Enums.CRUDResults.UPDATE_FAIL, result);
        } 

        #endregion
    }
}
