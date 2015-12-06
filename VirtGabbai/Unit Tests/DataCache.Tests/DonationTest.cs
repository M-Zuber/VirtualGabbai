//using LocalTypes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace LocalTypesTest
//{
    
    
//    /// <summary>
//    ///This is a test class for DonationTest and is intended
//    ///to contain all DonationTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class DonationTest
//    {


//        private TestContext testContextInstance;

//        #region Test Data Members

//        //Target Data Members
//        Donation targetDonation = null;
//        int id;
//        string reason;
//        double amount;
//        string comments;
//        DateTime donationDate = DateTime.Today;

//        #endregion

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
//            id = 1;
//            reason = "Just because";
//            amount = 10.5;
//            comments = "not to much";

//            targetDonation = new Donation(id, reason, amount, donationDate, comments);
//        }
//        //
//        //Use TestCleanup to run code after each test has run
//        [TestCleanup()]
//        public void MyTestCleanup()
//        {
//            targetDonation = null;
//        }
//        //
//        #endregion

//        #region ToString Test

//        /// <summary>
//        ///Donation.ToString() with all fields set
//        ///</summary>
//        [TestMethod()]
//        public void Donation_ToString_AllFieldsSet()
//        {
//            string expected = "Donated for: \"" + targetDonation.Reason +
//                              "\" Amount donated: \"" + targetDonation.Amount + 
//                              "\" Date donated: \"" + targetDonation.DonationDate.ToString("dd/MM/yyyy") +
//                              "\" Comments: \"" + targetDonation.Comments + "\"";
//            string actual = targetDonation.ToString();
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///Donation.ToString() with no comment
//        ///</summary>
//        [TestMethod()]
//        public void Donation_ToString_NoComment()
//        {
//            Donation target = new Donation(id, reason, amount, donationDate, "");
//            string expected = "Donated for: \"" + target.Reason +
//                              "\" Amount donated: \"" + target.Amount +
//                              "\" Date donated: \"" + target.DonationDate.ToString("dd/MM/yyyy") + "\"";
//            string actual = target.ToString();
//            Assert.AreEqual(expected, actual);
//        }
        
//        #endregion

//        #region Equals Test

//        /// <summary>
//        ///Comparing two donations with no differences
//        ///</summary>
//        [TestMethod()]
//        public void Donation_Equals_NoDifferences()
//        {
//            Donation otherDonation = new Donation(id, reason, amount, donationDate, comments);
//            Assert.IsTrue(targetDonation.Equals(otherDonation));
//        }

//        /// <summary>
//        ///Comparing two donations with a difference in the id
//        ///</summary>
//        [TestMethod()]
//        public void Donation_Equals_DifferenceInId()
//        {
//            Donation otherDonation = new Donation((id * 2), reason, amount, donationDate, comments);
//            Assert.IsFalse(targetDonation.Equals(otherDonation));
//        }

//        /// <summary>
//        ///Comparing two donations with a difference in the reason
//        ///</summary>
//        [TestMethod()]
//        public void Donation_Equals_DifferenceInReason()
//        {
//            Donation otherDonation = new Donation(id, reason + reason, amount, donationDate, comments);
//            Assert.IsFalse(targetDonation.Equals(otherDonation));
//        }

//        /// <summary>
//        ///Comparing two donations with a difference in the amount
//        ///</summary>
//        [TestMethod()]
//        public void Donation_Equals_DifferenceInAmount()
//        {
//            Donation otherDonation = new Donation(id, reason, (amount * 2), donationDate, comments);
//            Assert.IsFalse(targetDonation.Equals(otherDonation));
//        }

//        /// <summary>
//        ///Comparing two donations with a difference in the donation date
//        ///</summary>
//        [TestMethod()]
//        public void Donation_Equals_DifferenceInDonationDate()
//        {
//            Donation otherDonation = new Donation(id, reason, amount, DateTime.MaxValue, comments);
//            Assert.IsFalse(targetDonation.Equals(otherDonation));
//        }

//        /// <summary>
//        ///Comparing two donations with a difference in the comment
//        ///</summary>
//        [TestMethod()]
//        public void Donation_Equals_DifferenceInComment()
//        {
//            Donation otherDonation = new Donation(id, reason, amount, donationDate, comments + comments);
//            Assert.IsFalse(targetDonation.Equals(otherDonation));
//        }

//        /// <summary>
//        ///Comparing two donations with a difference in ever field
//        ///</summary>
//        [TestMethod()]
//        public void Donation_Equals_DifferencesInAllFields()
//        {
//            Donation otherDonation = 
//                new Donation((id *2), reason + reason, (amount * 2), DateTime.MaxValue, comments + comments);
//            Assert.IsFalse(targetDonation.Equals(otherDonation));
//        }

//        #endregion
//    }
//}
