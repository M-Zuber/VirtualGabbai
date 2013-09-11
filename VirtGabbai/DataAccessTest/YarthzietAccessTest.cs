using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using DataCache;

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

        #region Add Tests

        /// <summary>
        ///A test for AddMultipleNewYartzieht
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewYartziehtTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            List<Yarthzieht> myYaList = null; //  : Initialize to an appropriate value
            target.AddMultipleNewYartzieht(myYaList);
        }

        /// <summary>
        ///A test for AddNewYartzieht
        ///</summary>
        [TestMethod()]
        public void AddNewYartziehtTest()
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

        #endregion

        #region Convert Tests

        /// <summary>
        ///A test for ConverSingleYarhtzietToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConverSingleYarhtzietToLocalTypeTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            t_yarthziehts ya = null; //  : Initialize to an appropriate value
            Yarthzieht expected = null; //  : Initialize to an appropriate value
            Yarthzieht actual;
            actual = target.ConverSingleYarhtzietToLocalType(ya);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleYarhtzietToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleYarhtzietToDbTypeTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            List<Yarthzieht> myYaList = null; //  : Initialize to an appropriate value
            List<t_yarthziehts> expected = null; //  : Initialize to an appropriate value
            List<t_yarthziehts> actual;
            actual = target.ConvertMultipleYarhtzietToDbType(myYaList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleYarhtzietToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleYarhtzietToLocalTypeTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            List<t_yarthziehts> ya = null; //  : Initialize to an appropriate value
            List<Yarthzieht> expected = null; //  : Initialize to an appropriate value
            List<Yarthzieht> actual;
            actual = target.ConvertMultipleYarhtzietToLocalType(ya);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleYarhtzietToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleYarhtzietToDbTypeTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            Yarthzieht ya = null; //  : Initialize to an appropriate value
            t_yarthziehts expected = null; //  : Initialize to an appropriate value
            t_yarthziehts actual;
            actual = target.ConvertSingleYarhtzietToDbType(ya);
            Assert.AreEqual(expected, actual);
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
        ///A test for DeleteSingleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void DeleteSingleYarhtziehtTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            Yarthzieht ya = null; //  : Initialize to an appropriate value
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

        /// <summary>
        ///A test for GetYarhtzietsByName
        ///</summary>
        [TestMethod()]
        public void GetYarhtzietsByNameTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            string personName = string.Empty; //  : Initialize to an appropriate value
            List<Yarthzieht> expected = null; //  : Initialize to an appropriate value
            List<Yarthzieht> actual;
            actual = target.GetYarhtzietsByName(personId, personName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetYarhtzietsByRelation
        ///</summary>
        [TestMethod()]
        public void GetYarhtzietsByRelationTest()
        {
            YarthzietAccess target = new YarthzietAccess(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            string relation = string.Empty; //  : Initialize to an appropriate value
            List<Yarthzieht> expected = null; //  : Initialize to an appropriate value
            List<Yarthzieht> actual;
            actual = target.GetYarhtzietsByRelation(personId, relation);
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

        /// <summary>
        ///A test for LookupYarhtzietsByName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietsByNameTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            string personName = string.Empty; //  : Initialize to an appropriate value
            List<t_yarthziehts> expected = null; //  : Initialize to an appropriate value
            List<t_yarthziehts> actual;
            actual = target.LookupYarhtzietsByName(personId, personName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupYarhtzietsByRelation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietsByRelationTest()
        {
            YarthzietAccess_Accessor target = new YarthzietAccess_Accessor(); //  : Initialize to an appropriate value
            long personId = 0; //  : Initialize to an appropriate value
            string relation = string.Empty; //  : Initialize to an appropriate value
            List<t_yarthziehts> expected = null; //  : Initialize to an appropriate value
            List<t_yarthziehts> actual;
            actual = target.LookupYarhtzietsByRelation(personId, relation);
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
