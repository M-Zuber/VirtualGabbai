using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass()]
    public class PersonTest
    {
        private int id;
        private string emailAddress;
        private string GivenName;
        private string FamilyName;
        private string streetAddress;
        private Account account = null;
        private List<PhoneNumber> phoneNumbers = null;
        private List<Yahrtzieht> yahrtziehts = null;
        private Person targetPerson = null;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            id = 1;
            emailAddress = "funinjust@gmail.com";
            GivenName = "Justin";
            FamilyName = "Fune";
            streetAddress = ";12;somwhere road;city;state;country;325";
            account = new Account {
                ID = 1,
                MonthlyPaymentAmount = 50,
                LastMonthlyPaymentDate = new DateTime(DateTime.Today.Year, 1, DateTime.Today.Day),
                Donations = new List<Donation>()
                {
                    new Donation { ID  = 1,Reason = "cuz i wanna",Amount = 45.90, DonationDate = DateTime.Today,Comments = "i dont know that we will ever see the money" },
                    new Donation { ID = 2, Reason = "it was a glorious day", Amount = 4010, DonationDate = DateTime.Today, Comments = "it was good that this money came in", DatePaid = DateTime.Today , Paid = true }
                }
            };
            phoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber { ID = 1, Number = "123456789", Type = new PhoneType { ID = 1, Name = "nothing" } }
            };
            yahrtziehts = new List<Yahrtzieht>()
            {
                new Yahrtzieht { ID = 1, Date = DateTime.MinValue, Name = "ploni ben almoni", Relation =  "they where not related" }
            };
            targetPerson = new Person
            {
                ID = id,
                Email = emailAddress,
                GivenName = GivenName,
                FamilyName = FamilyName,
                Member = true,
                Address = streetAddress,
                Account = account,
                PhoneNumbers = phoneNumbers,
                Yahrtziehts = yahrtziehts
            };
        }
        
        [TestCleanup()]
        public void MyTestCleanup()
        {
            targetPerson = null;
            account = null;
            phoneNumbers = null;
            yahrtziehts = null;
        }

        #region Equals Tests

        /// <summary>
        ///Comparing two people with no differences
        ///</summary>
        [TestMethod()]
        public void Person_Equals_NoDifferences()
        {
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName,
                                    targetPerson.FamilyName, targetPerson.Member, targetPerson.Address,
                                    targetPerson.Account, targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
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
            string secondGivenName = "Jack";
            string secondFamilyName = "O'niell";
            string secondStreetAddress = "12;45;cheyenne mountain;summer springs;colorado;usa;87254";
            Account secondAccount = new Account(18, 500, new DateTime(DateTime.Today.Year, 2, DateTime.Today.Day),
                new List<Donation>() {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new Donation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today, true)});
            List<PhoneNumber> secondPhoneNumbers = new List<PhoneNumber>()
            {
                new PhoneNumber(id:1,number: "987654321",type: new PhoneType(1,"nothing"))
            };
            List<Yahrtzieht> secondYahrtziehts = new List<Yahrtzieht>()
            {
                new Yahrtzieht(id:18,date: DateTime.MinValue,name: "ploni ben almoni",relation: "they where not related")
            };
            Person otherPerson = new Person(secondId, secondEmailAddress, secondGivenName,
                                    secondFamilyName, false, secondStreetAddress, secondAccount,
                                    secondPhoneNumbers, secondYahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the membership status
        ///</summary>
        [TestMethod()]
        public void Person_Equals_DifferenceInMembership()
        {
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName,
                                    targetPerson.FamilyName, false, targetPerson.Address,
                                    targetPerson.Account, targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the email
        ///</summary>
        [TestMethod()]
        public void Person_Equals_DifferenceInEmail()
        {
            Person otherPerson = new Person(targetPerson.ID, "yeahright@somethingelse.blah", targetPerson.GivenName, targetPerson.FamilyName,
                                    targetPerson.Member, targetPerson.Address, targetPerson.Account,
                                    targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the first name
        ///</summary>
        [TestMethod()]
        public void People_Equals_DfferenceInGivenName()
        {
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, "not him again", targetPerson.FamilyName,
                                    targetPerson.Member, targetPerson.Address, targetPerson.Account,
                                    targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the last name
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInFamilyName()
        {
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName, "no it wasnt me",
                                     targetPerson.Member, targetPerson.Address, targetPerson.Account,
                                     targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the street address
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInStreetAddress()
        {
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName, targetPerson.FamilyName,
                                     targetPerson.Member, ";;;;;;", targetPerson.Account,
                                     targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the account
        ///</summary>
        [TestMethod()]
        public void People_Equals_DifferenceInAccount()
        {
            Account secondAccount = new Account(18, 500, new DateTime(DateTime.Today.Year, 2, DateTime.Today.Day),
                new List<Donation>() {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new Donation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today, true)});
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName,
                                    targetPerson.FamilyName, targetPerson.Member, targetPerson.Address,
                                    secondAccount, targetPerson.PhoneNumbers, targetPerson.Yahrtziehts);
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
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName,
                                    targetPerson.FamilyName, targetPerson.Member, targetPerson.Address,
                                    targetPerson.Account, secondPhoneNumbers, targetPerson.Yahrtziehts);
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
            Person otherPerson = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName,
                                    targetPerson.FamilyName, targetPerson.Member, targetPerson.Address,
                                    targetPerson.Account, targetPerson.PhoneNumbers, secondYahrtziehts);
            Assert.IsFalse(targetPerson.Equals(otherPerson));
        }

        [TestMethod]
        public void Person_Equals_Null_Returns_False()
        {
            Assert.IsFalse(targetPerson.Equals(null));
        }

        [TestMethod]
        public void Person_Equals_Non_Person_Returns_False()
        {
            Assert.IsFalse(targetPerson.Equals(0));
        }

        [TestMethod]
        public void Person_Equals_Same_Ref_Returns_True()
        {
            Assert.IsTrue(targetPerson.Equals(targetPerson));
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
                for (int j = 1; j < yahrtziehts.Count; j++)
                {
                    yahrtziehtStrings += "\n" + yahrtziehts[j].ToString();
                }
            }

            string expected = "Justin Fune\nfuninjust@gmail.com\nLives at:\n" + (new StreetAddress(streetAddress)).ToString() +
                              "\nHas membership" +
                              "\nAccount information:\n" + account.ToString() +
                              "\nPhone Numbers:\n\t" + phoneNumberStrings +
                              "\nYahrtziehts:\n\t" + yahrtziehtStrings;
            string actual = targetPerson.ToString();
            var eParts = expected.Split(new string[] { "\n" }, StringSplitOptions.None);
            var aParts = actual.Split(new string[] { "\n" }, StringSplitOptions.None);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Person.ToString() with membership field false test
        ///</summary>
        [TestMethod()]
        public void Person_ToString_IsNotAMember()
        {
            Person newTarget = new Person(targetPerson.ID, targetPerson.Email, targetPerson.GivenName, targetPerson.FamilyName,
                                          false, targetPerson.Address, targetPerson.Account,
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
                for (int j = 1; j < yahrtziehts.Count; j++)
                {
                    yahrtziehtStrings += "\n" + yahrtziehts[j].ToString();
                }
            }

            string expected = "Justin Fune\nfuninjust@gmail.com\nLives at:\n" + (new StreetAddress(streetAddress)).ToString() +
                              "\nAccount information:\n" + account.ToString() +
                              "\nPhone Numbers:\n\t" + phoneNumberStrings +
                              "\nYahrtziehts:\n\t" + yahrtziehtStrings;
            string actual = newTarget.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
