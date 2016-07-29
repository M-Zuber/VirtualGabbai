using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass()]
    public class AccountTest
    {
        private Account targetAccount = null;
        private List<Donation> allDonations = null;
        DateTime lastMonthlyPaymentDate = DateTime.Today;
        int id;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            id = 1;
            allDonations = new List<Donation>()
            {
                new Donation { ID = 1, Reason = "reason:1", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { ID = 2, Reason = "reason:2", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment"},
                new Donation { ID  =3, Reason = "reason:3", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { ID = 4, Reason = "reason:4", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { ID = 5, Reason = "reason:5", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment" },
                new Donation { ID = 6, Reason = "reason:6", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true },
                new Donation { ID = 7, Reason = "reason:7", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true },
                new Donation { ID = 8, Reason = "reason:8", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true }
            };

            targetAccount = new Account { ID = id, LastMonthlyPaymentDate = lastMonthlyPaymentDate, Donations = allDonations };
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetAccount = null;
            allDonations = null;
        }

        #region Equals Tests

        [TestMethod]
        public void Account_Equals_Other_Is_Null_Returns_False()
        {
            Assert.IsFalse(targetAccount.Equals(null));
        }

        [TestMethod]
        public void Account_Equals_Other_Is_Not_Account_Returns_False()
        {
            Assert.IsFalse(targetAccount.Equals(1));
        }

        [TestMethod]
        public void Account_Equals_Same_Reference_Returns_True()
        {
            var other = targetAccount;

            Assert.IsTrue(targetAccount.Equals(other));
            Assert.IsTrue(other.Equals(targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the donations list
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInDonations()
        {
            List<Donation> otherDonations = new List<Donation>(allDonations);
            otherDonations.Add(new Donation { ID = 34, Reason = "reason:6", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true });
            otherDonations.Add(new Donation { ID = 7, Reason = "reason:7", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true });
            otherDonations.Add(new Donation { ID = 56, Reason = "reason:8", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true });

            Account otherAccount = new Account { ID = id, LastMonthlyPaymentDate = lastMonthlyPaymentDate, Donations = otherDonations };

            Assert.IsFalse(targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the last montly payment date
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInLastMonthlyPayment()
        {
            Account otherAccount = new Account { ID = id, LastMonthlyPaymentDate = DateTime.MaxValue, Donations = allDonations };

            Assert.IsFalse(targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the monthly payment total
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInMonthlyPaymentTotal()
        {
            Account otherAccount = new Account { ID = id, MonthlyPaymentAmount = 2, LastMonthlyPaymentDate = lastMonthlyPaymentDate, Donations = allDonations };

            Assert.IsFalse(targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the id
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInId()
        {
            Account otherAccount = new Account { ID = (id * 2), LastMonthlyPaymentDate = lastMonthlyPaymentDate, Donations = allDonations };

            Assert.IsFalse(targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with no differences
        ///</summary>
        [TestMethod()]
        public void Account_Equals_NoDifferences()
        {
            Account otherAccount = new Account { ID = id, LastMonthlyPaymentDate = lastMonthlyPaymentDate, Donations = allDonations };

            Assert.IsTrue(targetAccount.Equals(otherAccount));
            Assert.IsTrue(otherAccount.Equals(targetAccount));
        }

        /// <summary>
        ///Comparing two accounts with every field different
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInEveryField()
        {
            List<Donation> otherDonation = new List<Donation>(allDonations);
            otherDonation.Add(new Donation { ID = 6, Reason = "reason:6", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true });
            otherDonation.Add(new Donation { ID = 7, Reason = "reason:7", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true });
            otherDonation.Add(new Donation { ID = 8, Reason = "reason:8", Amount = 12.5, DonationDate = DateTime.Today, Comments = "comment", DatePaid = DateTime.Today, Paid = true });

            Account otherAccount = new Account { ID = (id * 2), LastMonthlyPaymentDate = DateTime.MaxValue, Donations = otherDonation };

            Assert.IsFalse(targetAccount.Equals(otherAccount));
            Assert.IsFalse(otherAccount.Equals(targetAccount));
        }

        #endregion

        [TestMethod]
        public void Account_GetHashCode_Same_Properties_Returns_Same_Value()
        {
            var otherAccount = new Account { ID = id, LastMonthlyPaymentDate = lastMonthlyPaymentDate, Donations = allDonations };

            Assert.AreEqual(targetAccount.GetHashCode(), otherAccount.GetHashCode());
        }

        /// <summary>
        ///Account.ToString() test
        ///</summary>
        [TestMethod()]
        public void Account_ToStringTest()
        {
            string donations = "";
            foreach (Donation CurrDonation in targetAccount.UnpaidDonations)
            {
                donations += CurrDonation.ToString();
                donations += "\n";
            }

            foreach (Donation CurrPaidDonation in targetAccount.PaidDonations)
            {
                donations += CurrPaidDonation.ToString();
                donations += "\n";
            }
            donations = donations.Remove(donations.Length - 1);
            string expected = "Total owed for the monthly payment: '" + targetAccount.MonthlyPaymentTotal + "'\n" +
                              "Last month the monthly payment was made: '" + targetAccount.LastMonthlyPaymentDate?.Month + "'\n" +
                              "Donations:\n" + donations;
            string actual = targetAccount.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AccountMonthlyPaymentTotal_SameDateAsPaymentDate_EqualZero()
        {
            Account actual = new Account(1, 100.30M, DateTime.Today, new List<Donation>());
            Assert.AreEqual(0, actual.MonthlyPaymentTotal);
        }

        [TestMethod()]
        public void AccountMonthlyPaymentTotal_LastPaymentMadeOnDayOfPreviousMonth_Equals()
        {
            DateTime lastPaymentDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month - 1));
            Account actual = new Account(1, 50.50M, lastPaymentDateTime, new List<Donation>());
            decimal expectedPaymentTotal = 50.50M * Decimal.Divide((DateTime.Today - lastPaymentDateTime).Days, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            AssertAreEqualDecimal(expectedPaymentTotal, actual.MonthlyPaymentTotal, 0.0001M);
        }

        [TestMethod()]
        public void AccountMonthlyPaymentTotal_LastPaymentMadeOnDayInCurrentMonth_Equals()
        {
            DateTime lastPaymentDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 25);
            Account actual = new Account(1, 50.50M, lastPaymentDateTime, new List<Donation>());
            decimal expectedPaymentTotal = 50.50M * Decimal.Divide((decimal) (DateTime.Today - lastPaymentDateTime).TotalDays, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            AssertAreEqualDecimal(expectedPaymentTotal, actual.MonthlyPaymentTotal, delta: 0.0001M);
        }

        [TestMethod()]
        public void AccountMonthlyPaymentTotal_LastMonthlyPaymentDateIsNull_EqualsZero()
        {
            Account actual = new Account(1, 50.50M, null, new List<Donation>());
            Assert.AreEqual(0, actual.MonthlyPaymentTotal);
        }

        [TestMethod()]
        public void AccountMonthlyPaymentTotal_LastMonthlyPaymentDateIsInAPreviousMonth_EqualsZero()
        {
            DateTime lastPaymentDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.AddMonths(-5).Month, 25);
            Account actual = new Account(1, 50.50M, lastPaymentDateTime, new List<Donation>());
            decimal expectedPaymentTotal = 0.0M;
            DateTime firstBillableDate = lastPaymentDateTime.AddDays(1);
            while (firstBillableDate <= DateTime.Today)
            {
                expectedPaymentTotal += decimal.Divide(1, DateTime.DaysInMonth(firstBillableDate.Year, firstBillableDate.Month));
                firstBillableDate = firstBillableDate.AddDays(1);
            }
            expectedPaymentTotal *= 50.50M;
            Assert.AreEqual(expectedPaymentTotal, actual.MonthlyPaymentTotal);
        }

        private static void AssertAreEqualDecimal(decimal expected, decimal actual, decimal delta)
        {
            Assert.IsTrue(Math.Abs(expected - actual) < delta);
        }
    }
}
