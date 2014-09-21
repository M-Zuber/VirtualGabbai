using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LocalTypes;
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
                var newPerson = t_people.Createt_people(1);
                newPerson.address = "12;" + 1 + 1 + ";main st;anywhere;anystate;usa;12345";
                newPerson.email = 1 + "@something.somewhere";
                newPerson.family_name = "Doe";
                newPerson.given_name = "Jack/Jane";
                newPerson.member = true;
                Cache.CacheData.t_people.AddObject(newPerson);
            }
            if (!Cache.CacheData.t_accounts.Any(account => account.C_id == 1))
            {
                var newAccount = t_accounts.Createt_accounts(1, 1);
                newAccount.monthly_total = 0;
                newAccount.last_month_paid = DateTime.Today;
                Cache.CacheData.t_accounts.AddObject(newAccount);
            }
            for (int newDonationIndex = 1; newDonationIndex <= 5; newDonationIndex++)
            {
                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
                {
                    var newDonation = t_donations.Createt_donations(
                                newDonationIndex, 1, "reason:" + newDonationIndex, 12.5, DateTime.Today, false);
                    Cache.CacheData.t_donations.AddObject(newDonation); 
                }
            }
            for (int newDonationIndex = 6; newDonationIndex <= 10; newDonationIndex++)
            {
                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
                {
                    var newDonation = t_donations.Createt_donations(
                                newDonationIndex, 1, "reason:" + newDonationIndex, 12.5, DateTime.Today, true);
                    newDonation.date_paid = DateTime.Today;
                    Cache.CacheData.t_donations.AddObject(newDonation); 
                }
            }
            for (int moreDonations = 11; moreDonations < 15; moreDonations++)
            {
                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == moreDonations))
                {
                    var newDonation = t_donations.Createt_donations(
                                moreDonations, 1, "reason:" + moreDonations, 12.5, DateTime.Today, false);
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
            Cache.CacheData.clear_database();
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
            int accountId = 1;
            List<Donation> newDonationList = new List<Donation>();

            for (int i = 15; i <= 20; i++)
            {
                Donation newDonation = new Donation(i, "reason:" + i.ToString(), 12.5, DateTime.Today, "comment");
                newDonationList.Add(newDonation);
            }
            DonationAccess.AddMultipleNewDonations(newDonationList, accountId);

            List<Donation> actual = new List<Donation>();
            for (int i = 15; i <= 20; i++)
            {
                actual.Add(DonationAccess.GetDonationById(i));
            }

            CollectionAssert.AreEqual(newDonationList, actual);
        }

        /// <summary>
        ///A test for AddNewDonation
        ///</summary>
        [TestMethod()]
        public void AddNewDonationTest()
        {
            int accountId = 1;
            Donation newDonation = new Donation(21,"reason:21", 12.5, DateTime.Today, "comment");
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.AddNewDonation(newDonation, accountId);
            Assert.AreEqual(expected, actual);
            Donation addedDonation = DonationAccess.GetDonationById(21);
            Assert.AreEqual(newDonation, addedDonation);
        }

        /// <summary>
        ///A test for AddNewDonation
        ///</summary>
        [TestMethod()]
        public void AddNewPaidDonationTest()
        {
            int accountId = 1;
            PaidDonation newDonation = new PaidDonation(22, "reason:21", 12.5, DateTime.Today, "comment", DateTime.Today);
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.AddNewDonation(newDonation, accountId);
            Assert.AreEqual(expected, actual);
            Donation addedDonation = DonationAccess.GetDonationById(22);
            Assert.AreEqual(newDonation, addedDonation);
        }
        
        #endregion

        #region Conversion Tests

        /// <summary>
        ///A test for ConvertMultipleDbDonationsToLocalType
        ///</summary>
        [TestMethod()]
        
        public void ConvertMultipleDbDonationsToLocalTypeTest()
        {
            List<t_donations> dbTypeDonationList = new List<t_donations>()
            {
                t_donations.Createt_donations(1,1,"reason:1", 12.5, DateTime.Today, false),
                t_donations.Createt_donations(2,1,"reason:2", 12.5, DateTime.Today, true)
            };
            List<Donation> expected = new List<Donation>()
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, ""),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "", DateTime.Today)
            };
            List<Donation> actual;
            actual = DonationAccess_Accessor.ConvertMultipleDbDonationsToLocalType(dbTypeDonationList);
            CollectionAssert.AreEqual(expected, actual);
            Assert.IsInstanceOfType(actual[0], typeof(Donation));
            Assert.IsInstanceOfType(actual[1], typeof(PaidDonation));
        }

        /// <summary>
        ///A test for ConvertMultipleLocalDonationssToDbType
        ///</summary>
        [TestMethod()]
        
        public void ConvertMultipleLocalDonationssToDbTypeTest()
        {
            List<Donation> localTypeDonationList = new List<Donation>()
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, ""),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "", DateTime.Today)
            };
            List<t_donations> expected = new List<t_donations>()
            {
                t_donations.Createt_donations(1,1,"reason:1", 12.5, DateTime.Today, false),
                t_donations.Createt_donations(2,1,"reason:2", 12.5, DateTime.Today, true)
            };
            expected[0].comments = "";
            expected[1].comments = "";
            expected[1].date_paid = DateTime.Today;
            List<t_donations> actual;
            actual = DonationAccess_Accessor.ConvertMultipleLocalDonationssToDbType(localTypeDonationList, 1);
            Assert.AreEqual(expected[0].account_id, actual[0].account_id);
            Assert.AreEqual(expected[0].amount, actual[0].amount);
            Assert.AreEqual(expected[0].C_id, actual[0].C_id);
            Assert.AreEqual(expected[0].comments, actual[0].comments);
            Assert.AreEqual(expected[0].date_donated, actual[0].date_donated);
            Assert.AreEqual(expected[0].date_paid, actual[0].date_paid);
            Assert.AreEqual(expected[0].paid, actual[0].paid);
            Assert.AreEqual(expected[0].reason, actual[0].reason);

            Assert.AreEqual(expected[1].account_id, actual[1].account_id);
            Assert.AreEqual(expected[1].amount, actual[1].amount);
            Assert.AreEqual(expected[1].C_id, actual[1].C_id);
            Assert.AreEqual(expected[1].comments, actual[1].comments);
            Assert.AreEqual(expected[1].date_donated, actual[1].date_donated);
            Assert.AreEqual(expected[1].date_paid, actual[1].date_paid);
            Assert.AreEqual(expected[1].paid, actual[1].paid);
            Assert.AreEqual(expected[1].reason, actual[1].reason);
        }

        /// <summary>
        ///A test for ConvertSingleDbDonationToLocalType
        ///</summary>
        [TestMethod()]
        
        public void ConvertSingleUnpaidDbDonationToLocalTypeTest()
        {
            t_donations dbTypeDoantions = t_donations.Createt_donations(1,1,"reason:1", 12.5, DateTime.Today, false);
            Donation expected = new Donation(1, "reason:1", 12.5, DateTime.Today, "");
            Donation actual = DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(dbTypeDoantions);
            Assert.IsInstanceOfType(actual, typeof(Donation));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleDbDonationToLocalType
        ///</summary>
        [TestMethod()]
        
        public void ConvertSinglePaidDbDonationToLocalTypeTest()
        {
            t_donations dbTypeDoantions = t_donations.Createt_donations(1, 1, "reason:1", 12.5, DateTime.Today, true);
            dbTypeDoantions.date_paid = DateTime.Today;
            PaidDonation expected = new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "",DateTime.Today);
            Donation actual = DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(dbTypeDoantions);
            Assert.IsInstanceOfType(actual, typeof(PaidDonation));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertSingleLocalDonationToDbType
        ///</summary>
        [TestMethod()]
        
        public void ConvertSingleLocalDonationToDbTypeTest()
        {
            Donation localTypeDonation = new Donation(1, "reason:1", 12.5, DateTime.Today, "comment");
            t_donations expected = t_donations.Createt_donations(1,1, "reason:1", 12.5, DateTime.Today, false);
            expected.comments = "comment";
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
        
        public void ConvertSinglePaidLocalDonationToDbTypeTest()
        {
            PaidDonation localTypeDonation = new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "comment", DateTime.Today);
            t_donations expected = t_donations.Createt_donations(1, 1, "reason:1", 12.5, DateTime.Today, true);
            expected.date_paid = DateTime.Today;
            expected.comments = "comment";
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
            int accountId = 1;
            List<Donation> deletedDonationList = new List<Donation>()
            {
                new Donation(2,"reason:2", 12.5, DateTime.Today, ""),
                new PaidDonation(10, "reason:10", 12.5, DateTime.Today, "", DateTime.Today)
            };
            DonationAccess.DeleteMultipleDonations(deletedDonationList, accountId);

            List<Donation> allDonations = DonationAccess.GetAllDonations(accountId);

            for (int i = 0; i < allDonations.Count; i++)
            {
                if (!(allDonations[i] is PaidDonation))
                {
                    Assert.AreNotEqual(allDonations[i], deletedDonationList[0]);
                    Assert.AreNotEqual(allDonations[i], deletedDonationList[1]);
                }
                else
                {
                    Assert.AreNotEqual(allDonations[i], deletedDonationList[1]);
                }
            }
        }

        /// <summary>
        ///A test for DeleteSingleDonation
        ///</summary>
        [TestMethod()]
        public void DeleteSingleDonationTest()
        {
            int accountId = 1;
            Donation deleteDonation = new Donation(4,"reason:4", 12.5, DateTime.Today, "");
            Enums.CRUDResults expected = Enums.CRUDResults.DELETE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.DeleteSingleDonation(deleteDonation, accountId);
            List<Donation> allDonations = DonationAccess.GetAllDonations(accountId);
            Assert.AreEqual(expected, actual);
            for (int i = 0; i < allDonations.Count; i++)
            {
                if (!(allDonations[i] is PaidDonation))
                {
                    Assert.AreNotEqual(allDonations[i], deleteDonation);
                } 
            }
        }

        /// <summary>
        ///A test for DeleteSingleDonation
        ///</summary>
        [TestMethod()]
        public void DeleteSingleNonExistentDonationTest()
        {
            int accountId = 1;
            Donation deleteDonation = new Donation(50, "", 12, DateTime.Today, "");
            Enums.CRUDResults expected = Enums.CRUDResults.DELETE_FAIL;
            Enums.CRUDResults actual;
            actual = DonationAccess.DeleteSingleDonation(deleteDonation, accountId);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Get Tests

        /// <summary>
        ///A test for GetAllDonations
        ///</summary>
        [TestMethod()]
        
        public void GetAllDonationsTest()
        {
            int accountId = 1;
            List<Donation> expected = 
                DonationAccess_Accessor.ConvertMultipleDbDonationsToLocalType(
                                    DonationAccess_Accessor.LookupAllDonations(accountId));
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetAllDonations(accountId);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAllDonations
        ///</summary>
        [TestMethod()]
        
        public void GetAllDonationsOfNonExistentAccountTest()
        {
            int accountId = 50;
            List<Donation> actual = DonationAccess_Accessor.GetAllDonations(accountId);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetByDonationDate
        ///</summary>
        [TestMethod()]
        
        public void GetByDonationDateTest()
        {
            DateTime donationDate = DateTime.Today;
            List<Donation> expected = DonationAccess_Accessor.ConvertMultipleDbDonationsToLocalType(
                DonationAccess_Accessor.LookupByDonationDate(donationDate));
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByDonationDate(donationDate);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByDonationDate
        ///</summary>
        [TestMethod()]
        
        public void GetByNonExistentDonationDateTest()
        {
            DateTime donationDate = DateTime.MinValue;
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByDonationDate(donationDate);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetByPaymentDate
        ///</summary>
        [TestMethod()]
        
        public void GetByPaymentDateTest()
        {
            DateTime paymentDate = DateTime.Today;
            List<Donation> expected = 
                DonationAccess_Accessor.ConvertMultipleDbDonationsToLocalType(
                              DonationAccess_Accessor.LookupByPaymentDate(paymentDate));
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByPaymentDate(paymentDate);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByPaymentDate
        ///</summary>
        [TestMethod()]
        
        public void GetByNonExistentPaymentDateTest()
        {
            DateTime paymentDate = DateTime.MinValue;
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByPaymentDate(paymentDate);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetByReason
        ///</summary>
        [TestMethod()]
        
        public void GetByReasonTest()
        {
            string reason = "reason:1";
            List<Donation> expected = 
                DonationAccess_Accessor.ConvertMultipleDbDonationsToLocalType(
                    DonationAccess_Accessor.LookupByReason(reason));
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByReason(reason);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetByReason
        ///</summary>
        [TestMethod()]
        
        public void GetByNonExistentReasonTest()
        {
            string reason = "reason";
            List<Donation> actual;
            actual = DonationAccess_Accessor.GetByReason(reason);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetDonationById
        ///</summary>
        [TestMethod()]
        
        public void GetDonationByIdTest()
        {
            int id = 1;
            Donation expected =
                DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(
                    DonationAccess_Accessor.LookupDonationById(id));
            Donation actual;
            actual = DonationAccess_Accessor.GetDonationById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetDonationById
        ///</summary>
        [TestMethod()]
        
        public void GetDonationByNonExistentIdTest()
        {
            int id = 0;
            Donation expected = null;
            Donation actual;
            actual = DonationAccess_Accessor.GetDonationById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificDonation
        ///</summary>
        [TestMethod()]
        
        public void GetSpecificDonationTest()
        {
            string reason = "reason:1";
            double amount = 12.5;
            DateTime donationDate = DateTime.Today;
            Donation expected = 
                DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(
                    DonationAccess_Accessor.LookupSpecificDonation(reason, amount, donationDate));
            Donation actual;
            actual = DonationAccess_Accessor.GetSpecificDonation(reason, amount, donationDate);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetSpecificDonation
        ///</summary>
        [TestMethod()]
        
        public void GetSpecificNonExistentDonationTest()
        {
            string reason = string.Empty;
            double amount = 0F;
            DateTime donationDate = new DateTime();
            Donation expected = null;
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
        
        public void LookupAllDonationsTest()
        {
            int accountId = 1;
            List<t_donations> expected = 
                (from CurrDonation in Cache.CacheData.t_donations select CurrDonation).ToList<t_donations>(); 
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupAllDonations(accountId);

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue((expected[i].account_id == accountId) && (expected.Contains(actual[i])));
            }
        }

        /// <summary>
        ///A test for LookupAllDonations
        ///</summary>
        [TestMethod()]
        
        public void LookupAllDonationsOfNonExistentAccountTest()
        {
            int accountId = 0;
            List<t_donations> expected = null;
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupAllDonations(accountId);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupByDonationDate
        ///</summary>
        [TestMethod()]
        
        public void LookupByDonationDateTest()
        {
            DateTime donationDate = DateTime.Today;
            List<t_donations> expected = 
                (from CurrDonation in Cache.CacheData.t_donations select CurrDonation).ToList<t_donations>();
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByDonationDate(donationDate);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue((expected[i].date_donated == donationDate) && (expected.Contains(actual[i])));
            }
        }

        /// <summary>
        ///A test for LookupByDonationDate
        ///</summary>
        [TestMethod()]
        
        public void LookupByNonExistentDonationDateTest()
        {
            DateTime donationDate = DateTime.MinValue;
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByDonationDate(donationDate);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for LookupByPaymentDate
        ///</summary>
        [TestMethod()]
        
        public void LookupByPaymentDateTest()
        {
            DateTime paymentDate = DateTime.Today;
            List<t_donations> expected = 
                (from CurrDonation in Cache.CacheData.t_donations select CurrDonation).ToList<t_donations>();
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByPaymentDate(paymentDate);
            for (int i = 0; i < expected.Count; i++)
            {
                if (expected[i].date_paid == paymentDate)
                {
                    Assert.IsTrue(actual.Contains(expected[i])); 
                }
            }
        }

        /// <summary>
        ///A test for LookupByPaymentDate
        ///</summary>
        [TestMethod()]
        
        public void LookupByNonExistentPaymentDateTest()
        {
            DateTime paymentDate = DateTime.MinValue;
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByPaymentDate(paymentDate);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for LookupByReason
        ///</summary>
        [TestMethod()]
        
        public void LookupByReasonTest()
        {
            string reason = "reason:1";
            List<t_donations> expected = new List<t_donations>() 
            {
                t_donations.Createt_donations(1,1,"reason:1", 12.5, DateTime.Today, false)
            };
            expected[0].comments = "";
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByReason(reason);
            Assert.AreEqual(expected[0].account_id, actual[0].account_id);
            Assert.AreEqual(expected[0].amount, actual[0].amount);
            Assert.AreEqual(expected[0].C_id, actual[0].C_id);
            Assert.AreEqual(expected[0].comments, actual[0].comments);
            Assert.AreEqual(expected[0].date_donated, actual[0].date_donated);
            Assert.AreEqual(expected[0].date_paid, actual[0].date_paid);
            Assert.AreEqual(expected[0].paid, actual[0].paid);
            Assert.AreEqual(expected[0].reason, actual[0].reason);
        }

        /// <summary>
        ///A test for LookupByReason
        ///</summary>
        [TestMethod()]
        
        public void LookupByNonExistentReasonTest()
        {
            string reason = "reason";
            List<t_donations> actual;
            actual = DonationAccess_Accessor.LookupByReason(reason);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for LookupDonationById
        ///</summary>
        [TestMethod()]
        
        public void LookupDonationByIdTest()
        {
            int idUnpaid = 1;
            t_donations expectedUnpaid = t_donations.Createt_donations(1,1,"reason:1", 12.5, DateTime.Today, false);
            t_donations actualUnpaid = DonationAccess_Accessor.LookupDonationById(idUnpaid);
            Assert.AreEqual(DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(expectedUnpaid),
                            DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(actualUnpaid));
            int idPpaid = 7;
            t_donations expectedPaid = t_donations.Createt_donations(7, 1, "reason:7", 12.5, DateTime.Today, true);
            expectedPaid.date_paid = DateTime.Today;
            t_donations actualPaid = DonationAccess_Accessor.LookupDonationById(idPpaid);
            Assert.AreEqual(DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(expectedPaid),
                             DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(actualPaid));
        }

        /// <summary>
        ///A test for LookupDonationById
        ///</summary>
        [TestMethod()]
        
        public void LookupDonationByNonExistentIdTest()
        {
            int id = 0;
            t_donations expected = null;
            t_donations actual;
            actual = DonationAccess_Accessor.LookupDonationById(id);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LookupSpecificDonation
        ///</summary>
        [TestMethod()]
        
        public void LookupSpecificDonationTest()
        {
            string reason = "reason:1";
            double amount = 12.5;
            DateTime donationDate = DateTime.Today;
            t_donations expected = t_donations.Createt_donations(1,1,"reason:1", 12.5, DateTime.Today, false);
            t_donations actual;
            actual = DonationAccess_Accessor.LookupSpecificDonation(reason, amount, donationDate);
            Assert.AreEqual(DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(expected),
                           DonationAccess_Accessor.ConvertSingleDbDonationToLocalType(actual));
        }

        /// <summary>
        ///A test for LookupSpecificDonation
        ///</summary>
        [TestMethod()]
        
        public void LookupSpecificNonExistentDonationTest()
        {
            string reason = "reason:1";
            double amount = 0F;
            DateTime donationDate = DateTime.Today;
            t_donations expected = null;
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
            int accountId = 1;
            List<Donation> updatedDonationList = new List<Donation>()
            {
                new Donation(5, "updated reason:5", 12.5, DateTime.Today, ""),
                new PaidDonation(6, "updated reason:6", 12.5, DateTime.Today, "", DateTime.Today)
            };
            DonationAccess.UpdateMultipleDonations(updatedDonationList, accountId);

            List<Donation> actual = new List<Donation>()
            {
                DonationAccess.GetDonationById(5),
                DonationAccess.GetDonationById(6)
            };

            CollectionAssert.AreEqual(updatedDonationList, actual);
        }

        /// <summary>
        ///A test for UpdateSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpdateSingleDonationTest()
        {
            int accountId = 1;
            PaidDonation updatedDonation = new PaidDonation(8, "updated reason:8", 12.5, DateTime.Today, "comment", DateTime.Today);
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.UpdateSingleDonation(updatedDonation, accountId);
            Donation actualDonation = DonationAccess.GetDonationById(8);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(updatedDonation, actualDonation);
        }

        /// <summary>
        ///A test for UpdateSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpdateByTurningToPaidSingleDonationTest()
        {
            int accountId = 1;
            Donation updatedDonation = new Donation(3, "reason:3", 12.5, DateTime.Today, "its now paid");
            PaidDonation donationPaid = new PaidDonation(updatedDonation, DateTime.Today);
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.UpdateSingleDonation(donationPaid, accountId);
            Donation actualDonation = DonationAccess.GetDonationById(3);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(updatedDonation, actualDonation);
        }

        /// <summary>
        ///A test for UpdateSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpdateSingleNonExistentDonationTest()
        {
            int accountId = 1;
            Donation updatedDonation = new Donation(50, "diff reason", 25, DateTime.MinValue, "");
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_FAIL;
            Enums.CRUDResults actual;
            actual = DonationAccess.UpdateSingleDonation(updatedDonation, accountId);
            Assert.AreEqual(expected, actual);
        }
        
        #endregion

        #region Upsert Tests

        /// <summary>
        ///A test for UpsertSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpsertAddSingleDonationTest()
        {
            Donation upsertedDonation = new Donation(613, "reason:613", 87, DateTime.Today, "");
            int personId = 1;
            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.UpsertSingleDonation(upsertedDonation, personId);
            Assert.AreEqual(expected, actual);
            Donation afterUpsert = DonationAccess.GetDonationById(613);
            Assert.AreEqual(upsertedDonation, afterUpsert);
        }

        /// <summary>
        ///A test for UpsertSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpsertUpdateSingleDonationTest()
        {
            Donation upsertedDonation = new Donation(11, "the other reason", 35, DateTime.Today, "");
            int personId = 1;
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.UpsertSingleDonation(upsertedDonation, personId);
            Assert.AreEqual(expected, actual);
            Donation afterUpsert = DonationAccess.GetDonationById(11);
            Assert.AreEqual(upsertedDonation, afterUpsert);
        }

        /// <summary>
        ///A test for UpsertSingleDonation
        ///</summary>
        [TestMethod()]
        public void UpsertUpdateToPaidSingleDonationTest()
        {
            PaidDonation upsertedDonation = new PaidDonation(DonationAccess.GetDonationById(12), DateTime.Today);
            int personId = 1;
            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
            Enums.CRUDResults actual;
            actual = DonationAccess.UpsertSingleDonation(upsertedDonation, personId);
            Assert.AreEqual(expected, actual);
            Donation afterUpsert = DonationAccess.GetDonationById(12);
            Assert.IsTrue(afterUpsert is PaidDonation);
            Assert.AreEqual(upsertedDonation, afterUpsert);
        }
        
        #endregion
    }
}
