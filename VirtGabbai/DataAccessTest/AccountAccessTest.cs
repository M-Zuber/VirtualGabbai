using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using Framework;
using DataCache;

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
        ///A test for ConvertMultipleDbAccountsToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleDbAccountsToLocalTypeTest()
        {
            List<t_accounts> dbTypeAccountList = null; // TODO: Initialize to an appropriate value
            List<Account> expected = null; // TODO: Initialize to an appropriate value
            List<Account> actual;
            actual = AccountAccess_Accessor.ConvertMultipleDbAccountsToLocalType(dbTypeAccountList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleLocalAccountsToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleLocalAccountsToDbTypeTest()
        {
            List<Account> localTypeAccountList = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            List<t_accounts> expected = null; // TODO: Initialize to an appropriate value
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.ConvertMultipleLocalAccountsToDbType(localTypeAccountList, personId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbAccountToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleDbAccountToLocalTypeTest()
        {
            t_accounts dbTypeAccount = null; // TODO: Initialize to an appropriate value
            Account expected = null; // TODO: Initialize to an appropriate value
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
            Account localTypeAccount = null; // TODO: Initialize to an appropriate value
            int personId = 0; // TODO: Initialize to an appropriate value
            t_accounts expected = null; // TODO: Initialize to an appropriate value
            t_accounts actual;
            actual = AccountAccess_Accessor.ConvertSingleLocalAccountToDbType(localTypeAccount, personId);
            Assert.AreEqual(expected, actual);
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
        ///A test for GetByDonation
        ///</summary>
        [TestMethod()]
        public void GetByDonationTest()
        {
            Donation donationToLookBy = null; // TODO: Initialize to an appropriate value
            List<Account> expected = null; // TODO: Initialize to an appropriate value
            List<Account> actual;
            actual = AccountAccess.GetByDonation(donationToLookBy);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonation
        ///</summary>
        [TestMethod()]
        public void GetByDonationTest1()
        {
            int donationId = 0; // TODO: Initialize to an appropriate value
            List<Account> expected = null; // TODO: Initialize to an appropriate value
            List<Account> actual;
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
            double monthlyTotal = 0F; // TODO: Initialize to an appropriate value
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

        #endregion
        #region Lookup Tests
        /// <summary>
        ///A test for LookupAllAccounts
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllAccountsTest()
        {
            List<t_accounts> expected = null; // TODO: Initialize to an appropriate value
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.LookupAllAccounts();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByAccountId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByAccountIdTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            t_accounts expected = null; // TODO: Initialize to an appropriate value
            t_accounts actual;
            actual = AccountAccess_Accessor.LookupByAccountId(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByDonationTest()
        {
            int donationId = 0; // TODO: Initialize to an appropriate value
            List<t_accounts> expected = null; // TODO: Initialize to an appropriate value
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.LookupByDonation(donationId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByDonationTest1()
        {
            Donation donationToLookBy = null; // TODO: Initialize to an appropriate value
            List<t_accounts> expected = null; // TODO: Initialize to an appropriate value
            List<t_accounts> actual;
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
            DateTime lastPayment = new DateTime(); // TODO: Initialize to an appropriate value
            List<t_accounts> expected = null; // TODO: Initialize to an appropriate value
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.LookupByLastMonthlyPaymentDate(lastPayment);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByMonthlyPaymentTotal
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByMonthlyPaymentTotalTest()
        {
            double monthlyTotal = 0F; // TODO: Initialize to an appropriate value
            List<t_accounts> expected = null; // TODO: Initialize to an appropriate value
            List<t_accounts> actual;
            actual = AccountAccess_Accessor.LookupByMonthlyPaymentTotal(monthlyTotal);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPersonId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPersonIdTest()
        {
            int personId = 0; // TODO: Initialize to an appropriate value
            t_accounts expected = null; // TODO: Initialize to an appropriate value
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
        #endregion
    }
}
