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
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            string expected = "Donated for: \"" + target.Reason +
                              "\" Amount donated: \"" + target.Amount + 
                              "\" Date donated: \"" + target.DonationDate.ToString("dd/MM/yyyy") +
                              "\" Comments: \"" + "comments" +
                              "\" Date paid: \"" + target.PaymentDate.ToString("dd/MM/yyyy") + "\"";
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
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, "comments");
            string expected = "Donated for: \"" + target.Reason +
                              "\" Amount donated: \"" + target.Amount +
                              "\" Date donated: \"" + target.DonationDate.ToString("dd/MM/yyyy") +
                              "\" Comments: \"" + "comments" + "\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void NoCommentPaidToStringTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "");
            string expected = "Donated for: \"" + target.Reason +
                              "\" Amount donated: \"" + target.Amount +
                              "\" Date donated: \"" + target.DonationDate.ToString("dd/MM/yyyy") +
                              "\" Date paid: \"" + target.PaymentDate.ToString("dd/MM/yyyy") + "\"";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void NoCommentUnpaidToStringTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, "");
            string expected = "Donated for: \"" + target.Reason +
                              "\" Amount donated: \"" + target.Amount +
                              "\" Date donated: \"" + target.DonationDate.ToString("dd/MM/yyyy") + "\"";
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
        public void AllSamePaidDonationEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today,DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            bool expected = true;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameUnpaidDonationEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason", 23.09, DateTime.Today, "comments");
            bool expected = true;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(2, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffReasonEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason:2", 23.09, DateTime.Today, DateTime.Today, "comments");
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffAmountEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason", 23.10, DateTime.Today, DateTime.Today, "comments");
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffDonationDateEqualsTest()
        {
            DateTime donationDate = new DateTime(2013, 1, 1);
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason", 23.09, donationDate, DateTime.Today, "comments");
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPaymentDateEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason", 23.09, DateTime.Today, "comments");
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffDonationPaidEqualsTest()
        {
            Donation_Accessor target = new Donation_Accessor(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation_Accessor obj = new Donation_Accessor(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            obj.DonationPaid = false;
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffCommentEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments:2");
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void SomeDiffEqualsTest()
        {
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(1, "reason:2", 23.54, DateTime.Today, DateTime.Today, "comments:2");
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
            Donation target = new Donation(1, "reason", 23.09, DateTime.Today, DateTime.Today, "comments");
            Donation obj = new Donation(2, "reason:2", 23.87, DateTime.MinValue, DateTime.MaxValue, "comments:2");
            bool expected = false;
            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
