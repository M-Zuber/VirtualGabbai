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

        #region Add Tests -Passed#1

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
                ya.Date = DateTime.Now;
                ya.Name = "ploni ben almoni";
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
            if (!Cache.CacheData.t_people.Any(person => person.C_id == 1))
            {
                Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            }

            if (!Cache.CacheData.t_yahrtziehts.Any(item => item.C_id == 1))
            {
                YahrtziehtAccess target = new YahrtziehtAccess();
                Yahrtzieht ya = new Yahrtzieht();
                ya._Id = 1;
                ya.Date = DateTime.Now;
                ya.Name = "ploni ben almoni";
                ya.Relation = "dogs previous owner";
                ya.PersonId = 1;

                target.AddNewYahrtzieht(ya); 
            }
            else
            {
                Assert.Fail("The entity already exists, please delete it from the db and run it again");
            }
        } 

        #endregion

        #region Delete Tests

        /// <summary>
        ///Deletes a yarhtzieht that exists
        ///</summary>
        [TestMethod()]
        public void DeleteSingleYarhtziehtTest()
        {
            if (!Cache.CacheData.t_people.Any(person => person.C_id == 1))
            {
                Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            }

            YahrtziehtAccess target = new YahrtziehtAccess();
            Yahrtzieht ya = new Yahrtzieht(1, DateTime.Now, "the dogs friends cat", "rufos maximus", 1);
            target.AddNewYahrtzieht(ya);
            target.DeleteSingleYahrtzieht(ya);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllYarthziehts
        ///</summary>
        [TestMethod()]
        public void GetAllYarthziehtsTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            List<Yahrtzieht> expected = null; //  : Initialize to an appropriate value
            List<Yahrtzieht> actual;
            actual = target.GetAllYahrtziehts(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        public void GetSpecificYarthziehtTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            string personName = string.Empty; //  : Initialize to an appropriate value
            Yahrtzieht expected = null; //  : Initialize to an appropriate value
            Yahrtzieht actual;
            actual = target.GetSpecificYahrtzieht(personId, date, personName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        public void GetYarhtzietsByDateTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            List<Yahrtzieht> expected = null; //  : Initialize to an appropriate value
            List<Yahrtzieht> actual;
            actual = target.GetYahrtziehtsByDate(personId, date);
            Assert.AreEqual(expected, actual);
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
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            List<t_yahrtziehts> expected = null; //  : Initialize to an appropriate value
            List<t_yahrtziehts> actual;
            actual = target.LookupAllYahrtziehts(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificYarthziehtTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            string personName = string.Empty; //  : Initialize to an appropriate value
            t_yahrtziehts expected = null; //  : Initialize to an appropriate value
            t_yahrtziehts actual;
            actual = target.LookupSpecificYahrtzieht(personId, date, personName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupYarhtzietById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietByIdTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor(); //  : Initialize to an appropriate value
            long ID = 0; //  : Initialize to an appropriate value
            t_yahrtziehts expected = null; //  : Initialize to an appropriate value
            t_yahrtziehts actual;
            actual = target.LookupYahrtziehtById(ID);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietsByDateTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            List<t_yahrtziehts> expected = null; //  : Initialize to an appropriate value
            List<t_yahrtziehts> actual;
            actual = target.LookupYahrtziehtsByDate(personId, date);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region UpdateTest

        /// <summary>
        ///A test for UpdateSingleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void UpdateSingleYarhtziehtTest()
        {
            if (!Cache.CacheData.t_people.Any(person => person.C_id == 1))
            {
                Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            }

            YahrtziehtAccess target = new YahrtziehtAccess();
            Yahrtzieht ya = new Yahrtzieht(27, DateTime.Now, "the dogs friends cat", "rufos maximus", 1);
            target.AddNewYahrtzieht(ya);
            ya.Name = "The second name";
            target.UpdateSingleYahrtzieht(ya);
        } 

        #endregion
    }
}
