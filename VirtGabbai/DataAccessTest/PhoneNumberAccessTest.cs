using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using DataCache;
using System.Linq;

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
            Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            Cache.CacheData.t_phone_types.AddObject(t_phone_types.Createt_phone_types(1, "cell phone"));
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
            actual = PhoneNumberAccess_Accessor.ConvertMultipleLocalPhoneNumbersToDbType(localTypePhoneNumberList);
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
            PhoneNumber expected = new PhoneNumber(1, "phone number:1", new PhoneType(1, "cell phone"));
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
            PhoneNumber localTypePhoneNumber = new PhoneNumber(1, "phone number:1", new PhoneType(1, "cell phone"));
            t_phone_numbers expected = t_phone_numbers.Createt_phone_numbers(1, "phone number:1", 1, 1);
            t_phone_numbers actual;
            actual = PhoneNumberAccess_Accessor.ConvertSingleLocalPhoneNumberToDbType(localTypePhoneNumber);
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
            List<t_phone_numbers> dbActual = PhoneNumberAccess_Accessor.ConvertMultipleLocalPhoneNumbersToDbType(actual);
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
        ///A test for GetPhoneNumberById
        ///</summary>
        [TestMethod()]
        public void GetPhoneNumberByIdTest()
        {
            int id = 1;
            PhoneNumber expected = new PhoneNumber(1, "phone number:1", new PhoneType(1, "cell phone"));
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
            PhoneType searchedType = new PhoneType(1, "cell phone");
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
        ///A test for GetSpecificPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetSpecificPhoneNumberTest()
        {
            string phoneNumber = "phone number:1";
            PhoneType wantedType = new PhoneType(1, "cell phone");
            PhoneNumber expected = new PhoneNumber(1, "phone number:1", new PhoneType(1, "cell phone"));
            PhoneNumber actual;
            actual = PhoneNumberAccess.GetSpecificPhoneNumber(phoneNumber, wantedType);
            Assert.AreEqual(expected, actual);
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
        ///A test for LookupPhoneNumberByType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupPhoneNumberByTypeTest()
        {
            PhoneType searchedType = new PhoneType(1, "cell phone");
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
     
        #endregion        

        #region Update Tests
        
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

        #endregion
    }
}
