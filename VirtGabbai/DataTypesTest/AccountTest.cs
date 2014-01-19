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

        #region Test Data Members

        // Target Data Members
        private Account targetAccount = null;
        private List<Donation> allDonations = null;
        DateTime lastMonthlyPaymentDate = DateTime.Today;
        int monthlyPaymentTotal;
        int id;

        #endregion

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            id = 1;
            monthlyPaymentTotal = 12;
            allDonations = new List<Donation>() 
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

            targetAccount = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetAccount = null;
            allDonations = null;
        }
        //
        #endregion

        #region Equals Tests

        /// <summary>
        ///Comparing two accounts with a difference in the donations list
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInDonations_Test()
        {   
            List<Donation> otherDonations = new List<Donation>();
            otherDonations.AddRange(allDonations);
            otherDonations.Add(new PaidDonation(34, "reason:6", 12.5, DateTime.Today, "comment", DateTime.Today));
            otherDonations.Add(new PaidDonation(7, "reason:7", 12.5, DateTime.Today, "comment", DateTime.Today));
            otherDonations.Add(new PaidDonation(56, "reason:8", 12.5, DateTime.Today, "comment", DateTime.Today));
            Account otherAccount = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, otherDonations);

            Assert.IsFalse(targetAccount.Equals(otherAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the last montly payment date
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInLastMonthlyPayment_Test()
        {
            Account otherAccount = new Account(id, monthlyPaymentTotal, DateTime.MaxValue, allDonations);
            Assert.IsFalse(targetAccount.Equals(otherAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the monthly payment total
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInMonthlyPaymentTotal_Test()
        {
            Account otherAccount = new Account(id, (2 * monthlyPaymentTotal), lastMonthlyPaymentDate, allDonations);
            Assert.IsFalse(targetAccount.Equals(otherAccount));
        }

        /// <summary>
        ///Comparing two accounts with a difference in the id
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInId_Test()
        {
            Account otherAccount = new Account((id * 2), monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            Assert.IsFalse(targetAccount.Equals(otherAccount));
        }

        /// <summary>
        ///Comparing two accounts with no differences
        ///</summary>
        [TestMethod()]
        public void Account_Equals_NoDifferences_Test()
        {
            Account otherAccount = new Account(id, monthlyPaymentTotal, lastMonthlyPaymentDate, allDonations);
            Assert.IsTrue(targetAccount.Equals(otherAccount));
        }

        /// <summary>
        ///Comparing two accounts with every field different
        ///</summary>
        [TestMethod()]
        public void Account_Equals_DifferenceInEveryField_Test()
        {
            List<Donation> otherDonation = new List<Donation>();
            otherDonation.AddRange(allDonations);
            otherDonation.Add(new PaidDonation(6, "reason:6", 12.5, DateTime.Today, "comment", DateTime.Today));
            otherDonation.Add(new PaidDonation(7, "reason:7", 12.5, DateTime.Today, "comment", DateTime.Today));
            otherDonation.Add(new PaidDonation(8, "reason:8", 12.5, DateTime.Today, "comment", DateTime.Today));
            Account otherAccount = new Account((id * 2), (monthlyPaymentTotal * 2), DateTime.MaxValue, otherDonation);
            Assert.IsFalse(targetAccount.Equals(otherAccount));
        }

        #endregion
        
        #region GetPaidDonations Tests
        
        /// <summary>
        ///Running the GetPaidDonations function when all the donations are paid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void Account_GetPaidDonations_AllDonations_ofType_PaidDonation_Test()
        {
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
            actual = Account_Accessor.GetPaidDonations(allDonations);
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Running the GetPaidDonations function when some of the donations are paid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void Account_GetPaidDonations_SomeDonations_ofType_PaidDonation_Test()
        {
            List<Donation> allDonations = new List<Donation>()
            {
                new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "comment", DateTime.Today),
                new Donation(2, "reason:2", 12.5, DateTime.Today, "comment"),
                new PaidDonation(3, "reason:3", 12.5, DateTime.Today, "comment", DateTime.Today),
                new Donation(4, "reason:4", 12.5, DateTime.Today, "comment")
            };
            List<PaidDonation> actual;
            actual = Account_Accessor.GetPaidDonations(allDonations);
            for (int i = 0; i < allDonations.Count; i++)
            {
                if (allDonations[i] is PaidDonation)
                {
                    Assert.IsTrue(actual.Contains((PaidDonation)allDonations[i]));
                }
            }
        }

        /// <summary>
        ///Running the GetPaidDonations function when no donations are paid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void Account_GetPaidDonations_NoDonations_ofType_PaidDonation_Test()
        {
            List<Donation> allDonations = new List<Donation>()
            {
                new Donation(1, "reason:1", 12.5, DateTime.Today, "comment"),
                new Donation(2, "reason:2", 12.5, DateTime.Today, "comment"),
                new Donation(3, "reason:3", 12.5, DateTime.Today, "comment"),
                new Donation(4, "reason:4", 12.5, DateTime.Today, "comment")
            };
            List<PaidDonation> actual;
            actual = Account_Accessor.GetPaidDonations(allDonations);
            Assert.IsTrue(actual.Count == 0);
        }

        #endregion
        
        #region GetUnPaidDonations Tests

        /// <summary>
        ///Running the GetUnpaidDonations function when no donations are unpaid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void Account_GetUnpaidDonations_NoDonations_ofType_Donation_Test()
        {
            List<Donation> allDonations = new List<Donation>()
            {
                new PaidDonation(1, "reason:1", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(3, "reason:3", 12.5, DateTime.Today, "comment", DateTime.Today),
                new PaidDonation(4, "reason:4", 12.5, DateTime.Today, "comment", DateTime.Today)
            };
            List<Donation> actual;
            actual = Account_Accessor.GetUnpaidDonations(allDonations);
            Assert.IsTrue(actual.Count == 0);
        }

        /// <summary>
        ///Running the GetUnpaidDonations function when some of the donations are unpaid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void Account_GetUnpaidDonations_SomeDonations_ofType_Donation_Test()
        {
            List<Donation> allDonations = new List<Donation>()
            {
                new Donation(1, "reason:1", 12.5, DateTime.Today, "comment"),
                new PaidDonation(2, "reason:2", 12.5, DateTime.Today, "comment", DateTime.Today),
                new Donation(3, "reason:3", 12.5, DateTime.Today, "comment"),
                new PaidDonation(4, "reason:4", 12.5, DateTime.Today, "comment", DateTime.Today)
            };
            List<Donation> actual;
            actual = Account_Accessor.GetUnpaidDonations(allDonations);

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
        ///Running the GetUnpaidDonations function when all of the donations are unpaid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DataTypes.dll")]
        public void Account_GetUnpaidDonations_AllDonations_ofType_Donation_Test()
        {
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
            actual = Account_Accessor.GetUnpaidDonations(allDonations);
            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion
        
        #region ToString Tests
        
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

            foreach (PaidDonation CurrPaidDonation in targetAccount.PaidDonations)
            {
                donations += CurrPaidDonation.ToString();
                donations += "\n";
            }
            donations = donations.Remove(donations.Length - 1);
            string expected = "Total owed for the monthly payment: \""+ targetAccount.MonthlyPaymentTotal+ "\"\n" + 
                              "Last month the monthly payment was made: \""+ targetAccount.LastMonthlyPaymentDate.Month +"\"\n" +
                              "Donations:\n" + donations;
            string actual = targetAccount.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
