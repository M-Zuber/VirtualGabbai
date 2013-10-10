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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            for (int newYahrIndex = 1; newYahrIndex <= 10; newYahrIndex++)
            {
                var newYahr = t_yahrtziehts.Createt_yahrtziehts(
                        newYahrIndex, 1, DateTime.Today, "passed on number:" + newYahrIndex.ToString());
                newYahr.relation = "best friends dog";
                Cache.CacheData.t_yahrtziehts.AddObject(newYahr);
            }
            Cache.CacheData.SaveChanges();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var test = (from ptype in Cache.CacheData.t_yahrtziehts select ptype).ToList<t_yahrtziehts>();
            var otherTest = (from peType in Cache.CacheData.t_people select peType).ToList<t_people>();
            for (int j = 0; j < otherTest.Count; j++)
            {
                Cache.CacheData.t_people.DeleteObject(otherTest[j]);                
            }
            for (int i = 0; i < test.Count; i++)
            {
                Cache.CacheData.t_yahrtziehts.DeleteObject(test[i]);
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
        ///Adds a list of Yarhtziehts to the database
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewYartziehtTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            List<Yahrtzieht> myYaList = new List<Yahrtzieht>();

            for (int i = 11; i <= 20; i++)
            {
                Yahrtzieht ya = new Yahrtzieht();
                ya._Id = i;
                ya.Date = DateTime.Today;
                ya.Name = "passed on number:" + i.ToString();
                ya.Relation = "best friends dog";
                ya.PersonId = 1;
                myYaList.Add(ya);
            }
            target.AddMultipleNewYahrtzieht(myYaList);

            List<Yahrtzieht> actual = new List<Yahrtzieht>();

            for (int actualIndex = 11; actualIndex <= 20; actualIndex++)
            {
                actual.Add(target.GetYahrtziehtById(actualIndex));
            }

            for (int assertIndex = 0; assertIndex < myYaList.Count; assertIndex++)
            {
                Assert.IsTrue(myYaList[assertIndex].Equals(actual[assertIndex]));                
            }
        }

        /// <summary>
        ///Adds a single new Yarhtzieht to the database
        ///</summary>
        [TestMethod()]
        public void AddNewYartziehtTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            Yahrtzieht ya = new Yahrtzieht();
            ya._Id = 21;
            ya.Date = DateTime.Today;
            ya.Name = "passed on number:" + ya._Id.ToString();
            ya.Relation = "best friends dog";
            ya.PersonId = 1;

            target.AddNewYahrtzieht(ya);
            Yahrtzieht actual = target.GetYahrtziehtById(21);
            Assert.IsTrue(ya.Equals(actual));
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
            Yahrtzieht ya = new Yahrtzieht(4, DateTime.Today, "passed on number:4", "best friends dog", 1);
            target.DeleteSingleYahrtzieht(ya);

            List<Yahrtzieht> allYahrtziehts = target.GetAllYahrtziehts(1);

            Assert.IsFalse(allYahrtziehts.Contains(ya));
        }

        /// <summary>
        ///A test for DeleteMultipleYahrtziehts
        ///</summary>
        [TestMethod()]
        public void DeleteMultipleYahrtziehtsTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            List<Yahrtzieht> deletedYahrList = new List<Yahrtzieht>()
            {
                new Yahrtzieht(2, DateTime.Today, "passed on number:2", "best friends dog", 1),
                new Yahrtzieht(3, DateTime.Today, "passed on number:3", "best friends dog", 1)
            };
            target.DeleteMultipleYahrtziehts(deletedYahrList);

            List<Yahrtzieht> allYahrtziehts = target.GetAllYahrtziehts(1);

            for (int i = 0; i < deletedYahrList.Count; i++)
            {
                Assert.IsFalse(allYahrtziehts.Contains(deletedYahrList[i]));
            }
        }

        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetYahrtziehtById
        ///</summary>
        [TestMethod()]
        public void GetYahrtziehtByIdTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            int yahrId = 1;
            Yahrtzieht expected = new Yahrtzieht(1, DateTime.Today, "passed on number:1", "best friends dog", 1);
            Yahrtzieht actual;
            actual = target.GetYahrtziehtById(yahrId);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///Recieves all the yahrtziehts of the person with the given id
        ///</summary>
        [TestMethod()]
        public void GetAllYarthziehtsTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();

            int personId = 1;
            
            List<Yahrtzieht> actual;
            actual = target.GetAllYahrtziehts(personId);

            Assert.IsInstanceOfType(actual, typeof(List<Yahrtzieht>));
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for GetSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        public void GetSpecificYarthziehtTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;
            string personName = "passed on number:1";
            string relation = "best friends dog";
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

            List<Yahrtzieht> expected = target.GetAllYahrtziehts(personId);
            List<Yahrtzieht> actual = target.GetYahrtziehtsByDate(personId, date);

            for (int yahrIndex = 0; yahrIndex < expected.Count; yahrIndex++)
            {
                Assert.IsTrue((expected[yahrIndex].Date == date) && (actual.Contains(expected[yahrIndex])));
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

            List<t_yahrtziehts> actual;
            actual = target.LookupAllYahrtziehts(personId);

            Assert.IsInstanceOfType(actual, typeof(List<t_yahrtziehts>));
            Assert.IsTrue(actual.Count > 0);
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
            string personName = "passed on number:1";
            string relation = "best friends dog";
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            t_yahrtziehts expected = new t_yahrtziehts();
            expected.C_id = 1;
            expected.date = date;
            expected.deceaseds_name = personName;
            expected.person_id = personId;
            expected.relation = relation;
            t_yahrtziehts actual;
            actual = target.LookupSpecificYahrtzieht(personId, date, personName);
            Assert.IsTrue(target.ConvertSingleYahrtziehtToLocalType(expected).
                Equals(target.ConvertSingleYahrtziehtToLocalType(actual)));
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
            string personName = "passed on number:1";
            string relation = "best friends dog";
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
            Assert.IsTrue(target.ConvertSingleYahrtziehtToLocalType(expected).
                Equals(target.ConvertSingleYahrtziehtToLocalType(actual)));
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

            for (int yahrIndex = 0; yahrIndex < expected.Count; yahrIndex++)
            {
                Assert.IsTrue((expected[yahrIndex].date == date) && (actual.Contains(expected[yahrIndex])));
            }
        }

        #endregion

        #region UpdateTest

        /// <summary>
        ///A test for UpdateSingleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void UpdateSingleYarhtziehtTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            Yahrtzieht updatedYahr = new Yahrtzieht(7, DateTime.Today, "updated passed on number:7", "best friends dog", 1);
            target.UpdateSingleYahrtzieht(updatedYahr);
            Yahrtzieht actual = target.GetYahrtziehtById(7);
            Assert.IsTrue(actual.Equals(updatedYahr));
        }

        /// <summary>
        ///A test for UpdateMultipleYahrtziehts
        ///</summary>
        [TestMethod()]
        public void UpdateMultipleYahrtziehtsTest()
        {
            YahrtziehtAccess target = new YahrtziehtAccess();
            List<Yahrtzieht> updatedYahrList = new List<Yahrtzieht>()
            {
                new Yahrtzieht(5, DateTime.Today, "updated passed on number:5", "best friends dog", 1),
                new Yahrtzieht(6, DateTime.Today, "updated passed on number:6", "best friends dog", 1)
            };

            target.UpdateMultipleYahrtziehts(updatedYahrList);

            List<Yahrtzieht> actual = new List<Yahrtzieht>()
            {
                target.GetYahrtziehtById(5),
                target.GetYahrtziehtById(6)
            };

            for (int i = 0; i < updatedYahrList.Count; i++)
            {
                Assert.IsTrue(updatedYahrList[i].Equals(actual[i]));
            }
        }

        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConvertMultipleYahrtziehtsToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleYahrtziehtsToLocalTypeTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            List<t_yahrtziehts> dbTypeYahrList = new List<t_yahrtziehts>();
            List<Yahrtzieht> expected = new List<Yahrtzieht>();

            for (int i = 0; i < 5; i++)
            {
                t_yahrtziehts toAdd = t_yahrtziehts.Createt_yahrtziehts(i, 1, DateTime.Today, "passed on number:1");
                toAdd.relation = "best friends dog";
                dbTypeYahrList.Add(toAdd);
                expected.Add(target.ConvertSingleYahrtziehtToLocalType(toAdd));
            }

            List<Yahrtzieht> actual;
            actual = target.ConvertMultipleYahrtziehtsToLocalType(dbTypeYahrList);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(expected[i].Equals(actual[i]));
            }
        }

        /// <summary>
        ///A test for ConvertMultipleYahrtziehtsToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleYahrtziehtsToDbTypeTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            List<Yahrtzieht> localTypeYahrList = new List<Yahrtzieht>();
            List<t_yahrtziehts> expected = new List<t_yahrtziehts>();
            for (int i = 0; i < 5; i++)
            {
                t_yahrtziehts toAdd = t_yahrtziehts.Createt_yahrtziehts(i, 1, DateTime.Today, "passed on number:1");
                toAdd.relation = "best friends dog";
                localTypeYahrList.Add(target.ConvertSingleYahrtziehtToLocalType(toAdd));
                expected.Add(toAdd);
            }
            List<t_yahrtziehts> actual;
            actual = target.ConvertMultipleYahrtziehtsToDbType(localTypeYahrList);
            List<Yahrtzieht> localActual = target.ConvertMultipleYahrtziehtsToLocalType(actual);
            List<Yahrtzieht> localExpected = target.ConvertMultipleYahrtziehtsToLocalType(expected);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue(localExpected[i].Equals(localActual[i]));
            }
        }

        /// <summary>
        ///A test for ConvertSingleYahrtziehtToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleYahrtziehtToDbTypeTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            Yahrtzieht localTypeYahr = new Yahrtzieht(1, DateTime.Today, "passed on number:1", "best friends dog", 1);
            t_yahrtziehts expected = t_yahrtziehts.Createt_yahrtziehts(1, 1, DateTime.Today, "passed on number:1");
            expected.relation = "best friends dog";
            t_yahrtziehts actual;
            actual = target.ConvertSingleYahrtziehtToDbType(localTypeYahr);
            Assert.IsTrue(target.ConvertSingleYahrtziehtToLocalType(expected).Equals(
                                            target.ConvertSingleYahrtziehtToLocalType(actual)));
        }

        /// <summary>
        ///A test for ConvertSingleYahrtziehtToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleYahrtziehtToLocalTypeTest()
        {
            YahrtziehtAccess_Accessor target = new YahrtziehtAccess_Accessor();
            t_yahrtziehts dbTypeYahr = t_yahrtziehts.Createt_yahrtziehts(1, 1, DateTime.Today, "passed on number:1");
            dbTypeYahr.relation = "best friends dog";
            Yahrtzieht expected = new Yahrtzieht(1, DateTime.Today, "passed on number:1", "best friends dog", 1);
            Yahrtzieht actual;
            actual = target.ConvertSingleYahrtziehtToLocalType(dbTypeYahr);
            Assert.IsTrue(expected.Equals(actual));
        } 

        #endregion

    }
}
