using DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataTypesTest
{
    
    
    /// <summary>
    ///This is a test class for DonationTest and is intended
    ///to contain all DonationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DonationTest
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

        #region ToString Test

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void AllFieldsSetToStringTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void UnpaidDonationToStringTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void NoCommentToStringTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Equals Test

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffReasonEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffAmountEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffDonationDateEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPaymentDateEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffDonationPaidEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffCommentEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void NullPaymentDateEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void SomeDiffEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region DonationDate Test

        /// <summary>
        ///A test for DonationDate
        ///</summary>
        [TestMethod()]
        public void GoodDateDonationDateTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.DonationDate = expected;
            actual = target.DonationDate;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for DonationDate
        ///</summary>
        [TestMethod()]
        public void NullDateDonationDateTest()
        {
            Donation target = new Donation(); // TODO: Initialize to an appropriate value
            DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime actual;
            target.DonationDate = expected;
            actual = target.DonationDate;
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Ctor Test

        /// <summary>
        ///A test for Donation Constructor
        ///</summary>
        [TestMethod()]
        public void UnpaidDonationConstructorTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            double amount = 0F; // TODO: Initialize to an appropriate value
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime paymentDate = new DateTime(); // TODO: Initialize to an appropriate value
            string comments = string.Empty; // TODO: Initialize to an appropriate value
            Donation target = new Donation(id, reason, amount, donationDate, paymentDate, comments);
        }
        
        /// <summary>
        ///A test for Donation Constructor
        ///</summary>
        [TestMethod()]
        public void PaidDonationConstructorTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            double amount = 0F; // TODO: Initialize to an appropriate value
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime paymentDate = new DateTime(); // TODO: Initialize to an appropriate value
            string comments = string.Empty; // TODO: Initialize to an appropriate value
            Donation target = new Donation(id, reason, amount, donationDate, paymentDate, comments);
        }

        /// <summary>
        ///A test for Donation Constructor
        ///</summary>
        [TestMethod()]
        public void NoDonationDateConstructorTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            double amount = 0F; // TODO: Initialize to an appropriate value
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            DateTime paymentDate = new DateTime(); // TODO: Initialize to an appropriate value
            string comments = string.Empty; // TODO: Initialize to an appropriate value
            Donation target = new Donation(id, reason, amount, donationDate, paymentDate, comments);
        }

        #endregion
    }
}
