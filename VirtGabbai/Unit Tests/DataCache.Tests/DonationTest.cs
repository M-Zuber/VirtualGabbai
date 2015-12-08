using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataCache.Tests
{
    [TestClass()]
    public class DonationTest
    {
        Donation targetDonation = null;
        int id;
        string reason;
        double amount;
        string comments;
        DateTime donationDate = DateTime.Today;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            id = 1;
            reason = "Just because";
            amount = 10.5;
            comments = "not to much";

            targetDonation = new Donation { ID = id, Reason = reason, Amount = amount, DonationDate = donationDate, Comments = comments };
        }
       
        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetDonation = null;
        }

        #region ToString Test

        /// <summary>
        ///Donation.ToString() with all fields set
        ///</summary>
        [TestMethod()]
        public void Donation_ToString_AllFieldsSet()
        {
            string expected = "Donated for: \"" + targetDonation.Reason +
                              "\" Amount donated: \"" + targetDonation.Amount +
                              "\" Date donated: \"" + targetDonation.DonationDate.ToString("dd/MM/yyyy") +
                              "\" Comments: \"" + targetDonation.Comments + "\"";
            string actual = targetDonation.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Donation.ToString() with no comment
        ///</summary>
        [TestMethod()]
        public void Donation_ToString_NoComment()
        {
            Donation target = new Donation { ID = id, Reason = reason, Amount = amount, DonationDate = donationDate };
            string expected = "Donated for: \"" + target.Reason +
                              "\" Amount donated: \"" + target.Amount +
                              "\" Date donated: \"" + target.DonationDate.ToString("dd/MM/yyyy") + "\"";
            string actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Equals Test

        /// <summary>
        ///Comparing two donations with no differences
        ///</summary>
        [TestMethod()]
        public void Donation_Equals_NoDifferences()
        {
            Donation otherDonation = new Donation { ID = id, Reason = reason, Amount = amount, DonationDate = donationDate, Comments = comments };

            Assert.IsTrue(targetDonation.Equals(otherDonation));
            Assert.IsTrue(otherDonation.Equals(targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the id
        ///</summary>
        [TestMethod()]
        public void Donation_Equals_DifferenceInId()
        {
            Donation otherDonation = new Donation { ID = (id * 2), Reason = reason, Amount = amount, DonationDate = donationDate, Comments = comments };

            Assert.IsFalse(targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the reason
        ///</summary>
        [TestMethod()]
        public void Donation_Equals_DifferenceInReason()
        {
            Donation otherDonation = new Donation { ID = id, Reason = reason + reason, Amount = amount, DonationDate = donationDate, Comments = comments };

            Assert.IsFalse(targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the amount
        ///</summary>
        [TestMethod()]
        public void Donation_Equals_DifferenceInAmount()
        {
            Donation otherDonation = new Donation { ID = id, Reason = reason, Amount = (amount * 2), DonationDate = donationDate, Comments = comments };

            Assert.IsFalse(targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the donation date
        ///</summary>
        [TestMethod()]
        public void Donation_Equals_DifferenceInDonationDate()
        {
            Donation otherDonation = new Donation { ID = id, Reason = reason, Amount = amount, DonationDate = DateTime.MaxValue, Comments = comments };

            Assert.IsFalse(targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the comment
        ///</summary>
        [TestMethod()]
        public void Donation_Equals_DifferenceInComment()
        {
            Donation otherDonation = new Donation { ID = id, Reason = reason, Amount = amount, DonationDate = donationDate, Comments = comments + comments };

            Assert.IsFalse(targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in ever field
        ///</summary>
        [TestMethod()]
        public void Donation_Equals_DifferencesInAllFields()
        {
            Donation otherDonation = new Donation { ID = (id * 2), Reason = reason + reason, Amount = (amount * 2), DonationDate = DateTime.MaxValue, Comments = comments + comments };

            Assert.IsFalse(targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(targetDonation));
        }

        [TestMethod]
        public void Donation_Equals_Null_Returns_False()
        {
            Assert.IsFalse(targetDonation.Equals(null));
        }

        [TestMethod]
        public void Donation_Equals_Same_Ref_Returns_True()
        {
            var other = targetDonation;

            Assert.IsTrue(targetDonation.Equals(other));
            Assert.IsTrue(other.Equals(targetDonation));
        }

        [TestMethod]
        public void Donation_Equals_Non_Donation_Returns_True()
        {
            Assert.IsFalse(targetDonation.Equals(3));
        }

        #endregion
    }
}
