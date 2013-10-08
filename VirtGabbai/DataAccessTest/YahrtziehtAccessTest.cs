using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using DataCache;
using System.Data.Objects;
using System.Linq;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for YarthzietAccessTest and is intended
    ///to contain all YarthzietAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class YahrtziehtAccessTest
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
        ///Adds a list of Yarhtziehts to the database
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewYartziehtTest()
        {
            if (!Cache.CacheData.t_people.Any(person => person.C_id == 1))
            {
                Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            }
            YahrtziehtAccess target = new YahrtziehtAccess();
            List<Yahrtzieht> myYaList = new List<Yahrtzieht>();

            for (int i = 1; i <= 10; i++)
            {
                Yahrtzieht ya = new Yahrtzieht();
                ya._Id = i;
                ya.Date = DateTime.Today;
                ya.Name = "Ploni ben Almoni";
                ya.Relation = "dogs previous owner";
                ya.PersonId = 1;
                myYaList.Add(ya);
            }
            target.AddMultipleNewYahrtzieht(myYaList);
        }

        /// <summary>
        ///Adds a single new Yarhtzieht to the database
        ///</summary>
        [TestMethod()]
        public void AddNewYartziehtTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            Yahrtzieht ya = new Yahrtzieht();
            ya._Id = 12;
            ya.Date = DateTime.Today;
            ya.Name = "ploni ben almoni";
            ya.Relation = "dogs previous owner";
            ya.PersonId = 1;

            target.AddNewYahrtzieht(ya); 
            
        } 

        #endregion

        #region Delete Tests

        /// <summary>
        ///Deletes a yarhtzieht that exists
        ///</summary>
        [TestMethod()]
        public void DeleteSingleYarhtziehtTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            Yahrtzieht ya = new Yahrtzieht(12, DateTime.Today, "rufos maximus", "the dogs friends cat", 1);
            target.DeleteSingleYahrtzieht(ya);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///Recieves all the yahrtziehts of the person with the given id
        ///</summary>
        [TestMethod()]
        public void GetAllYarthziehtsTest()
        {
            Yahrtzieht ya;
            List<Yahrtzieht> expected = new List<Yahrtzieht>();
            for (int i = 1; i <= 10; i++)
            {
                ya = new Yahrtzieht();
                ya._Id = i;
                ya.Date = DateTime.Today;
                ya.Name = "Ploni ben Almoni";
                ya.Relation = "dogs previous owner";
                ya.PersonId = 1;
                expected.Add(ya);
            }
            ya = new Yahrtzieht();
            ya._Id = 12;
            ya.Date = DateTime.Today;
            ya.Name = "Ploni ben Almoni";
            ya.Relation = "dogs previous owner";
            ya.PersonId = 1;
            expected.Add(ya);
            YahrtziehtAccess target = new YahrtziehtAccess();
            int personId = 1;
            
            List<Yahrtzieht> actual;
            actual = target.GetAllYahrtziehts(personId);
            Assert.AreEqual(expected.Count, actual.Count);

            for (int yahrIndex = 0; yahrIndex < actual.Count; yahrIndex++)
            {
                Assert.IsTrue(expected[yahrIndex].Equals(actual[yahrIndex]));
            }
        }

        /// <summary>
        ///A test for GetSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        public void GetSpecificYarthziehtTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;
            string personName = "Ploni ben Almoni";
            string relation = "dogs previous owner";
            Yahrtzieht expected = new Yahrtzieht(1, date, personName, relation, personId);

            YahrtziehtAccess target = new YahrtziehtAccess();
            Yahrtzieht actual = target.GetSpecificYahrtzieht(personId, date, personName);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        public void GetYarhtzietsByDateTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            int personId = 1;
            DateTime date = DateTime.Today;

            // ugly hack
            List<Yahrtzieht> expected = target.GetAllYahrtziehts(personId);
            List<Yahrtzieht> actual = target.GetYahrtziehtsByDate(personId, date);
            Assert.AreEqual(expected.Count, actual.Count);

            for (int yahrIndex = 0; yahrIndex < actual.Count; yahrIndex++)
            {
                Assert.IsTrue(expected[yahrIndex].Equals(actual[yahrIndex]));
            }
        }

        #endregion

        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllYarthziehts
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllYarthziehtsTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            int personId = 1;
            List<t_yahrtziehts> expected = 
                target.ConvertMultipleYahrtziehtsToDbType(target.GetAllYahrtziehts(personId));
            List<t_yahrtziehts> actual;
            actual = target.LookupAllYahrtziehts(personId);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        /// <summary>
        ///A test for LookupSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificYarthziehtTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;
            string personName = "Ploni ben Almoni";
            string relation = "dogs previous owner";
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            t_yahrtziehts expected = new t_yahrtziehts();
            expected.C_id = 1;
            expected.date = date;
            expected.deceaseds_name = personName;
            expected.person_id = personId;
            expected.relation = relation;
            t_yahrtziehts actual;
            actual = target.LookupSpecificYahrtzieht(personId, date, personName);
            Assert.IsTrue(target.ConverSingleYahrtziehtToLocalType(expected).
                Equals(target.ConverSingleYahrtziehtToLocalType(actual)));
        }

        /// <summary>
        ///A test for LookupYarhtzietById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietByIdTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;
            string personName = "Ploni ben Almoni";
            string relation = "dogs previous owner";
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            t_yahrtziehts expected = new t_yahrtziehts();
            expected.C_id = 1;
            expected.date = date;
            expected.deceaseds_name = personName;
            expected.person_id = personId;
            expected.relation = relation;
            int ID = 1;
            t_yahrtziehts actual;
            actual = target.LookupYahrtziehtById(ID);
            Assert.IsTrue(target.ConverSingleYahrtziehtToLocalType(expected).
                Equals(target.ConverSingleYahrtziehtToLocalType(actual)));
        }

        /// <summary>
        ///A test for LookupYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietsByDateTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            int personId = 1;
            DateTime date = DateTime.Today;
            List<t_yahrtziehts> expected = target.LookupAllYahrtziehts(personId);
            List<t_yahrtziehts> actual;
            actual = target.LookupYahrtziehtsByDate(personId, date);
            Assert.AreEqual(expected.Count, actual.Count);
        }

        #endregion

        #region UpdateTest

        /// <summary>
        ///A test for UpdateSingleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void UpdateSingleYarhtziehtTest()
        {
            string newName = "The second name";
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            t_yahrtziehts expected = target.LookupYahrtziehtById(10);
            expected.deceaseds_name = newName;
            target.UpdateSingleYahrtzieht(target.ConverSingleYahrtziehtToLocalType(expected));
            string actual = target.LookupYahrtziehtById(10).deceaseds_name;
            Assert.AreEqual(newName, actual);
        } 

        #endregion
    }
}
