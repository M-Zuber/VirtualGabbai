using DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataTypesTest
{
    
    
    /// <summary>
    ///This is a test class for YahrtziehtTest and is intended
    ///to contain all YahrtziehtTest Unit Tests
    ///</summary>
    [TestClass()]
    public class YahrtziehtTest
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

        #region Equals

        /// <summary>
        ///Everything is the same
        ///</summary>
        [TestMethod()]
        public void AllIsEqualEqualsTest()
        {
            Yahrtzieht target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            Yahrtzieht yahrComparing = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            bool expected = true;
            bool actual;
            actual = target.Equals(yahrComparing);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///The id is different
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            Yahrtzieht target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            Yahrtzieht yahrComparing = new Yahrtzieht(14, DateTime.Today, "rufus", "cats");
            bool expected = false;
            bool actual;
            actual = target.Equals(yahrComparing);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///The date is different
        ///</summary>
        [TestMethod()]
        public void DiffDateEqualsTest()
        {
            Yahrtzieht target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            Yahrtzieht yahrComparing = new Yahrtzieht(1, DateTime.MinValue, "rufus", "cats");
            bool expected = false;
            bool actual;
            actual = target.Equals(yahrComparing);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///The name is different
        ///</summary>
        [TestMethod()]
        public void DiffNameEqualsTest()
        {
            Yahrtzieht target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            Yahrtzieht yahrComparing = new Yahrtzieht(1, DateTime.Today, "fido", "cats");
            bool expected = false;
            bool actual;
            actual = target.Equals(yahrComparing);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///The relation is different
        ///</summary>
        [TestMethod()]
        public void DiffRelationEqualsTest()
        {
            Yahrtzieht target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            Yahrtzieht yahrComparing = new Yahrtzieht(1, DateTime.Today, "rufus", "dogs");
            bool expected = false;
            bool actual;
            actual = target.Equals(yahrComparing);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Everything is different
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            Yahrtzieht target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            Yahrtzieht yahrComparing = new Yahrtzieht(12, DateTime.MinValue, "fido", "dogs");
            bool expected = false;
            bool actual;
            actual = target.Equals(yahrComparing);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Multiple properties are different
        ///</summary>
        [TestMethod()]
        public void MultiDiffEqualsTest()
        {
            Yahrtzieht target = new Yahrtzieht(1, DateTime.Today, "rufus", "cats");
            Yahrtzieht yahrComparing = new Yahrtzieht(14, DateTime.Today, "fido", "cats");
            bool expected = false;
            bool actual;
            actual = target.Equals(yahrComparing);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ToString

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Yahrtzieht target = new Yahrtzieht(1,DateTime.Today,"Ploni ben Almoni", "Cats dog");
            string expected = "Deceased's Name:\"Ploni ben Almoni\" " +
                              "Date:\"" + DateTime.Today.Date.ToString("dd/MM/yyyy")+"\" " + 
                              "Relation:\"Cats dog\"";
            string actual= target.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
