using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LocalTypesTest
{
    
    
    /// <summary>
    ///This is a test class for PaidDonationTest and is intended
    ///to contain all PaidDonationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PaidDonationTest
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


        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllEqualsTest()
        {
            Donation donationPaying = new Donation(1, "reason:1", 12.5, DateTime.Today, "");
            DateTime paymentDate = DateTime.Today;
            PaidDonation target = new PaidDonation(donationPaying, paymentDate);
            PaidDonation obj = new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "", DateTime.Today);
            bool expected = true;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void SomeDiffEqualsTest()
        {
            Donation donationPaying = new Donation(1, "reason:1", 12.5, DateTime.Today, "");
            DateTime paymentDate = DateTime.Today;
            PaidDonation target = new PaidDonation(donationPaying, paymentDate);
            PaidDonation obj = new PaidDonation(1, "reason:2", 12.5, DateTime.Today, "a comment", DateTime.Today);
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            Donation donationPaying = new Donation(1, "reason:1", 12.5, DateTime.Today, "");
            DateTime paymentDate = DateTime.Today;
            PaidDonation target = new PaidDonation(donationPaying, paymentDate);
            PaidDonation obj = new PaidDonation(2, "reason:2", 25, DateTime.MinValue, "a comment", DateTime.MaxValue);
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ToString Tests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Donation donationPaying = new Donation(1, "reason:1", 12.5, DateTime.Today, "a comment");
            DateTime paymentDate = DateTime.Today;
            PaidDonation target = new PaidDonation(donationPaying, paymentDate);
            string expected = donationPaying.ToString() + " Paid on: \"" + paymentDate.ToString("dd/MM/yyyy") + "\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringNoCommentTest()
        {
            Donation donationPaying = new Donation(1, "reason:1", 12.5, DateTime.Today, "");
            DateTime paymentDate = DateTime.Today;
            PaidDonation target = new PaidDonation(donationPaying, paymentDate);
            string expected = donationPaying.ToString() + " Paid on: \"" + paymentDate.ToString("dd/MM/yyyy") + "\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
