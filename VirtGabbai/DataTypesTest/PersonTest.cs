using LocalTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LocalTypesTest
{
    
    
    /// <summary>
    ///This is a test class for PersonTest and is intended
    ///to contain all PersonTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PersonTest
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

        #region Setup

        static int id = 1;
        static string emailAddress = "funinjust@gmail.com";
        static string firstName = "Justin";
        static string lastName = "Fune";
        static string streetAddress = ";12;somwhere road;city;state;country;325";
        static Account personalAccount = new Account(1, 50, new DateTime(DateTime.Today.Year, 1, DateTime.Today.Day),
            new List<Donation>() {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new PaidDonation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today)});
        static List<PhoneNumber> phoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber(1, "123456789", new PhoneType(1,"nothing"))
            };
        static List<Yahrtzieht> yahrtziehts = new List<Yahrtzieht>()
            {
                new Yahrtzieht(1, DateTime.MinValue, "ploni ben almoni", "they where not related")
            };
        private Person target = new Person(id, emailAddress, firstName, lastName, true,
                                streetAddress, personalAccount, phoneNumbers, yahrtziehts);

        #endregion
        
        #region Equals Tests

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void AllSameEqualsTest()
        {
            object obj = new Person(target._Id, target.Email.ToString(), target.FirstName,
                                    target.LastName, target.MembershipStatus, target.Address.ToDbString(),
                                    target.PersonalAccount, target.PhoneNumbers, target.Yahrtziehts);
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
            int secondId = 2;
            string secondEmailAddress = "ColonelJack@stargateCommand.com";
            string secondFirstName = "Jack";
            string secondLastName = "O'niell";
            string secondStreetAddress = "12;45;cheyenne mountain;summer springs;colorado;usa;87254";
            Account secondPersonalAccount = new Account(18, 500, new DateTime(DateTime.Today.Year, 2, DateTime.Today.Day),
                new List<Donation>() {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new PaidDonation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today)});
            List<PhoneNumber> secondPhoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber(1, "987654321", new PhoneType(1,"nothing"))
            };
            List<Yahrtzieht> secondYahrtziehts = new List<Yahrtzieht>()
            {
                new Yahrtzieht(18, DateTime.MinValue, "ploni ben almoni", "they where not related")
            };
            object obj = new Person(secondId, secondEmailAddress, secondFirstName,
                                    secondLastName, false,secondStreetAddress, secondPersonalAccount,
                                    secondPhoneNumbers, secondYahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffMembershipEqualsTest()
        {
            object obj = new Person(target._Id, target.Email.ToString(), target.FirstName,
                                    target.LastName, false, target.Address.ToDbString(),
                                    target.PersonalAccount, target.PhoneNumbers, target.Yahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffEmailEqualsTest()
        {
            object obj = new Person(target._Id, "yeahright@somethingelse.blah", target.FirstName, target.LastName,
                                    target.MembershipStatus,target.Address.ToDbString(), target.PersonalAccount,
                                    target.PhoneNumbers, target.Yahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffFirstNameEqualsTest()
        {
            object obj = new Person(target._Id, target.Email.ToString(), "not him again", target.LastName,
                                    target.MembershipStatus,target.Address.ToDbString(), target.PersonalAccount,
                                    target.PhoneNumbers, target.Yahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffLastNameEqualsTest()
        {
            object obj = new Person(target._Id, target.Email.ToString(), target.FirstName, "no it wasnt me",
                                     target.MembershipStatus,target.Address.ToDbString(), target.PersonalAccount,
                                     target.PhoneNumbers, target.Yahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffStreetAddressEqualsTest()
        {
            object obj = new Person(target._Id, target.Email.ToString(), target.FirstName, target.LastName,
                                     target.MembershipStatus, ";;;;;;", target.PersonalAccount,
                                     target.PhoneNumbers, target.Yahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffAccountEqualsTest()
        {
            Account secondPersonalAccount = new Account(18, 500, new DateTime(DateTime.Today.Year, 2, DateTime.Today.Day),
                new List<Donation>() {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new PaidDonation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today)});
            object obj = new Person(target._Id, target.Email.ToString(), target.FirstName,
                                    target.LastName, target.MembershipStatus,target.Address.ToDbString(),
                                    secondPersonalAccount, target.PhoneNumbers, target.Yahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffPhoneNumbersEqualsTest()
        {
            List<PhoneNumber> secondPhoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber(1, "987654321", new PhoneType(1,"nothing"))
            };
            object obj = new Person(target._Id, target.Email.ToString(), target.FirstName,
                                    target.LastName, target.MembershipStatus,target.Address.ToDbString(),
                                    target.PersonalAccount, secondPhoneNumbers, target.Yahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void DiffYahrtziehtEqualsTest()
        {
            List<Yahrtzieht> secondYahrtziehts = new List<Yahrtzieht>()
            {
                new Yahrtzieht(18, DateTime.MinValue, "ploni ben almoni", "they where not related")
            };
            object obj = new Person(target._Id, target.Email.ToString(), target.FirstName,
                                    target.LastName, target.MembershipStatus,target.Address.ToDbString(),
                                    target.PersonalAccount, target.PhoneNumbers, secondYahrtziehts);
            bool expected = false;
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ToStringTests

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string phoneNumberStrings = "";
            if (phoneNumbers.Count > 0)
            {
                phoneNumberStrings += phoneNumbers[0].ToString();
                for (int i = 1; i < phoneNumbers.Count; i++)
                {
                    phoneNumberStrings += "\n" + phoneNumbers[i].ToString();
                } 
            }

            string yahrtziehtStrings = "";
            if (yahrtziehts.Count > 0)
            {
                yahrtziehtStrings += yahrtziehts[0].ToString();
                for (int i = 1; i < yahrtziehts.Count; i++)
                {
                    yahrtziehtStrings += "\n" + yahrtziehts[i].ToString();
                }
            }

            string expected = "Justin Fune\nfuninjust@gmail.com\nLives at:\n" + (new StreetAddress(streetAddress)).ToString() +
                              "\nHas membership" +
                              "\nAccount information:\n" + personalAccount.ToString() +
                              "\nPhone Numbers:\n\t" + phoneNumberStrings +
                              "\nYahrtziehts:\n\t" + yahrtziehtStrings;
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void NotMemberToStringTest()
        {
            Person newTarget = new Person(target._Id, target.Email.Address, target.FirstName, target.LastName,
                                          false, target.Address.ToDbString(), target.PersonalAccount,
                                          target.PhoneNumbers, target.Yahrtziehts);
            string phoneNumberStrings = "";
            if (phoneNumbers.Count > 0)
            {
                phoneNumberStrings += phoneNumbers[0].ToString();
                for (int i = 1; i < phoneNumbers.Count; i++)
                {
                    phoneNumberStrings += "\n" + phoneNumbers[i].ToString();
                }
            }

            string yahrtziehtStrings = "";
            if (yahrtziehts.Count > 0)
            {
                yahrtziehtStrings += yahrtziehts[0].ToString();
                for (int i = 1; i < yahrtziehts.Count; i++)
                {
                    yahrtziehtStrings += "\n" + yahrtziehts[i].ToString();
                }
            }

            string expected = "Justin Fune\nfuninjust@gmail.com\nLives at:\n" + (new StreetAddress(streetAddress)).ToString() +
                              "\nAccount information:\n" + personalAccount.ToString() +
                              "\nPhone Numbers:\n\t" + phoneNumberStrings +
                              "\nYahrtziehts:\n\t" + yahrtziehtStrings;
            string actual;
            actual = newTarget.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
