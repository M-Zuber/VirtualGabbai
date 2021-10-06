using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass]
    public class AccountTest
    {
        private Account _targetAccount;
        private List<Donation> _allDonations;
        private readonly DateTime _lastMonthlyPaymentDate = DateTime.Today;
        private int _id;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _id = 1;
            _allDonations = new List<Donation>
            {
                new Donation { Id = 1, Reason = "reason:1", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { Id = 2, Reason = "reason:2", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment"},
                new Donation { Id  =3, Reason = "reason:3", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { Id = 4, Reason = "reason:4", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { Id = 5, Reason = "reason:5", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { Id = 6, Reason = "reason:6", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true },
                new Donation { Id = 7, Reason = "reason:7", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true },
                new Donation { Id = 8, Reason = "reason:8", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true }
            };

            _targetAccount = new Account { Id = _id, LastMonthlyPaymentDate = _lastMonthlyPaymentDate, Donations = _allDonations };
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _targetAccount = null;
            _allDonations = null;
        }

        #region Equals Tests

        [TestMethod]
        public void Account_Equals_Other_Is_Null_Returns_False()
        {
            Assert.IsFalse(_targetAccount.Equals(null));
        }

        [TestMethod]
        public void Account_Equals_Other_Is_Not_Account_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_targetAccount.Equals(1));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void Account_Equals_Same_Reference_Returns_True()
        {
            var other = _targetAccount;

            Assert.IsTrue(_targetAccount.Equals(other));
            Assert.IsTrue(other.Equals(_targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the donations list
        ///</summary>
        [TestMethod]
        public void Account_Equals_DifferenceInDonations()
        {
            var otherDonations = new List<Donation>(_allDonations)
            {
                new Donation
                {
                    Id = 34,
                    Reason = "reason:6",
                    Amount = 12.5,
                    DonationDate = DateTime.Today,
                    Comments = "comment",
                    DatePaid = DateTime.Today,
                    Paid = true
                },
                new Donation
                {
                    Id = 7,
                    Reason = "reason:7",
                    Amount = 12.5,
                    DonationDate = DateTime.Today,
                    Comments = "comment",
                    DatePaid = DateTime.Today,
                    Paid = true
                },
                new Donation
                {
                    Id = 56,
                    Reason = "reason:8",
                    Amount = 12.5,
                    DonationDate = DateTime.Today,
                    Comments = "comment",
                    DatePaid = DateTime.Today,
                    Paid = true
                }
            };

            var otherAccount = new Account { Id = _id, LastMonthlyPaymentDate = _lastMonthlyPaymentDate, Donations = otherDonations };

            Assert.IsFalse(_targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(_targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the last monthly payment date
        ///</summary>
        [TestMethod]
        public void Account_Equals_DifferenceInLastMonthlyPayment()
        {
            var otherAccount = new Account { Id = _id, LastMonthlyPaymentDate = DateTime.MaxValue, Donations = _allDonations };

            Assert.IsFalse(_targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(_targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the monthly payment total
        ///</summary>
        [TestMethod]
        public void Account_Equals_DifferenceInMonthlyPaymentTotal()
        {
            var otherAccount = new Account { Id = _id, MonthlyPaymentAmount = 2, LastMonthlyPaymentDate = _lastMonthlyPaymentDate, Donations = _allDonations };

            Assert.IsFalse(_targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(_targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the id
        ///</summary>
        [TestMethod]
        public void Account_Equals_DifferenceInId()
        {
            var otherAccount = new Account { Id = _id * 2, LastMonthlyPaymentDate = _lastMonthlyPaymentDate, Donations = _allDonations };

            Assert.IsFalse(_targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(_targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with no differences
        ///</summary>
        [TestMethod]
        public void Account_Equals_NoDifferences()
        {
            var otherAccount = new Account { Id = _id, LastMonthlyPaymentDate = _lastMonthlyPaymentDate, Donations = _allDonations };

            Assert.IsTrue(_targetAccount.Equals(otherAccount));
            Assert.IsTrue(otherAccount.Equals(_targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with every field different
        ///</summary>
        [TestMethod]
        public void Account_Equals_DifferenceInEveryField()
        {
            var otherDonation = new List<Donation>(_allDonations)
            {
                new Donation { Id = 6, Reason = "reason:6", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true },
                new Donation { Id = 7, Reason = "reason:7", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true },
                new Donation { Id = 8, Reason = "reason:8", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true }
            };

            var otherAccount = new Account { Id = _id * 2, LastMonthlyPaymentDate = DateTime.MaxValue, Donations = otherDonation };

            Assert.IsFalse(_targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(_targetAccount));
        }

        #endregion

        [TestMethod]
        public void Account_GetHashCode_Same_Properties_Returns_Same_Value()
        {
            var otherAccount = new Account { Id = _id, LastMonthlyPaymentDate = _lastMonthlyPaymentDate, Donations = _allDonations };

            Assert.AreEqual(_targetAccount.GetHashCode(), otherAccount.GetHashCode());
        }

        /// <summary>
        ///Account.ToString() test
        ///</summary>
        [TestMethod]
        public void Account_ToStringTest()
        {
            var donations = "";
            foreach (var curDonation in _targetAccount.UnpaidDonations)
            {
                donations += curDonation.ToString();
                donations += "\n";
            }

            foreach (var curPaidDonation in _targetAccount.PaidDonations)
            {
                donations += curPaidDonation.ToString();
                donations += "\n";
            }
            donations = donations.Remove(donations.Length - 1);
            var expected = "Total owed for the monthly payment: '" + _targetAccount.MonthlyPaymentTotal + "'\n" +
                              "Last month the monthly payment was made: '" + _targetAccount.LastMonthlyPaymentDate?.Month + "'\n" +
                              "Donations:\n" + donations;
            var actual = _targetAccount.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AccountMonthlyPaymentTotal_SameDateAsPaymentDate_EqualZero()
        {
            var actual = new Account
            {
                Id = 1,
                MonthlyPaymentAmount = 100.30M,
                LastMonthlyPaymentDate = DateTime.Today,
                Donations = new List<Donation>()
            };

            Assert.AreEqual(0, actual.MonthlyPaymentTotal);
        }

        [TestMethod]
        public void AccountMonthlyPaymentTotal_LastPaymentMadeOnDayOfPreviousMonth_Equals()
        {
            var lastPaymentDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month - 1));
            var actual = new Account
            {
                Id = 1,
                MonthlyPaymentAmount = 50.50M,
                LastMonthlyPaymentDate = lastPaymentDateTime,
                Donations = new List<Donation>()
            };
            const decimal expectedPaymentTotal = 50.50M;
            Assert.AreEqual(expectedPaymentTotal, actual.MonthlyPaymentTotal);
        }

        [TestMethod]
        public void AccountMonthlyPaymentTotal_LastPaymentMadeOnDayInCurrentMonth_EqualsZero()
        {
            var lastPaymentDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var actual = new Account
            {
                Id = 1,
                MonthlyPaymentAmount = 50.50M,
                LastMonthlyPaymentDate = lastPaymentDateTime,
                Donations = new List<Donation>()
            };
            Assert.AreEqual(0, actual.MonthlyPaymentTotal);
        }

        [TestMethod]
        public void AccountMonthlyPaymentTotal_LastMonthlyPaymentDateIsNull_EqualsZero()
        {
            var actual = new Account
            {
                Id = 1,
                MonthlyPaymentAmount = 50.50M,
                LastMonthlyPaymentDate = null,
                Donations = new List<Donation>()
            };
            Assert.AreEqual(0, actual.MonthlyPaymentTotal);
        }

        [TestMethod]
        public void AccountMonthlyPaymentTotal_LastMonthlyPaymentDateIsInAPreviousMonth_EqualsZero()
        {
            var lastPaymentDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(-5).Month, 25);
            var actual = new Account
            {
                Id = 1,
                MonthlyPaymentAmount = 50.50M,
                LastMonthlyPaymentDate = lastPaymentDateTime,
                Donations = new List<Donation>()
            };
            const decimal expectedPaymentTotal = 50.50M * 5;
            Assert.AreEqual(expectedPaymentTotal, actual.MonthlyPaymentTotal);
        }
    }
}
