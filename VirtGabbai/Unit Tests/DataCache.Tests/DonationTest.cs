using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataCache.Tests
{
    [TestClass]
    public class DonationTest
    {
        private Donation _targetDonation;
        private int _id;
        private string _reason;
        private double _amount;
        private string _comments;
        private readonly DateTime _donationDate = DateTime.Today;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _id = 1;
            _reason = "Just because";
            _amount = 10.5;
            _comments = "not to much";

            _targetDonation = new Donation { Id = _id, Reason = _reason, Amount = _amount, DonationDate = _donationDate, Comments = _comments };
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _targetDonation = null;
        }

        #region ToString Test

        /// <summary>
        ///Donation.ToString() with all fields set
        ///</summary>
        [TestMethod]
        public void Donation_ToString_AllFieldsSet()
        {
            var expected = "Donated for: \"" + _targetDonation.Reason +
                              "\" Amount donated: \"" + _targetDonation.Amount +
                              "\" Date donated: \"" + _targetDonation.DonationDate.ToString("dd/MM/yyyy") +
                              "\" Comments: \"" + _targetDonation.Comments + "\"";
            var actual = _targetDonation.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Donation.ToString() with no comment
        ///</summary>
        [TestMethod]
        public void Donation_ToString_NoComment()
        {
            var target = new Donation { Id = _id, Reason = _reason, Amount = _amount, DonationDate = _donationDate };
            var expected = "Donated for: \"" + target.Reason +
                              "\" Amount donated: \"" + target.Amount +
                              "\" Date donated: \"" + target.DonationDate.ToString("dd/MM/yyyy") + "\"";
            var actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Equals Test

        /// <summary>
        ///Comparing two donations with no differences
        ///</summary>
        [TestMethod]
        public void Donation_Equals_NoDifferences()
        {
            var otherDonation = new Donation { Id = _id, Reason = _reason, Amount = _amount, DonationDate = _donationDate, Comments = _comments };

            Assert.IsTrue(_targetDonation.Equals(otherDonation));
            Assert.IsTrue(otherDonation.Equals(_targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the id
        ///</summary>
        [TestMethod]
        public void Donation_Equals_DifferenceInId()
        {
            var otherDonation = new Donation { Id = _id * 2, Reason = _reason, Amount = _amount, DonationDate = _donationDate, Comments = _comments };

            Assert.IsFalse(_targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(_targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the reason
        ///</summary>
        [TestMethod]
        public void Donation_Equals_DifferenceInReason()
        {
            var otherDonation = new Donation { Id = _id, Reason = _reason + _reason, Amount = _amount, DonationDate = _donationDate, Comments = _comments };

            Assert.IsFalse(_targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(_targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the amount
        ///</summary>
        [TestMethod]
        public void Donation_Equals_DifferenceInAmount()
        {
            var otherDonation = new Donation { Id = _id, Reason = _reason, Amount = _amount * 2, DonationDate = _donationDate, Comments = _comments };

            Assert.IsFalse(_targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(_targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the donation date
        ///</summary>
        [TestMethod]
        public void Donation_Equals_DifferenceInDonationDate()
        {
            var otherDonation = new Donation { Id = _id, Reason = _reason, Amount = _amount, DonationDate = DateTime.MaxValue, Comments = _comments };

            Assert.IsFalse(_targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(_targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in the comment
        ///</summary>
        [TestMethod]
        public void Donation_Equals_DifferenceInComment()
        {
            var otherDonation = new Donation { Id = _id, Reason = _reason, Amount = _amount, DonationDate = _donationDate, Comments = _comments + _comments };

            Assert.IsFalse(_targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(_targetDonation));
        }

        /// <summary>
        ///Comparing two donations with a difference in ever field
        ///</summary>
        [TestMethod]
        public void Donation_Equals_DifferencesInAllFields()
        {
            var otherDonation = new Donation { Id = _id * 2, Reason = _reason + _reason, Amount = _amount * 2, DonationDate = DateTime.MaxValue, Comments = _comments + _comments };

            Assert.IsFalse(_targetDonation.Equals(otherDonation));
            Assert.IsFalse(otherDonation.Equals(_targetDonation));
        }

        [TestMethod]
        public void Donation_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_targetDonation.Equals(null));
        }

        [TestMethod]
        public void Donation_Equals_Same_Ref_Returns_True()
        {
            var other = _targetDonation;

            Assert.IsTrue(_targetDonation.Equals(other));
            Assert.IsTrue(other.Equals(_targetDonation));
        }

        [TestMethod]
        public void Donation_Equals_Non_Donation_Returns_True()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_targetDonation.Equals(3));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        #endregion
    }
}
