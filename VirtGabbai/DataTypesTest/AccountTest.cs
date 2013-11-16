using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LocalTypesTest
{
    
    
    /// <summary>
    ///This is a test class for AccountTest and is intended
    ///to contain all AccountTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AccountTest
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

        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffrencesInDonationsEqualsTest()
        {
            int id = 1;
            int monthlyPaymentTotal = 12;
            DateTime lastMonthlyPaymentDate = DateTime.Today;
            List<Donation> allDonations = new List<Donation>() 
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2,"reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3,"reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4,"reason:4", 12.5, DateTime.Today, "comment"),
                new Donation(5,"reason:5", 12.5, DateTime.Today, "comment")
            };
            
            Account target = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            allDonations.Add(new PaidDonation(6, "reason:6", 12.5, DateTime.Today, "comment", DateTime.Today));
            allDonations.Add(new PaidDonation(7, "reason:7", 12.5, DateTime.Today, "comment", DateTime.Today));
            allDonations.Add(new PaidDonation(8, "reason:8", 12.5, DateTime.Today, "comment", DateTime.Today));
            List<Donation> otherDonations = new List<Donation>();
            otherDonations.AddRange(allDonations);
            otherDonations.Add(new PaidDonation(34, "reason:6", 12.5, DateTime.Today, "comment", DateTime.Today));
            otherDonations.Add(new PaidDonation(7, "reason:7", 12.5, DateTime.Today, "comment", DateTime.Today));
            otherDonations.Add(new PaidDonation(56, "reason:8", 12.5, DateTime.Today, "comment", DateTime.Today));
            object obj = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, otherDonations);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffLastMonthlyPaymentDateEqualsTest()
        {
            int id = 1;
            int monthlyPaymentTotal = 12;
            DateTime lastMonthlyPaymentDate = DateTime.Today;
            List<Donation> allDonations = new List<Donation>() 
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2,"reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3,"reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4,"reason:4", 12.5, DateTime.Today, "comment"),
                new Donation(5,"reason:5", 12.5, DateTime.Today, "comment")
            };
            Account target = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            object obj = new Account(id, monthlyPaymentTotal, DateTime.MaxValue, allDonations);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffMonthlyPaymentTotalEqualsTest()
        {
            int id = 1;
            int monthlyPaymentTotal = 12;
            DateTime lastMonthlyPaymentDate = DateTime.Today;
            List<Donation> allDonations = new List<Donation>() 
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2,"reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3,"reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4,"reason:4", 12.5, DateTime.Today, "comment"),
                new Donation(5,"reason:5", 12.5, DateTime.Today, "comment")
            };
            Account target = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            object obj = new Account(id, 25, lastMonthlyPaymentDate, allDonations);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffIdEqualsTest()
        {
            int id = 1;
            int monthlyPaymentTotal = 12;
            DateTime lastMonthlyPaymentDate = DateTime.Today;
            List<Donation> allDonations = new List<Donation>() 
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2,"reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3,"reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4,"reason:4", 12.5, DateTime.Today, "comment"),
                new Donation(5,"reason:5", 12.5, DateTime.Today, "comment")
            };
            Account target = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            object obj = new Account(3, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            int id = 1;
            int monthlyPaymentTotal = 12;
            DateTime lastMonthlyPaymentDate = DateTime.Today;
            List<Donation> allDonations = new List<Donation>() 
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2,"reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3,"reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4,"reason:4", 12.5, DateTime.Today, "comment"),
                new Donation(5,"reason:5", 12.5, DateTime.Today, "comment")
            };
            Account target = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            object obj = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            bool expected = true;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllDiffEqualsTest()
        {
            int id = 1;
            int monthlyPaymentTotal = 12;
            DateTime lastMonthlyPaymentDate = DateTime.Today;
            List<Donation> allDonations = new List<Donation>() 
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2,"reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3,"reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4,"reason:4", 12.5, DateTime.Today, "comment"),
                new Donation(5,"reason:5", 12.5, DateTime.Today, "comment")
            };
            Account target = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            allDonations.Add(new PaidDonation(6, "reason:6", 12.5, DateTime.Today, "comment", DateTime.Today));
            allDonations.Add(new PaidDonation(7, "reason:7", 12.5, DateTime.Today, "comment", DateTime.Today));
            allDonations.Add(new PaidDonation(8, "reason:8", 12.5, DateTime.Today, "comment", DateTime.Today));
            object obj = new Account(4, 25, DateTime.MaxValue, allDonations);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        #endregion
        
        #region GetPaidDonations Tests
        
        /// <summary>
        ///A test for GetPaidDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void AllDonationsPaidGetPaidDonationsTest()
        {
            Account_Accessor target = new Account_Accessor(1,1, DateTime.Today, new List<Donation>());
            List<Donation> allDonations = new List<Donation>()
            {
                new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(3, "reason:3", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(4, "reason:4", 12.5, DateTime.Today, "comment", DateTime.Today)
            };
            List<PaidDonation> expected = new List<PaidDonation>() 
            {
                new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(3, "reason:3", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(4, "reason:4", 12.5, DateTime.Today, "comment", DateTime.Today)
            };
            List<PaidDonation> actual;
            actual = target.GetPaidDonations(allDonations);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetPaidDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void SomeDonationsPaidGetPaidDonationsTest()
        {
            Account_Accessor target = new Account_Accessor(1, 1, DateTime.Today, new List<Donation>());
            List<Donation> allDonations = new List<Donation>()
            {
                new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "comment", DateTime.Today),
                new Donation(2, "reason:2", 12.5, DateTime.Today, "comment"),
                new PaidDonation(3, "reason:3", 12.5, DateTime.Today, "comment", DateTime.Today),
                new Donation(4, "reason:4", 12.5, DateTime.Today, "comment")
            };
            List<PaidDonation> actual;
            actual = target.GetPaidDonations(allDonations);
            for (int i = 0; i < allDonations.Count; i++)
            {
                if (allDonations[i] is PaidDonation)
                {
                    Assert.IsTrue(actual.Contains((PaidDonation)allDonations[i]));
                }
            }
        }

        /// <summary>
        ///A test for GetPaidDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void NoDonationsPaidGetPaidDonationsTest()
        {
            Account_Accessor target = new Account_Accessor(1, 1, DateTime.Today, new List<Donation>());
            List<Donation> allDonations = new List<Donation>()
            {
                new Donation(1, "reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2, "reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3, "reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4, "reason:4", 12.5, DateTime.Today, "comment")
            };
            List<PaidDonation> actual;
            actual = target.GetPaidDonations(allDonations);
            Assert.IsTrue(actual.Count == 0);
        }

        #endregion
        
        #region GetUnPaidDonations Tests

        /// <summary>
        ///A test for GetUnpaidDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void NoDonationsUnpaidGetUnpaidDonationsTest()
        {
            Account_Accessor target = new Account_Accessor(1, 1, DateTime.Today, new List<Donation>());
            List<Donation> allDonations = new List<Donation>()
            {
                new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(3, "reason:3", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(4, "reason:4", 12.5, DateTime.Today, "comment", DateTime.Today)
            };
            List<Donation> actual;
            actual = target.GetUnpaidDonations(allDonations);
            Assert.IsTrue(actual.Count == 0);
        }

        /// <summary>
        ///A test for GetUnpaidDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void SomeDonationsUnpaidGetUnpaidDonationsTest()
        {
            Account_Accessor target = new Account_Accessor(1, 1, DateTime.Today, new List<Donation>());
            List<Donation> allDonations = new List<Donation>()
            {
                new Donation(1, "reason:1", 12.5, DateTime.Today, "comment"),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "comment", DateTime.Today),
                new Donation(3, "reason:3", 12.5, DateTime.Today, "comment"),
                new PaidDonation(4, "reason:4", 12.5, DateTime.Today, "comment", DateTime.Today)
            };
            List<Donation> actual;
            actual = target.GetUnpaidDonations(allDonations);

            for (int i = 0; i < allDonations.Count; i++)
            {
                if (allDonations[i] is PaidDonation)
                {
                    Assert.IsFalse(actual.Contains(allDonations[i]));
                } 
                else
                {
                    Assert.IsTrue(actual.Contains(allDonations[i]));
                }
            }
        }

        /// <summary>
        ///A test for GetUnpaidDonations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void AllDonationsUnpaidGetUnpaidDonationsTest()
        {
            Account_Accessor target = new Account_Accessor(1, 1, DateTime.Today, new List<Donation>());
            List<Donation> allDonations = new List<Donation>()
            {
                new Donation(1, "reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2, "reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3, "reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4, "reason:4", 12.5, DateTime.Today, "comment")
            };
            List<Donation> expected = new List<Donation>() 
            {
                new Donation(1, "reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2, "reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3, "reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4, "reason:4", 12.5, DateTime.Today, "comment")
            };
            List<Donation> actual;
            actual = target.GetUnpaidDonations(allDonations);
            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
        
        #region ToString Tests
        
        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            int id = 1;
            int monthlyPaymentTotal = 254;
            DateTime lastMonthlyPaymentDate = DateTime.Today;
            List<Donation> allDonations = new List<Donation>() 
            {
                new Donation(1,"reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2,"reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3,"reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4,"reason:4", 12.5, DateTime.Today, "comment"),
                new Donation(5,"reason:5", 12.5, DateTime.Today, "comment"),
                new PaidDonation(6, "reason:6", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(7, "reason:7", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(8, "reason:8", 12.5, DateTime.Today, "comment", DateTime.Today)
            };
            Account target = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            string donations = "";
            foreach (Donation CurrDonation in target.UnpaidDonations)
            {
                donations += CurrDonation.ToString();
                donations += "\n";
            }

            foreach (PaidDonation CurrPaidDonation in target.PaidDonations)
            {
                donations += CurrPaidDonation.ToString();
                donations += "\n";
            }
            donations = donations.Remove(donations.Length - 1);
            string expected = "Total owed for the monthly payment: \""+ target.MonthlyPaymentTotal+ "\"\n" + 
                              "Last month the monthly payment was made: \""+ target.LastMonthlyPaymentDate.Month +"\"\n" +
                              "Donations:\n" + donations;
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
