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
    public class YarthzietAccessTest
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
            YarthzietAccess target = new YarthzietAccess();
            List<Yarthzieht> myYaList = new List<Yarthzieht>();

            for (int i = 1; i <= 10; i++)
            {
                Yarthzieht ya = new Yarthzieht();
                ya._Id = i;
                ya.Date = DateTime.Now;
                ya.Name = "ploni ben almoni";
                ya.Relation = "dogs previous owner";
                ya.PersonId = 1;
                myYaList.Add(ya);
            }
            target.AddMultipleNewYartzieht(myYaList);
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

            if (!Cache.CacheData.t_yarthziehts.Any(item => item.C_id == 1))
            {
                YarthzietAccess target = new YarthzietAccess();
                Yarthzieht ya = new Yarthzieht();
                ya._Id = 1;
                ya.Date = DateTime.Now;
                ya.Name = "ploni ben almoni";
                ya.Relation = "dogs previous owner";
                ya.PersonId = 1;

                target.AddNewYartzieht(ya); 
            }
            else
            {
                Assert.Fail("The entity already exists, please delete it from the db and run it again");
            }
        } 

        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultipleYarhtziet
        ///</summary>
        [TestMethod()]
        public void DeleteMultipleYarhtzietTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            List<Yarthzieht> myYaList = null; //  : Initialize to an appropriate value
            target.DeleteMultipleYarhtziet(myYaList);
        }

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

            YarthzietAccess target = new YarthzietAccess();
            Yarthzieht ya = new Yarthzieht(1, DateTime.Now, "the dogs friends cat", "rufos maximus", 1);
            target.AddNewYartzieht(ya);
            target.DeleteSingleYarhtzieht(ya);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllYarthziehts
        ///</summary>
        [TestMethod()]
        public void GetAllYarthziehtsTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            List<Yarthzieht> expected = null; //  : Initialize to an appropriate value
            List<Yarthzieht> actual;
            actual = target.GetAllYarthziehts(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        public void GetSpecificYarthziehtTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            string personName = string.Empty; //  : Initialize to an appropriate value
            Yarthzieht expected = null; //  : Initialize to an appropriate value
            Yarthzieht actual;
            actual = target.GetSpecificYarthzieht(personId, date, personName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        public void GetYarhtzietsByDateTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            List<Yarthzieht> expected = null; //  : Initialize to an appropriate value
            List<Yarthzieht> actual;
            actual = target.GetYarhtzietsByDate(personId, date);
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
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            List<t_yarthziehts> expected = null; //  : Initialize to an appropriate value
            List<t_yarthziehts> actual;
            actual = target.LookupAllYarthziehts(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificYarthziehtTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            string personName = string.Empty; //  : Initialize to an appropriate value
            t_yarthziehts expected = null; //  : Initialize to an appropriate value
            t_yarthziehts actual;
            actual = target.LookupSpecificYarthzieht(personId, date, personName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupYarhtzietById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietByIdTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            long ID = 0; //  : Initialize to an appropriate value
            t_yarthziehts expected = null; //  : Initialize to an appropriate value
            t_yarthziehts actual;
            actual = target.LookupYarhtzietById(ID);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietsByDateTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            DateTime date = new DateTime(); //  : Initialize to an appropriate value
            List<t_yarthziehts> expected = null; //  : Initialize to an appropriate value
            List<t_yarthziehts> actual;
            actual = target.LookupYarhtzietsByDate(personId, date);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region UpdateTest

        /// <summary>
        ///A test for UpdateMultipleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void UpdateMultipleYarhtziehtTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            List<Yarthzieht> myYaList = null; //  : Initialize to an appropriate value
            target.UpdateMultipleYarhtzieht(myYaList);
        }

        /// <summary>
        ///A test for UpdateSingleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void UpdateSingleYarhtziehtTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            Yarthzieht ya = null; //  : Initialize to an appropriate value
            target.UpdateSingleYarhtzieht(ya);
        } 

        #endregion
    }
}
