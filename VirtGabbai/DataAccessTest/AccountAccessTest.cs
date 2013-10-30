using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using DataCache;
using DataTypes;
using Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for AccountAccessTest and is intended
    ///to contain all AccountAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AccountAccessTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            if (!Cache.CacheData.t_people.Any(person => person.C_id == 1))
            {
                Cache.CacheData.t_people.AddObject(t_people.Createt_people(1));
            }
            if (!Cache.CacheData.t_accounts.Any(account => account.C_id == 1))
            {
                Cache.CacheData.t_accounts.AddObject(t_accounts.Createt_accounts(1, 1));
            }
            for (int newAccountIndex = 2; newAccountIndex <= 10; newAccountIndex++)
            {
                Cache.CacheData.t_accounts.AddObject(t_accounts.Createt_accounts(newAccountIndex, 1));
            }

            for (int newDonationIndex = 101; newDonationIndex <= 105; newDonationIndex++)
            {
                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
                {
                    var newDonation = t_donations.Createt_donations(
                                newDonationIndex, 2, "reason:" + newDonationIndex, 12.5, DateTime.Today, false);
                    Cache.CacheData.t_donations.AddObject(newDonation); 
                }
            }
            for (int newDonationIndex = 106; newDonationIndex <= 110; newDonationIndex++)
            {
                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
                {
                    var newDonation = t_donations.Createt_donations(
                               newDonationIndex, 2, "reason:" + newDonationIndex, 12.5, DateTime.Today, true);
                    newDonation.date_paid = DateTime.Today;
                    Cache.CacheData.t_donations.AddObject(newDonation); 
                }
            }
            Cache.CacheData.SaveChanges();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var donations = (from donate in Cache.CacheData.t_donations select donate).ToList<t_donations>();
            var accounts = (from account in Cache.CacheData.t_accounts select account).ToList<t_accounts>();
            var peoples = (from person in Cache.CacheData.t_people select person).ToList<t_people>();
            for (int i = 0; i < donations.Count; i++)
            {
                Cache.CacheData.t_donations.DeleteObject(donations[i]);
            }
            for (int i = 0; i < accounts.Count; i++)
            {
                Cache.CacheData.t_accounts.DeleteObject(accounts[i]);
            }
            for (int i = 0; i < peoples.Count; i++)
            {
                Cache.CacheData.t_people.DeleteObject(peoples[i]);
            }
            Cache.CacheData.SaveChanges();
        }
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

        #region Add Tests

        /// <summary>
        ///A test for AddMultipleNewAccounts
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewAccountsTest()
        {
            List<Account> newAccountList = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            AccountAccess.AddMultipleNewAccounts(newAccountList, personId);
        }

        /// <summary>
        ///A test for AddNewAccount
        ///</summary>
        [TestMethod()]
        public void AddNewAccountTest()
        {
            Account newAccount = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = AccountAccess.AddNewAccount(newAccount, personId);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Conversion Tests

        /// <summary>
        ///A test for ConvertSingleDbAccountToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleDbAccountToLocalTypeTest()
        {
            t_accounts dbTypeAccount = Cache.CacheData.t_accounts.First(account => account.C_id == 2);
            
            Account expected = new Account(2,0, DateTime.Today, 
                new List<Donation>()
                {
                    new Donation(101, "reason:101", 12.5, DateTime.Today, ""),
                    new Donation(102, "reason:102", 12.5, DateTime.Today, ""),
                    new Donation(103, "reason:103", 12.5, DateTime.Today, ""),
                    new Donation(104, "reason:104", 12.5, DateTime.Today, ""),
                    new Donation(105, "reason:105", 12.5, DateTime.Today, ""),
                    new PaidDonation(106, "reason:106", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(107, "reason:107", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(108, "reason:108", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(109, "reason:109", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(110, "reason:110", 12.5, DateTime.Today, "", DateTime.Today)
            });

            Account actual;
            actual = AccountAccess_Accessor.ConvertSingleDbAccountToLocalType(dbTypeAccount);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalAccountToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleLocalAccountToDbTypeTest()
        {
            Account localTypeAccount = new Account(2, 34, DateTime.Today,
                new List<Donation>()
                {
                    new Donation(101, "reason:101", 12.5, DateTime.Today, ""),
                    new Donation(102, "reason:102", 12.5, DateTime.Today, ""),
                    new Donation(103, "reason:103", 12.5, DateTime.Today, ""),
                    new Donation(104, "reason:104", 12.5, DateTime.Today, ""),
                    new Donation(105, "reason:105", 12.5, DateTime.Today, ""),
                    new PaidDonation(106, "reason:106", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(107, "reason:107", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(108, "reason:108", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(109, "reason:109", 12.5, DateTime.Today, "", DateTime.Today),
                    new PaidDonation(110, "reason:110", 12.5, DateTime.Today, "", DateTime.Today)
            });
            int personId = 1;
            t_accounts expected = t_accounts.Createt_accounts(2,1);
            expected.monthly_total = 34;
            expected.last_month_paid = DateTime.Today;
            
            t_accounts actual;
            actual = AccountAccess_Accessor.ConvertSingleLocalAccountToDbType(localTypeAccount, personId);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.person_id, actual.person_id);
            Assert.AreEqual(expected.last_month_paid, actual.last_month_paid);
            Assert.AreEqual(expected.monthly_total, actual.monthly_total);
        }

        #endregion
        
        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultipleAccounts
        ///</summary>
        [TestMethod()]
        public void DeleteMultipleAccountsTest()
        {
            List<Account> deletedAccountList = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            AccountAccess.DeleteMultipleAccounts(deletedAccountList, personId);
        }

        /// <summary>
        ///A test for DeleteSingleAccount
        ///</summary>
        [TestMethod()]
        public void DeleteSingleAccountTest()
        {
            Account deletedAccount = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = AccountAccess.DeleteSingleAccount(deletedAccount, personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteSingleAccount
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonexstintAccountTest()
        {
            Account deletedAccount = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = AccountAccess.DeleteSingleAccount(deletedAccount, personId);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Get Tests

        /// <summary>
        ///A test for GetAllAccounts
        ///</summary>
        [TestMethod()]
        public void GetAllAccountsTest()
        {
            List<Account> expected = null; // TODO: Initialize to an appropriate value
            List<Account> actual;
            actual = AccountAccess.GetAllAccounts();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByAccountId
        ///</summary>
        [TestMethod()]
        public void GetByAccountIdTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByAccountId(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByNonExsitentAccountId
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentAccountIdTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByAccountId(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonation
        ///</summary>
        [TestMethod()]
        public void GetByDonationTest()
        {
            Donation donationToLookBy = null; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByDonation(donationToLookBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonation
        ///</summary>
        [TestMethod()]
        public void GetByNonexsitentDonationTest()
        {
            Donation donationToLookBy = null; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByDonation(donationToLookBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonation
        ///</summary>
        [TestMethod()]
        public void GetByDonationIdTest()
        {
            int donationId = 0; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByDonation(donationId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonation
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentDonationIdTest()
        {
            int donationId = 0; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByDonation(donationId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByLastMonthlyPaymentDate
        ///</summary>
        [TestMethod()]
        public void GetByLastMonthlyPaymentDateTest()
        {
            DateTime lastPayment = new DateTime(); // TODO: Initialize to an appropriate value
            List<Account> expected = null; // TODO: Initialize to an appropriate value
            List<Account> actual;
            actual = AccountAccess.GetByLastMonthlyPaymentDate(lastPayment);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByMonthlyPaymentTotal
        ///</summary>
        [TestMethod()]
        public void GetByMonthlyPaymentTotalTest()
        {
            int monthlyTotal = 0; // TODO: Initialize to an appropriate value
            List<Account> expected = null; // TODO: Initialize to an appropriate value
            List<Account> actual;
            actual = AccountAccess.GetByMonthlyPaymentTotal(monthlyTotal);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPersonId
        ///</summary>
        [TestMethod()]
        public void GetByPersonIdTest()
        {
            int personId = 0; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByPersonId(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPersonId
        ///</summary>
        [TestMethod()]
        public void GetByNonExsistentPersonIdTest()
        {
            int personId = 0; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
            Account actual;
            actual = AccountAccess.GetByPersonId(personId);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllAccounts
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllAccountsTest()
        {
            List<t_accounts> expected = (from acc in Cache.CacheData.t_accounts
                                         select acc).ToList<t_accounts>();
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.LookupAllAccounts();
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAccountId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByAccountIdTest()
        {
            int accountId = 2;
            t_accounts expected = 
                Cache.CacheData.t_accounts.First(wantedAccount => wantedAccount.C_id == accountId);
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByAccountId(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAccountId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentAccountIdTest()
        {
            int accountId = 0;
            t_accounts expected = null;
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByAccountId(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByDonationIdTest()
        {
            int donationId = 105;
            t_accounts expected = Cache.CacheData.t_accounts.First(wantedAc => wantedAc.C_id == 2);
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByDonation(donationId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentDonationIdTest()
        {
            int donationId = 0;
            t_accounts expected = null;
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByDonation(donationId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByDonationTest()
        {
            Donation donationToLookBy = new Donation(102, "reason:102", 12.5, DateTime.Today, "");
            t_accounts expected = Cache.CacheData.t_accounts.First(wAcc => wAcc.C_id == 2);
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByDonation(donationToLookBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentDonationTest()
        {
            Donation donationToLookBy = new Donation(50, "reason:102", 12.5, DateTime.Today, ""); ;
            t_accounts expected = null;
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByDonation(donationToLookBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByLastMonthlyPaymentDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByLastMonthlyPaymentDateTest()
        {
            DateTime lastPayment = DateTime.Today;
            List<t_accounts> expected = (from currAcc in Cache.CacheData.t_accounts
                                         where currAcc.last_month_paid == lastPayment
                                         select currAcc).ToList();
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.LookupByLastMonthlyPaymentDate(lastPayment);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByMonthlyPaymentTotal
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByMonthlyPaymentTotalTest()
        {
            int monthlyTotal = 0;
            List<t_accounts> expected = (from CurrAccount in Cache.CacheData.t_accounts
                                         where CurrAccount.monthly_total == monthlyTotal
                                         select CurrAccount).ToList();
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.LookupByMonthlyPaymentTotal(monthlyTotal);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPersonId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPersonIdTest()
        {
            int personId = 1;
            t_accounts expected = Cache.CacheData.t_accounts.First(acc => acc.C_id == personId);
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByPersonId(personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPersonId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExsistentPersonIdTest()
        {
            int personId = 0;
            t_accounts expected = null;
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByPersonId(personId);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region Update Tests
        
        /// <summary>
        ///A test for UpdateMultipleAccounts
        ///</summary>
        [TestMethod()]
        public void UpdateMultipleAccountsTest()
        {
            List<Account> updatedAccountList = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            AccountAccess.UpdateMultipleAccounts(updatedAccountList, personId);
        }

        /// <summary>
        ///A test for UpdateSingleAccount
        ///</summary>
        [TestMethod()]
        public void UpdateSingleAccountTest()
        {
            Account updatedAccount = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = AccountAccess.UpdateSingleAccount(updatedAccount, personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateSingleAccount
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNonExsistentAccountTest()
        {
            Account updatedAccount = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = AccountAccess.UpdateSingleAccount(updatedAccount, personId);
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
