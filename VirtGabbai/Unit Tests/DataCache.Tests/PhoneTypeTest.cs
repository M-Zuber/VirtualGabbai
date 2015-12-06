//using LocalTypes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace LocalTypesTest
//{


//    /// <summary>
//    ///This is a test class for PhoneTypeTest and is intended
//    ///to contain all PhoneTypeTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class PhoneTypeTest
//    {
//        #region Test Data Members

//        //Target Data Members
//        PhoneType targetPhoneType = null;

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
//            targetPhoneType = new PhoneType(1, "one");
//        }
//        //
//        //Use TestCleanup to run code after each test has run
//        [TestCleanup()]
//        public void MyTestCleanup()
//        {
//            targetPhoneType = null;
//        }
//        //
//        #endregion

//        #region Equals

//        /// <summary>
//        ///Comparing two phone types with no differences
//        ///</summary>
//        [TestMethod()]
//        public void PhoneType_Equals_NoDifferences()
//        {
//            PhoneType otherPhoneType =
//                new PhoneType(targetPhoneType._Id, targetPhoneType.PhoneTypeName);
//            Assert.IsTrue(targetPhoneType.Equals(otherPhoneType));
//        }

//        /// <summary>
//        ///Comparing two phone types with a difference in the id
//        ///</summary>
//        [TestMethod()]
//        public void PhoneType_Equals_DifferenceInId()
//        {
//            PhoneType otherPhoneType =
//                new PhoneType((targetPhoneType._Id * 2), targetPhoneType.PhoneTypeName);
//            Assert.IsFalse(targetPhoneType.Equals(otherPhoneType));
//        }

//        /// <summary>
//        ///Comparing two phone types with a difference in the type
//        ///</summary>
//        [TestMethod()]
//        public void PhoneType_Equals_DifferenceInType()
//        {
//            PhoneType otherPhoneType =
//                new PhoneType(targetPhoneType._Id,
//                    targetPhoneType.PhoneTypeName + targetPhoneType.PhoneTypeName);
//            Assert.IsFalse(targetPhoneType.Equals(otherPhoneType));
//        }

//        /// <summary>
//        ///Comparing two phone types with a difference in every field
//        ///</summary>
//        [TestMethod()]
//        public void PhoneType_Equals_DifferenceInEveryField()
//        {
//            PhoneType otherPhoneType =
//                new PhoneType((targetPhoneType._Id * 2),
//                    targetPhoneType.PhoneTypeName + targetPhoneType.PhoneTypeName);
//            Assert.IsFalse(targetPhoneType.Equals(otherPhoneType));
//        }

//        #endregion

//        #region ToString

//        /// <summary>
//        ///PhoneType,ToString() Test
//        ///</summary>
//        [TestMethod()]
//        public void PhoneType_ToString()
//        {
//            string expected = "Type:\"one\"";
//            string actual = targetPhoneType.ToString();
//            Assert.AreEqual(expected, actual);
//        }

//        #endregion
//    }
//}
