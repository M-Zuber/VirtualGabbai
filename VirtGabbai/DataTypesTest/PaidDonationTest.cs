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
        #region Test Data Members
        
        //Target Data Members
        PaidDonation targetPaidDonation = null;
        Donation baseUnpaidDonation = new Donation(1, "reason", 12.5, DateTime.Today, "comment");

        #endregion

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            targetPaidDonation = new PaidDonation(baseUnpaidDonation, DateTime.Today);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetPaidDonation = null;
        }
        //
        #endregion


        #region Equals Tests

        /// <summary>
        ///Comparing two paid donations with no differences
        ///</summary>
        [TestMethod()]
        public void PaidDonation_Equals_NoDifferences_Test()
        {
            PaidDonation otherPaidDonation = 
                new PaidDonation(baseUnpaidDonation, DateTime.Today);
            Assert.IsTrue(targetPaidDonation.Equals(otherPaidDonation));
        }

        /// <summary>
        ///Comparing two paid donations with a difference in the payment date
        ///</summary>
        [TestMethod()]
        public void PaidDonation_Equals_DifferenceInPaymentDate_Test()
        {
            PaidDonation otherPaidDonation =
                new PaidDonation(baseUnpaidDonation, DateTime.MaxValue);
            Assert.IsFalse(targetPaidDonation.Equals(otherPaidDonation));
        }

        /// <summary>
        ///Comparing two paid donations with a difference in every field
        ///</summary>
        [TestMethod()]
        public void PaidDonation_Equals_DifferencesInAllFields_Test()
        {
            PaidDonation otherPaidDonation =
                new PaidDonation(baseUnpaidDonation._Id * 2,
                                 baseUnpaidDonation.Reason + baseUnpaidDonation.Reason,
                                 baseUnpaidDonation.Amount * 2,
                                 DateTime.MinValue,
                                 baseUnpaidDonation.Comments + baseUnpaidDonation.Comments,
                                 DateTime.Today);
            Assert.IsFalse(targetPaidDonation.Equals(otherPaidDonation));
        }

        #endregion

        #region ToString Tests

        /// <summary>
        ///PaidDonation.ToString() with all fields set
        ///</summary>
        [TestMethod()]
        public void PaidDonation_ToString_WithComment_Test()
        {
            string expected = baseUnpaidDonation.ToString() + 
                " Paid on: \"" + targetPaidDonation.PaymentDate.ToString("dd/MM/yyyy") + "\"";
            string actual = targetPaidDonation.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///PaidDonation.ToString() with no comment
        ///</summary>
        [TestMethod()]
        public void PaidDonation_ToString_WithOutComment_Test()
        {
            baseUnpaidDonation.Comments = "";
            targetPaidDonation.Comments = "";
            string expected = baseUnpaidDonation.ToString() +
                " Paid on: \"" + targetPaidDonation.PaymentDate.ToString("dd/MM/yyyy") + "\"";
            string actual = targetPaidDonation.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
