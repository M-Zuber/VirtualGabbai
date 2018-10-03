using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DataCache.Tests
{
    [TestClass]
    public class PersonTest
    {
        private int _id;
        private string _emailAddress;
        private string _givenName;
        private string _familyName;
        private string _streetAddress;
        private Account _account;
        private List<PhoneNumber> _phoneNumbers;
        private List<Yahrtzieht> _yahrtziehts;
        private Person _targetPerson;

        [TestInitialize]
        public void MyTestInitialize()
        {
            _id = 1;
            _emailAddress = "funinjust@gmail.com";
            _givenName = "Justin";
            _familyName = "Fune";
            _streetAddress = ";12;somwhere road;city;state;country;325";
            _account = new Account
            {
                ID = 1,
                MonthlyPaymentAmount = 50,
                LastMonthlyPaymentDate = new DateTime(DateTime.Today.Year, 1, DateTime.Today.Day),
                Donations = new List<Donation>
                {
                    new Donation { ID  = 1,Reason = "cuz i wanna",Amount = 45.90, DonationDate = DateTime.Today,Comments = "i dont know that we will ever see the money" },
                    new Donation { ID = 2, Reason = "it was a glorious day", Amount = 4010, DonationDate = DateTime.Today, Comments = "it was good that this money came in", DatePaid = DateTime.Today , Paid = true }
                }
            };
            _phoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber { ID = 1, Number = "123456789", Type = new PhoneType { ID = 1, Name = "nothing" } }
            };
            _yahrtziehts = new List<Yahrtzieht>
            {
                new Yahrtzieht { ID = 1, Date = DateTime.MinValue, Name = "ploni ben almoni", Relation =  "they where not related" }
            };
            _targetPerson = new Person
            {
                ID = _id,
                Email = _emailAddress,
                GivenName = _givenName,
                FamilyName = _familyName,
                Member = true,
                Address = _streetAddress,
                Account = _account,
                PhoneNumbers = _phoneNumbers,
                Yahrtziehts = _yahrtziehts
            };
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            _targetPerson = null;
            _account = null;
            _phoneNumbers = null;
            _yahrtziehts = null;
        }

        #region Equals Tests

        /// <summary>
        ///Comparing two people with no differences
        ///</summary>
        [TestMethod]
        public void Person_Equals_NoDifferences()
        {
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName,
                                    _targetPerson.FamilyName, _targetPerson.Member, _targetPerson.Address,
                                    _targetPerson.Account, _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsTrue(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in every field
        ///</summary>
        [TestMethod]
        public void Person_Equals_DifferenceInEveryField()
        {
            const int secondId = 2;
            const string secondEmailAddress = "ColonelJack@stargateCommand.com";
            const string secondGivenName = "Jack";
            const string secondFamilyName = "O'niell";
            const string secondStreetAddress = "12;45;cheyenne mountain;summer springs;colorado;usa;87254";
            var secondAccount = new Account(18, 500, new DateTime(DateTime.Today.Year, 2, DateTime.Today.Day),
                new List<Donation>
                {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new Donation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today, true)});
            var secondPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(1,"987654321",new PhoneType(1,"nothing"))
            };
            var secondYahrtziehts = new List<Yahrtzieht>
            {
                new Yahrtzieht(18,DateTime.MinValue,"ploni ben almoni","they where not related")
            };
            var otherPerson = new Person(secondId, secondEmailAddress, secondGivenName,
                                    secondFamilyName, false, secondStreetAddress, secondAccount,
                                    secondPhoneNumbers, secondYahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the membership status
        ///</summary>
        [TestMethod]
        public void Person_Equals_DifferenceInMembership()
        {
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName,
                                    _targetPerson.FamilyName, false, _targetPerson.Address,
                                    _targetPerson.Account, _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the email
        ///</summary>
        [TestMethod]
        public void Person_Equals_DifferenceInEmail()
        {
            var otherPerson = new Person(_targetPerson.ID, "yeahright@somethingelse.blah", _targetPerson.GivenName, _targetPerson.FamilyName,
                                    _targetPerson.Member, _targetPerson.Address, _targetPerson.Account,
                                    _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the first name
        ///</summary>
        [TestMethod]
        public void People_Equals_DfferenceInGivenName()
        {
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, "not him again", _targetPerson.FamilyName,
                                    _targetPerson.Member, _targetPerson.Address, _targetPerson.Account,
                                    _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the last name
        ///</summary>
        [TestMethod]
        public void People_Equals_DifferenceInFamilyName()
        {
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName, "no it wasnt me",
                                     _targetPerson.Member, _targetPerson.Address, _targetPerson.Account,
                                     _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the street address
        ///</summary>
        [TestMethod]
        public void People_Equals_DifferenceInStreetAddress()
        {
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName, _targetPerson.FamilyName,
                                     _targetPerson.Member, ";;;;;;", _targetPerson.Account,
                                     _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the account
        ///</summary>
        [TestMethod]
        public void People_Equals_DifferenceInAccount()
        {
            var secondAccount = new Account(18, 500, new DateTime(DateTime.Today.Year, 2, DateTime.Today.Day),
                new List<Donation>
                {new Donation(1, "cuz i wanna", 45.90, DateTime.Today, "i dont know that we will ever see the money"),
                                      new Donation(2, "it was a glorious day", 4010, DateTime.Today, "it was good that this money came in", DateTime.Today, true)});
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName,
                                    _targetPerson.FamilyName, _targetPerson.Member, _targetPerson.Address,
                                    secondAccount, _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the phone numbers
        ///</summary>
        [TestMethod]
        public void People_Equals_DifferenceInPhoneNumbers()
        {
            var secondPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(1, "987654321", new PhoneType(1,"nothing"))
            };
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName,
                                    _targetPerson.FamilyName, _targetPerson.Member, _targetPerson.Address,
                                    _targetPerson.Account, secondPhoneNumbers, _targetPerson.Yahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        /// <summary>
        ///Comparing two people with a difference in the yahrtziehts
        ///</summary>
        [TestMethod]
        public void People_Equals_DifferenceInLYahrtziehts()
        {
            var secondYahrtziehts = new List<Yahrtzieht>
            {
                new Yahrtzieht(18, DateTime.MinValue, "ploni ben almoni", "they where not related")
            };
            var otherPerson = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName,
                                    _targetPerson.FamilyName, _targetPerson.Member, _targetPerson.Address,
                                    _targetPerson.Account, _targetPerson.PhoneNumbers, secondYahrtziehts);
            Assert.IsFalse(_targetPerson.Equals(otherPerson));
        }

        [TestMethod]
        public void Person_Equals_Null_Returns_False()
        {
            Assert.IsFalse(_targetPerson.Equals(null));
        }

        [TestMethod]
        public void Person_Equals_Non_Person_Returns_False()
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.IsFalse(_targetPerson.Equals(0));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [TestMethod]
        public void Person_Equals_Same_Ref_Returns_True()
        {
            var other = _targetPerson;

            Assert.IsTrue(_targetPerson.Equals(other));
            Assert.IsTrue(other.Equals(_targetPerson));
        }

        #endregion

        #region ToStringTests

        /// <summary>
        ///Person.ToString() with membership field true test
        ///</summary>
        [TestMethod]
        public void Person_ToString_IsAMember()
        {
            var phoneNumberStrings = "";
            if (_phoneNumbers.Count > 0)
            {
                phoneNumberStrings += _phoneNumbers[0].ToString();
                for (var i = 1; i < _phoneNumbers.Count; i++)
                {
                    phoneNumberStrings += "\n" + _phoneNumbers[i];
                }
            }

            var yahrtziehtStrings = "";
            if (_yahrtziehts.Count > 0)
            {
                yahrtziehtStrings += _yahrtziehts[0].ToString();
                for (var j = 1; j < _yahrtziehts.Count; j++)
                {
                    yahrtziehtStrings += "\n" + _yahrtziehts[j];
                }
            }

            var expected = "Justin Fune\nfuninjust@gmail.com\nLives at:\n" + new StreetAddress(_streetAddress) +
                              "\nHas membership" +
                              "\nAccount information:\n" + _account +
                              "\nPhone Numbers:\n\t" + phoneNumberStrings +
                              "\nYahrtziehts:\n\t" + yahrtziehtStrings;
            var actual = _targetPerson.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Person.ToString() with membership field false test
        ///</summary>
        [TestMethod]
        public void Person_ToString_IsNotAMember()
        {
            var newTarget = new Person(_targetPerson.ID, _targetPerson.Email, _targetPerson.GivenName, _targetPerson.FamilyName,
                                          false, _targetPerson.Address, _targetPerson.Account,
                                          _targetPerson.PhoneNumbers, _targetPerson.Yahrtziehts);
            var phoneNumberStrings = "";
            if (_phoneNumbers.Count > 0)
            {
                phoneNumberStrings += _phoneNumbers[0].ToString();
                for (var i = 1; i < _phoneNumbers.Count; i++)
                {
                    phoneNumberStrings += "\n" + _phoneNumbers[i];
                }
            }

            var yahrtziehtStrings = "";
            if (_yahrtziehts.Count > 0)
            {
                yahrtziehtStrings += _yahrtziehts[0].ToString();
                for (var j = 1; j < _yahrtziehts.Count; j++)
                {
                    yahrtziehtStrings += "\n" + _yahrtziehts[j];
                }
            }

            var expected = "Justin Fune\nfuninjust@gmail.com\nLives at:\n" + new StreetAddress(_streetAddress) +
                              "\nAccount information:\n" + _account +
                              "\nPhone Numbers:\n\t" + phoneNumberStrings +
                              "\nYahrtziehts:\n\t" + yahrtziehtStrings;
            var actual = newTarget.ToString();
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
