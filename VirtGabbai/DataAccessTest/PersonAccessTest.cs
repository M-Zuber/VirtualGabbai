using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LocalTypes;
using System.Collections.Generic;
using Framework;
using DataCache;
using System.Net.Mail;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for PersonAccessTest and is intended
    ///to contain all PersonAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PersonAccessTest
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
        ///A test for AddMultipleNewPersons
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewPersonsTest()
        {
            List<Person> newPersonList = null; // TODO: Initialize to an appropriate value
            PersonAccess.AddMultipleNewPersons(newPersonList);
        }

        /// <summary>
        ///A test for AddNewPerson
        ///</summary>
        [TestMethod()]
        public void AddNewPersonTest()
        {
            Person newPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.AddNewPerson(newPerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Convert Tests

        /// <summary>
        ///A test for ConvertMultipleDbPersonsToLocalType
        ///</summary>
        [TestMethod()]
        public void ConvertMultipleDbPersonsToLocalTypeTest()
        {
            List<t_people> dbTypePersonList = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.ConvertMultipleDbPersonsToLocalType(dbTypePersonList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleLocalPersonsToDbType
        ///</summary>
        [TestMethod()]
        public void ConvertMultipleLocalPersonsToDbTypeTest()
        {
            List<Person> localTypePersonList = null; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess.ConvertMultipleLocalPersonsToDbType(localTypePersonList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbPersonToLocalType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleDbPersonToLocalTypeTest()
        {
            t_people dbTypePerson = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.ConvertSingleDbPersonToLocalType(dbTypePerson);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalPersonToDbType
        ///</summary>
        [TestMethod()]
        public void ConvertSingleLocalPersonToDbTypeTest()
        {
            Person localTypePerson = null; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess.ConvertSingleLocalPersonToDbType(localTypePerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultiplePersons
        ///</summary>
        [TestMethod()]
        public void DeleteMultiplePersonsTest()
        {
            List<Person> deletedPersonList = null; // TODO: Initialize to an appropriate value
            PersonAccess.DeleteMultiplePersons(deletedPersonList);
        }

        /// <summary>
        ///A test for DeleteSinglePerson
        ///</summary>
        [TestMethod()]
        public void DeleteSinglePersonTest()
        {
            Person deletedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.DeleteSinglePerson(deletedPerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Get Tests

        /// <summary>
        ///A test for GetAllPeople
        ///</summary>
        [TestMethod()]
        public void GetAllPeopleTest()
        {
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetAllPeople();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByAccount
        ///</summary>
        [TestMethod()]
        public void GetByAccountTest()
        {
            Account accountSearchedBy = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByAccount(accountSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByAddress
        ///</summary>
        [TestMethod()]
        public void GetByAddressTest()
        {
            StreetAddress addressSearchedBy = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByAddress(addressSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByEmail
        ///</summary>
        [TestMethod()]
        public void GetByEmailTest()
        {
            MailAddress email = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByName
        ///</summary>
        [TestMethod()]
        public void GetByNameTest()
        {
            string fullName = string.Empty; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByName(fullName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPhoneNumber
        ///</summary>
        [TestMethod()]
        public void GetByPhoneNumberTest()
        {
            PhoneNumber numberSearchedBy = null; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = PersonAccess.GetByPhoneNumber(numberSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByYahrtzieht
        ///</summary>
        [TestMethod()]
        public void GetByYahrtziehtTest()
        {
            Yahrtzieht yahrtziehtSearchedBy = null; // TODO: Initialize to an appropriate value
            List<Person> expected = null; // TODO: Initialize to an appropriate value
            List<Person> actual;
            actual = PersonAccess.GetByYahrtzieht(yahrtziehtSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllPeople
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllPeopleTest()
        {
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupAllPeople();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAccount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByAccountTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByAccount(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAddress
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByAddressTest()
        {
            string address = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByAddress(address);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByEmail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByEmailTest()
        {
            string email = string.Empty; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByEmail(email);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNameTest()
        {
            string fullName = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByName(fullName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPhoneNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPhoneNumberTest()
        {
            string numberSearchedBy = string.Empty; // TODO: Initialize to an appropriate value
            t_people expected = null; // TODO: Initialize to an appropriate value
            t_people actual;
            actual = PersonAccess_Accessor.LookupByPhoneNumber(numberSearchedBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByYahrtzieht
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByYahrtziehtTest()
        {
            string nameOfDeceased = string.Empty; // TODO: Initialize to an appropriate value
            string relationToDeceased = string.Empty; // TODO: Initialize to an appropriate value
            List<t_people> expected = null; // TODO: Initialize to an appropriate value
            List<t_people> actual;
            actual = PersonAccess_Accessor.LookupByYahrtzieht(nameOfDeceased, relationToDeceased);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Update Tests

        /// <summary>
        ///A test for UpdateMultiplePersons
        ///</summary>
        [TestMethod()]
        public void UpdateMultiplePersonsTest()
        {
            List<Person> updatedPersonList = null; // TODO: Initialize to an appropriate value
            PersonAccess.UpdateMultiplePersons(updatedPersonList);
        }

        /// <summary>
        ///A test for UpdateSinglePerson
        ///</summary>
        [TestMethod()]
        public void UpdateSinglePersonTest()
        {
            Person updatedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.UpdateSinglePerson(updatedPerson);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertMultiplePersons
        ///</summary>
        [TestMethod()]
        public void UpsertMultiplePersonsTest()
        {
            List<Person> upsertedList = null; // TODO: Initialize to an appropriate value
            PersonAccess.UpsertMultiplePersons(upsertedList);
        }

        /// <summary>
        ///A test for UpsertSinglePerson
        ///</summary>
        [TestMethod()]
        public void UpsertSinglePersonTest()
        {
            Person upsertedPerson = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = PersonAccess.UpsertSinglePerson(upsertedPerson);
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
