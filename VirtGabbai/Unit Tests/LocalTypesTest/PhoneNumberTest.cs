//using LocalTypes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace DataAccessTest
//{
    
    
//    /// <summary>
//    ///This is a test class for PhoneNumberTest and is intended
//    ///to contain all PhoneNumberTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class PhoneNumberTest
//    {

//        #region Test Data Members

//        //Target Data Members
//        PhoneNumber targetPhoneNumber = null;

//        #endregion

//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        // 
//        //You can use the following additional attributes as you write your tests:
//        //
//        //Use ClassInitialize to run code before running the first test in the class
//        //[ClassInitialize()]
//        //public static void MyClassInitialize(TestContext testContext)
//        //{
//        //}
//        //
//        //Use ClassCleanup to run code after all tests in a class have run
//        //[ClassCleanup()]
//        //public static void MyClassCleanup()
//        //{
//        //}
//        //
//        //Use TestInitialize to run code before running each test
//        [TestInitialize()]
//        public void MyTestInitialize()
//        {
//            targetPhoneNumber = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
//        }
//        //
//        //Use TestCleanup to run code after each test has run
//        [TestCleanup()]
//        public void MyTestCleanup()
//        {
//            targetPhoneNumber = null;
//        }
//        //
//        #endregion

//        #region Equals Test

//        /// <summary>
//        ///Comparing two phone numbers with no differences
//        ///</summary>
//        [TestMethod()]
//        public void PhoneNumber_Equals_NoDifferences()
//        {
//            PhoneNumber comparedNumber = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
//            Assert.IsTrue(targetPhoneNumber.Equals(comparedNumber));
//        }

//        /// <summary>
//        ///Comparing two phone numbers with a difference in every field
//        ///</summary>
//        [TestMethod()]
//        public void PhoneNumber_Equals_DifferenceInEveryField()
//        {
//            PhoneNumber comparedNumber = new PhoneNumber(2, "0546147485", new PhoneType(2, "house phone"));
//            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
//        }

//        /// <summary>
//        ///Comparing two phone numbers with a difference in the id
//        ///</summary>
//        [TestMethod()]
//        public void PhoneNumber_Equals_DifferenceInId()
//        {
//            PhoneNumber comparedNumber = new PhoneNumber(2, "0546137475", new PhoneType(1, "cell phone"));
//            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
//        }

//        /// <summary>
//        ///Comparing two phone numbers with a difference in the numnber
//        ///</summary>
//        [TestMethod()]
//        public void PhoneNumber_Equals_DifferenceInNumber()
//        {
//            PhoneNumber comparedNumber = new PhoneNumber(1, "0546147485", new PhoneType(1, "cell phone"));
//            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
//        }

//        /// <summary>
//        ///Comparing two phone numbers with a difference in the type
//        ///</summary>
//        [TestMethod()]
//        public void PhoneNumber_Equals_DifferenceInType()
//        {
//            PhoneNumber comparedNumber = new PhoneNumber(1, "0546137475", new PhoneType(2, "house phone"));
//            Assert.IsFalse(targetPhoneNumber.Equals(comparedNumber));
//        }

//        #endregion

//        #region ToStringTest

//        /// <summary>
//        ///PhoneNumber.ToString() test
//        ///</summary>
//        [TestMethod()]
//        public void PhoneNUmber_ToString()
//        {
//            string expected = "Number:\"" + targetPhoneNumber.Number + "\" " +
//                              targetPhoneNumber.NumberType.ToString();
//            string actual = targetPhoneNumber.ToString();
//            Assert.AreEqual(expected, actual);
//        }
        
//        #endregion
//    }
//}
