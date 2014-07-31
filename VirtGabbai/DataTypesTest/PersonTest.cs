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

        #region Test Data Members

        // Target Data Members
        private int id;
        private string emailAddress;
        private string firstName; 
        private string lastName;
        private string streetAddress;
        private Account personalAccount = null;
        private List<PhoneNumber> phoneNumbers = null;
        private List<Yahrtzieht> yahrtziehts = null;
        private Person targetPerson = null;

        #endregion
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            id = 1;
            emailAddress = "funinjust@gmail.com";
            firstName = "Justin";
            lastName = "Fune";
            streetAddress = ";12;somwhere road;city;state;country;325";
            personalAccount = new Account(1, 50, new DateTime(DateTime.Today.Year, 1, DateTime.Today.Day),
            new List<Donation>() {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new PaidDonation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today)});
            phoneNumbers =  new List<PhoneNumber>()
            {
                new PhoneNumber(1, "123456789", new PhoneType(1,"nothing"))
            };
            yahrtziehts = new List<Yahrtzieht>()
            {
                new Yahrtzieht(1, DateTime.MinValue, "ploni ben almoni", "they where not related")
            };
            targetPerson = new Person(id, emailAddress, firstName, lastName, true,
                                streetAddress, personalAccount, phoneNumbers, yahrtziehts);
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetPerson = null;
            personalAccount = null;
            phoneNumbers = null;
            yahrtziehts = null;
        }
        //
        #endregion
        
        #region Equals Tests

        /// <summary>
        ///Comparing two people with no differences
        ///</summary>
        [TestMethod()]
        public void Person_Equals_NoDifferences()
        {
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), targetPerson.FirstName,
                                    targetPerson.LastName, targetPerson.MembershipStatus, targetPerson.Address.ToDbString(),
                                    targetPerson.PersonalAccount, targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsTrue(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in every field
        ///</summary>
        [TestMethod()]
        public void Person_Equals_DifferenceInEveryField()
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
            Person otherPerson = new Person(secondId, secondEmailAddress, secondFirstName,
                                    secondLastName, false,secondStreetAddress, secondPersonalAccount,
                                    secondPhoneNumbers, secondYahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the membership status
        ///</summary>
        [TestMethod()]
        public void Person_Equals_DifferenceInMembership()
        {
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), targetPerson.FirstName,
                                    targetPerson.LastName, false, targetPerson.Address.ToDbString(),
                                    targetPerson.PersonalAccount, targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the email
        ///</summary>
        [TestMethod()]
        public void Person_Equals_DifferenceInEmail()
        {
            Person otherPerson = new Person(targetPerson._Id, "yeahright@somethingelse.blah", targetPerson.FirstName, targetPerson.LastName,
                                    targetPerson.MembershipStatus,targetPerson.Address.ToDbString(), targetPerson.PersonalAccount,
                                    targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the first name
        ///</summary>
        [TestMethod()]
        public void People_Equals_DfferenceInFirstName()
        {
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), "not him again", targetPerson.LastName,
                                    targetPerson.MembershipStatus,targetPerson.Address.ToDbString(), targetPerson.PersonalAccount,
                                    targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the last name
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInLastName()
        {
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), targetPerson.FirstName, "no it wasnt me",
                                     targetPerson.MembershipStatus,targetPerson.Address.ToDbString(), targetPerson.PersonalAccount,
                                     targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the street address
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInStreetAddress()
        {
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), targetPerson.FirstName, targetPerson.LastName,
                                     targetPerson.MembershipStatus, ";;;;;;", targetPerson.PersonalAccount,
                                     targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the account
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInAccount()
        {
            Account secondPersonalAccount = new Account(18, 500, new DateTime(DateTime.Today.Year, 2, DateTime.Today.Day),
                new List<Donation>() {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new PaidDonation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today)});
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), targetPerson.FirstName,
                                    targetPerson.LastName, targetPerson.MembershipStatus,targetPerson.Address.ToDbString(),
                                    secondPersonalAccount, targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the phone numbers
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInPhoneNumbers()
        {
            List<PhoneNumber> secondPhoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber(1, "987654321", new PhoneType(1,"nothing"))
            };
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), targetPerson.FirstName,
                                    targetPerson.LastName, targetPerson.MembershipStatus,targetPerson.Address.ToDbString(),
                                    targetPerson.PersonalAccount, secondPhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the yahrtziehts
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInLYahrtziehts()
        {
            List<Yahrtzieht> secondYahrtziehts = new List<Yahrtzieht>()
            {
                new Yahrtzieht(18, DateTime.MinValue, "ploni ben almoni", "they where not related")
            };
            Person otherPerson = new Person(targetPerson._Id, targetPerson.Email.ToString(), targetPerson.FirstName,
                                    targetPerson.LastName, targetPerson.MembershipStatus,targetPerson.Address.ToDbString(),
                                    targetPerson.PersonalAccount, targetPerson.PhoneNumbers, secondYahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        #endregion

        #region ToStringTests

        /// <summary>
        ///Person.ToString() with membership field true test
        ///</summary>
        [TestMethod()]
        public void Person_ToString_IsAMember()
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
            string actual = targetPerson.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Person.ToString() with membership field false test
        ///</summary>
        [TestMethod()]
        public void Person_ToString_IsNotAMember()
        {
            Person newTarget = new Person(targetPerson._Id, targetPerson.Email.Address, targetPerson.FirstName, targetPerson.LastName,
                                          false, targetPerson.Address.ToDbString(), targetPerson.PersonalAccount,
                                          targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
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
            string actual = newTarget.ToString();
            Assert.AreEqual(expected, actual);
        }
        
        #endregion
    }
}
