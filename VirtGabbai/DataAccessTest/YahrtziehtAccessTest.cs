using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LocalTypes;
using System.Collections.Generic;
using DataCache;
using System.Data.Objects;
using System.Linq;
using Framework;

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
            if (!Cache.CacheData.t_people.Any(person => person.C_id == 1))
            {
                var newPerson = t_people.Createt_people(1);
                newPerson.address = "12;" + 1 + 1 + ";main st;anywhere;anystate;usa;12345";
                newPerson.email = 1 + "@something.somewhere";
                newPerson.family_name = "Doe";
                newPerson.given_name = "Jack/Jane";
                Cache.CacheData.t_people.AddObject(newPerson);
            }

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
            List<Yahrtzieht> myYaList = new List<Yahrtzieht>();

            for (int i = 11; i <= 20; i++)
            {
                Yahrtzieht ya = new Yahrtzieht();
                ya._Id = i;
                ya.Date = DateTime.Today;
                ya.Name = "passed on number:" + i.ToString();
                ya.Relation = "best friends dog";
                myYaList.Add(ya);
            }
            YahrtziehtAccess.AddMultipleNewYahrtzieht(myYaList, 1);

            List<Yahrtzieht> actual = new List<Yahrtzieht>();

            for (int actualIndex = 11; actualIndex <= 20; actualIndex++)
            {
                actual.Add(YahrtziehtAccess.GetYahrtziehtById(actualIndex));
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
            Yahrtzieht ya = new Yahrtzieht();
            ya._Id = 21;
            ya.Date = DateTime.Today;
            ya.Name = "passed on number:" + ya._Id.ToString();
            ya.Relation = "best friends dog";

            Enums.CRUDResults result = YahrtziehtAccess.AddNewYahrtzieht(ya, 1);
            Yahrtzieht actual = YahrtziehtAccess.GetYahrtziehtById(21);
            Assert.AreEqual(Enums.CRUDResults.CREATE_SUCCESS, result);
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
            Yahrtzieht ya = new Yahrtzieht(4, DateTime.Today, "passed on number:4", "best friends dog");
            Enums.CRUDResults result = YahrtziehtAccess.DeleteSingleYahrtzieht(ya);

            List<Yahrtzieht> allYahrtziehts = YahrtziehtAccess.GetAllYahrtziehts(1);
            Assert.AreEqual(Enums.CRUDResults.DELETE_SUCCESS, result);
            Assert.IsFalse(allYahrtziehts.Contains(ya));
        }

        /// <summary>
        ///Deletes a yarhtzieht that does not exist
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExsistentYarhtziehtTest()
        {
            Yahrtzieht ya = new Yahrtzieht(50, DateTime.Today, "passed on number:4", "best friends dog");
            Enums.CRUDResults actual = YahrtziehtAccess.DeleteSingleYahrtzieht(ya);
            Assert.AreEqual(Enums.CRUDResults.DELETE_FAIL, actual);
        }

        /// <summary>
        ///A test for DeleteMultipleYahrtziehts
        ///</summary>
        [TestMethod()]
        public void DeleteMultipleYahrtziehtsTest()
        {
            List<Yahrtzieht> deletedYahrList = new List<Yahrtzieht>()
            {
                new Yahrtzieht(2, DateTime.Today, "passed on number:2", "best friends dog"),
                new Yahrtzieht(3, DateTime.Today, "passed on number:3", "best friends dog")
            };
            YahrtziehtAccess.DeleteMultipleYahrtziehts(deletedYahrList);

            List<Yahrtzieht> allYahrtziehts = YahrtziehtAccess.GetAllYahrtziehts(1);

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
            int yahrId = 1;
            Yahrtzieht expected = new Yahrtzieht(1, DateTime.Today, "passed on number:1", "best friends dog");
            Yahrtzieht actual;
            actual = YahrtziehtAccess.GetYahrtziehtById(yahrId);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetYahrtziehtById
        ///</summary>
        [TestMethod()]
        public void GetYahrtziehtByNonExistintIdTest()
        {
            int yahrId = 50;
            Yahrtzieht actual;
            actual = YahrtziehtAccess.GetYahrtziehtById(yahrId);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///Recieves all the yahrtziehts of the person with the given id
        ///</summary>
        [TestMethod()]
        public void GetAllYarthziehtsTest()
        {
            int personId = 1;
            
            List<Yahrtzieht> actual;
            actual = YahrtziehtAccess.GetAllYahrtziehts(personId);

            Assert.IsInstanceOfType(actual, typeof(List<Yahrtzieht>));
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///Recieves all the yahrtziehts of the person with the given id
        ///</summary>
        [TestMethod()]
        public void GetAllYarthziehtsOfNonExistintPersonTest()
        {
            int personId = 450;
            List<Yahrtzieht> actual;
            actual = YahrtziehtAccess.GetAllYahrtziehts(personId);
            Assert.IsNull(actual);
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
            Yahrtzieht expected = new Yahrtzieht(1, date, personName, relation);

            Yahrtzieht actual = YahrtziehtAccess.GetSpecificYahrtzieht(personId, date, personName);
            Assert.IsTrue(expected.Equals(actual));
        }

        /// <summary>
        ///A test for GetSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        public void GetSpecificNonExstintYarthziehtTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;
            string personName = "passed on number:50";

            Yahrtzieht actual = YahrtziehtAccess.GetSpecificYahrtzieht(personId, date, personName);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        public void GetYarhtzietsByDateTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;

            List<Yahrtzieht> expected = YahrtziehtAccess.GetAllYahrtziehts(personId);
            List<Yahrtzieht> actual = YahrtziehtAccess.GetYahrtziehtsByDate(personId, date);

            for (int yahrIndex = 0; yahrIndex < expected.Count; yahrIndex++)
            {
                Assert.IsTrue((expected[yahrIndex].Date == date) && (actual.Contains(expected[yahrIndex])));
            }
        }

        /// <summary>
        ///A test for GetYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        public void GetYarhtzietsByDateTestNotExist()
        {
            int personId = 1;
            DateTime date = DateTime.MinValue;
            List<Yahrtzieht> actual = YahrtziehtAccess.GetYahrtziehtsByDate(personId, date);
            Assert.AreEqual(0, actual.Count);
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
            int personId = 1;

            List<t_yahrtziehts> actual;
            actual = YahrtziehtAccess_Accessor.LookupAllYahrtziehts(personId);

            Assert.IsInstanceOfType(actual, typeof(List<t_yahrtziehts>));
            Assert.IsTrue(actual.Count > 0);

            List<t_yahrtziehts> expected = (from yahr in Cache.CacheData.t_yahrtziehts
                                            where yahr.t_people.C_id == personId
                                            select yahr).ToList<t_yahrtziehts>();

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.IsTrue((expected[i].person_id == personId) && (actual.Contains(expected[i])));
            }
        }
        /// <summary>
        ///A test for LookupAllYarthziehts
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllYarthziehtsOfNonExistintPersonTest()
        {
            int personId = 450;

            List<t_yahrtziehts> actual;
            actual = YahrtziehtAccess_Accessor.LookupAllYahrtziehts(personId);
            Assert.IsNull(actual);
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
            t_yahrtziehts expected = new t_yahrtziehts();
            expected.C_id = 1;
            expected.date = date;
            expected.deceaseds_name = personName;
            expected.person_id = personId;
            expected.relation = relation;
            t_yahrtziehts actual;
            actual = YahrtziehtAccess_Accessor.LookupSpecificYahrtzieht(personId, date, personName);
            Assert.IsTrue(YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(expected).
                Equals(YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(actual)));
        }

        /// <summary>
        ///A test for LookupSpecificYarthzieht
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificNonExistintYarthziehtTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;
            string personName = "passed on number:50";
            t_yahrtziehts actual;
            actual = YahrtziehtAccess_Accessor.LookupSpecificYahrtzieht(personId, date, personName);
            Assert.IsNull(actual);
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
            t_yahrtziehts expected = new t_yahrtziehts();
            expected.C_id = 1;
            expected.date = date;
            expected.deceaseds_name = personName;
            expected.person_id = personId;
            expected.relation = relation;
            int ID = 1;
            t_yahrtziehts actual;
            actual = YahrtziehtAccess_Accessor.LookupYahrtziehtById(ID);
            Assert.IsTrue(YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(expected).
                Equals(YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(actual)));
        }

        /// <summary>
        ///A test for LookupYarhtzietById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietByNonExistintIdTest()
        {
            int ID = 50;
            t_yahrtziehts actual;
            actual = YahrtziehtAccess_Accessor.LookupYahrtziehtById(ID);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for LookupYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietsByDateTest()
        {
            int personId = 1;
            DateTime date = DateTime.Today;
            List<t_yahrtziehts> expected = YahrtziehtAccess_Accessor.LookupAllYahrtziehts(personId);
            List<t_yahrtziehts> actual;
            actual = YahrtziehtAccess_Accessor.LookupYahrtziehtsByDate(personId, date);

            for (int yahrIndex = 0; yahrIndex < expected.Count; yahrIndex++)
            {
                Assert.IsTrue((expected[yahrIndex].date == date) && (actual.Contains(expected[yahrIndex])));
            }
        }

        /// <summary>
        ///A test for LookupYarhtzietsByDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupYarhtzietsByNonExistentDateTest()
        {
            int personId = 1;
            DateTime date = DateTime.MinValue;
            List<t_yahrtziehts> actual;
            actual = YahrtziehtAccess_Accessor.LookupYahrtziehtsByDate(personId, date);
            Assert.AreEqual(0, actual.Count);
        }

        #endregion

        #region UpdateTest

        /// <summary>
        ///A test for UpdateSingleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void UpdateSingleYarhtziehtTest()
        {
            Yahrtzieht updatedYahr = new Yahrtzieht(7, DateTime.Today, "updated passed on number:7", "best friends dog");
            Enums.CRUDResults result = YahrtziehtAccess.UpdateSingleYahrtzieht(updatedYahr, 1);
            Yahrtzieht actual = YahrtziehtAccess.GetYahrtziehtById(7);
            Assert.AreEqual(Enums.CRUDResults.UPDATE_SUCCESS, result);
            Assert.IsTrue(actual.Equals(updatedYahr));
        }

        /// <summary>
        ///A test for UpdateSingleYarhtzieht
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNonExistentYarhtziehtTest()
        {
            Yahrtzieht updatedYahr = new Yahrtzieht(50, DateTime.Today, "updated passed on number:7", "best friends dog");
            Enums.CRUDResults result = YahrtziehtAccess.UpdateSingleYahrtzieht(updatedYahr, 1);
            Yahrtzieht actual = YahrtziehtAccess.GetYahrtziehtById(7);
            Assert.AreEqual(Enums.CRUDResults.UPDATE_FAIL, result);
        }

        /// <summary>
        ///A test for UpdateMultipleYahrtziehts
        ///</summary>
        [TestMethod()]
        public void UpdateMultipleYahrtziehtsTest()
        {
            List<Yahrtzieht> updatedYahrList = new List<Yahrtzieht>()
            {
                new Yahrtzieht(5, DateTime.Today, "updated passed on number:5", "best friends dog"),
                new Yahrtzieht(6, DateTime.Today, "updated passed on number:6", "best friends dog")
            };

            YahrtziehtAccess.UpdateMultipleYahrtziehts(updatedYahrList, 1);

            List<Yahrtzieht> actual = new List<Yahrtzieht>()
            {
                YahrtziehtAccess.GetYahrtziehtById(5),
                YahrtziehtAccess.GetYahrtziehtById(6)
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
            List<t_yahrtziehts> dbTypeYahrList = new List<t_yahrtziehts>();
            List<Yahrtzieht> expected = new List<Yahrtzieht>();

            for (int i = 0; i < 5; i++)
            {
                t_yahrtziehts toAdd = t_yahrtziehts.Createt_yahrtziehts(i, 1, DateTime.Today, "passed on number:1");
                toAdd.relation = "best friends dog";
                dbTypeYahrList.Add(toAdd);
                expected.Add(YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(toAdd));
            }

            List<Yahrtzieht> actual;
            actual = YahrtziehtAccess_Accessor.ConvertMultipleYahrtziehtsToLocalType(dbTypeYahrList);
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
            List<Yahrtzieht> localTypeYahrList = new List<Yahrtzieht>();
            List<t_yahrtziehts> expected = new List<t_yahrtziehts>();
            for (int i = 0; i < 5; i++)
            {
                t_yahrtziehts toAdd = t_yahrtziehts.Createt_yahrtziehts(i, 1, DateTime.Today, "passed on number:1");
                toAdd.relation = "best friends dog";
                localTypeYahrList.Add(YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(toAdd));
                expected.Add(toAdd);
            }
            List<t_yahrtziehts> actual;
            actual = YahrtziehtAccess_Accessor.ConvertMultipleYahrtziehtsToDbType(localTypeYahrList, 1);
            List<Yahrtzieht> localActual = YahrtziehtAccess_Accessor.ConvertMultipleYahrtziehtsToLocalType(actual);
            List<Yahrtzieht> localExpected = YahrtziehtAccess_Accessor.ConvertMultipleYahrtziehtsToLocalType(expected);
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
            Yahrtzieht localTypeYahr = new Yahrtzieht(1, DateTime.Today, "passed on number:1", "best friends dog");
            t_yahrtziehts expected = t_yahrtziehts.Createt_yahrtziehts(1, 1, DateTime.Today, "passed on number:1");
            expected.relation = "best friends dog";
            t_yahrtziehts actual;
            actual = YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToDbType(localTypeYahr, 1);
            Assert.IsTrue(YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(expected).Equals(
                                            YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(actual)));
        }

        /// <summary>
        ///A test for ConvertSingleYahrtziehtToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleYahrtziehtToLocalTypeTest()
        {
            t_yahrtziehts dbTypeYahr = t_yahrtziehts.Createt_yahrtziehts(1, 1, DateTime.Today, "passed on number:1");
            dbTypeYahr.relation = "best friends dog";
            Yahrtzieht expected = new Yahrtzieht(1, DateTime.Today, "passed on number:1", "best friends dog");
            Yahrtzieht actual;
            actual = YahrtziehtAccess_Accessor.ConvertSingleYahrtziehtToLocalType(dbTypeYahr);
            Assert.IsTrue(expected.Equals(actual));
        } 

        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertSingleYahrtzieht
        ///</summary>
        [TestMethod()]
        public void UpsertAddSingleYahrtziehtTest()
        {
            Yahrtzieht upsertedYahrtzieht = new Yahrtzieht(613, DateTime.Today, "not my name", "i wont admit it");
            int personId = 1;
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = YahrtziehtAccess.UpsertSingleYahrtzieht(upsertedYahrtzieht, personId);
            Assert.AreEqual(expected, actual);
            List<Yahrtzieht> afterUpsert = YahrtziehtAccess.GetAllYahrtziehts(1);
            Assert.IsTrue(afterUpsert.Contains(upsertedYahrtzieht));
        }

        /// <summary>
        ///A test for UpsertSingleYahrtzieht
        ///</summary>
        [TestMethod()]
        public void UpsertUpdateSingleYahrtziehtTest()
        {
            Yahrtzieht upsertedYahrtzieht = new Yahrtzieht(8, DateTime.Today, "the other name", "is there really one?!!");
            int personId = 1;
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = YahrtziehtAccess.UpsertSingleYahrtzieht(upsertedYahrtzieht, personId);
            Assert.AreEqual(expected, actual);
            Yahrtzieht afterUpsert = YahrtziehtAccess.GetYahrtziehtById(8);
            Assert.AreEqual(upsertedYahrtzieht, afterUpsert);
        }
        
        #endregion
    }
}
