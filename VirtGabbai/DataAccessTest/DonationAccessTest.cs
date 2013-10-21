using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataTypes;
using System.Collections.Generic;
using Framework;
using DataCache;
using System.Linq;

namespace DataAccessTest
{
    
    
    /// <summary>
    ///This is a test class for DonationAccessTest and is intended
    ///to contain all DonationAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DonationAccessTest
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
            for (int newDonationIndex = 1; newDonationIndex <= 10; newDonationIndex++)
            {
                var newDonation = t_donations.Createt_donations(
                    newDonationIndex, 1, "reason:" + newDonationIndex, 12.5, DateTime.Today, false);
                Cache.CacheData.t_donations.AddObject(newDonation);
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
        ///A test for AddMultipleNewDonations
        ///</summary>
        [TestMethod()]
        public void AddMultipleNewDonationsTest()
        {
            List<Donation> newDonationList = null; // TODO: Initialize to an appropriate value
            DonationAccess.AddMultipleNewDonations(newDonationList);
        }

        /// <summary>
        ///A test for AddNewDonation
        ///</summary>
        [TestMethod()]
        public void AddNewDonationTest()
        {
            Donation newDonation = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = DonationAccess.AddNewDonation(newDonation);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConvertMultipleDbDonationsToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleDbDonationsToLocalTypeTest()
        {
            List<t_donations> dbTypeDonationList = null; // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.ConvertMultipleDbDonationsToLocalType(dbTypeDonationList);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertMultipleLocalDonationssToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertMultipleLocalDonationssToDbTypeTest()
        {
            List<Donation> localTypeDonationList = null; // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.ConvertMultipleLocalDonationssToDbType(localTypeDonationList, 1);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbDonationToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleUnpaidDbDonationToLocalTypeTest()
        {
            t_donations dbTypeDoantions = t_donations.Createt_donations(1,1,"reason:1", 12.5, DateTime.Today, false);
            Donation expected = new Donation(1, "reason:1", 12.5, DateTime.Today, "");
            Donation actual = DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(dbTypeDoantions);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbDonationToLocalType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSinglePaidDbDonationToLocalTypeTest()
        {
            t_donations dbTypeDoantions = t_donations.Createt_donations(1, 1, "reason:1", 12.5, DateTime.Today, false);
            dbTypeDoantions.paid = true;
            dbTypeDoantions.date_paid = DateTime.Today;
            Donation expected = new Donation(1, "reason:1", 12.5, DateTime.Today, DateTime.Today, "");
            Donation actual = DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(dbTypeDoantions);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalDonationToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSingleLocalDonationToDbTypeTest()
        {
            Donation localTypeDonation = new Donation(1, "reason:1", 12.5, DateTime.Today, "comment");
            t_donations expected = t_donations.Createt_donations(1,1, "reason:1", 12.5, DateTime.Today, false);
            t_donations actual;
            actual = DonationAccess_Accessor.ConvertSingleLocalDonationToDbType(localTypeDonation, 1);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.reason, actual.reason);
            Assert.AreEqual(expected.paid, actual.paid);
            Assert.AreEqual(expected.account_id, actual.account_id);
            Assert.AreEqual(expected.amount, actual.amount);
            Assert.AreEqual(expected.comments, actual.comments);
            Assert.AreEqual(expected.date_donated, actual.date_donated);
            Assert.AreEqual(expected.date_paid, actual.date_paid);            
        }

        /// <summary>
        ///A test for ConvertSingleLocalDonationToDbType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void ConvertSinglePaidLocalDonationToDbTypeTest()
        {
            Donation localTypeDonation = new Donation(1, "reason:1", 12.5, DateTime.Today, DateTime.Today, "comment");
            t_donations expected = t_donations.Createt_donations(1, 1, "reason:1", 12.5, DateTime.Today, true);
            expected.date_paid = DateTime.Today;
            t_donations actual;
            actual = DonationAccess_Accessor.ConvertSingleLocalDonationToDbType(localTypeDonation, 1);
            Assert.AreEqual(expected.C_id, actual.C_id);
            Assert.AreEqual(expected.reason, actual.reason);
            Assert.AreEqual(expected.paid, actual.paid);
            Assert.AreEqual(expected.account_id, actual.account_id);
            Assert.AreEqual(expected.amount, actual.amount);
            Assert.AreEqual(expected.comments, actual.comments);
            Assert.AreEqual(expected.date_donated, actual.date_donated);
            Assert.AreEqual(expected.date_paid, actual.date_paid);
        }
        
        #endregion

        #region Delete Tests

        /// <summary>
        ///A test for DeleteMultipleDonations
        ///</summary>
        [TestMethod()]
        public void DeleteMultipleDonationsTest()
        {
            List<Donation> deletedDonationList = null; // TODO: Initialize to an appropriate value
            DonationAccess.DeleteMultipleDonations(deletedDonationList);
        }

        /// <summary>
        ///A test for DeleteSingleDonation
        ///</summary>
        [TestMethod()]
        public void DeleteSingleDonationTest()
        {
            Donation deleteDonation = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = DonationAccess.DeleteSingleDonation(deleteDonation);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DeleteSingleDonation
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExistentDonationTest()
        {
            Donation deleteDonation = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = DonationAccess.DeleteSingleDonation(deleteDonation);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetAllDonationsTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetAllDonations(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAllDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetAllDonationsOfNonExistentAccountTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetAllDonations(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonationDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetByDonationDateTest()
        {
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByDonationDate(donationDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonationDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetByNonExistentDonationDateTest()
        {
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByDonationDate(donationDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPaymentDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetByPaymentDateTest()
        {
            DateTime paymentDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByPaymentDate(paymentDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPaymentDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetByNonExistentPaymentDateTest()
        {
            DateTime paymentDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByPaymentDate(paymentDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByReason
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetByReasonTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByReason(reason);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByReason
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetByNonExistentReasonTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            List<Donation> expected = null; // TODO: Initialize to an appropriate value
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByReason(reason);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDonationById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetDonationByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            Donation expected = null; // TODO: Initialize to an appropriate value
            Donation actual;
            actual = DonationAccess_Accessor.GetDonationById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDonationById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetDonationByNonExistentIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            Donation expected = null; // TODO: Initialize to an appropriate value
            Donation actual;
            actual = DonationAccess_Accessor.GetDonationById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetSpecificDonationTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            double amount = 0F; // TODO: Initialize to an appropriate value
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            Donation expected = null; // TODO: Initialize to an appropriate value
            Donation actual;
            actual = DonationAccess_Accessor.GetSpecificDonation(reason, amount, donationDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void GetSpecificNonExistentDonationTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            double amount = 0F; // TODO: Initialize to an appropriate value
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            Donation expected = null; // TODO: Initialize to an appropriate value
            Donation actual;
            actual = DonationAccess_Accessor.GetSpecificDonation(reason, amount, donationDate);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Lookup Tests

        /// <summary>
        ///A test for LookupAllDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllDonationsTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupAllDonations(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupAllDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupAllDonationsOfNonExistentAccountTest()
        {
            int accountId = 0; // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupAllDonations(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonationDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByDonationDateTest()
        {
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByDonationDate(donationDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonationDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExistentDonationDateTest()
        {
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByDonationDate(donationDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPaymentDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByPaymentDateTest()
        {
            DateTime paymentDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByPaymentDate(paymentDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByPaymentDate
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExistentPaymentDateTest()
        {
            DateTime paymentDate = new DateTime(); // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByPaymentDate(paymentDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByReason
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByReasonTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByReason(reason);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByReason
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupByNonExistentReasonTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            List<t_donations> expected = null; // TODO: Initialize to an appropriate value
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByReason(reason);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupDonationById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupDonationByIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            t_donations expected = null; // TODO: Initialize to an appropriate value
            t_donations actual;
            actual = DonationAccess_Accessor.LookupDonationById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupDonationById
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupDonationByNonExistentIdTest()
        {
            int id = 0; // TODO: Initialize to an appropriate value
            t_donations expected = null; // TODO: Initialize to an appropriate value
            t_donations actual;
            actual = DonationAccess_Accessor.LookupDonationById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupSpecificDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificDonationTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            double amount = 0F; // TODO: Initialize to an appropriate value
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            t_donations expected = null; // TODO: Initialize to an appropriate value
            t_donations actual;
            actual = DonationAccess_Accessor.LookupSpecificDonation(reason, amount, donationDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupSpecificDonation
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataAccess.dll")]
        public void LookupSpecificNonExistentDonationTest()
        {
            string reason = string.Empty; // TODO: Initialize to an appropriate value
            double amount = 0F; // TODO: Initialize to an appropriate value
            DateTime donationDate = new DateTime(); // TODO: Initialize to an appropriate value
            t_donations expected = null; // TODO: Initialize to an appropriate value
            t_donations actual;
            actual = DonationAccess_Accessor.LookupSpecificDonation(reason, amount, donationDate);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Update Tests

        /// <summary>
        ///A test for UpdateMultipleDonations
        ///</summary>
        [TestMethod()]
        public void UpdateMultipleDonationsTest()
        {
            List<Donation> updatedDonationList = null; // TODO: Initialize to an appropriate value
            DonationAccess.UpdateMultipleDonations(updatedDonationList);
        }

        /// <summary>
        ///A test for UpdateSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpdateSingleDonationTest()
        {
            Donation updatedDonation = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = DonationAccess.UpdateSingleDonation(updatedDonation);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for UpdateSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNonExistentDonationTest()
        {
            Donation updatedDonation = null; // TODO: Initialize to an appropriate value
            Enums.CRUDResults expected = new Enums.CRUDResults(); // TODO: Initialize to an appropriate value
            Enums.CRUDResults actual;
            actual = DonationAccess.UpdateSingleDonation(updatedDonation);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
