//using System;
//using System.Collections.Generic;
//using System.Linq;
//using DataAccess;
//using DataCache;
//using LocalTypes;
//using Framework;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Net.Mail;

//namespace DataAccessTest
//{
    
    
//    /// <summary>
//    ///This is a test class for PersonAccessTest and is intended
//    ///to contain all PersonAccessTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class PersonAccessTest
//    {

//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        // 
//        //You can use the following additional attributes as you write your tests:
//        //
//        //Use ClassInitialize to run code before running the first test in the class
//        [ClassInitialize()]
//        public static void MyClassInitialize(TestContext testContext)
//        {
//            for (int i = 2; i <= 15; i++)
//            {
//                if (!Cache.CacheData.t_people.Any(person => person.C_id == i))
//                {
//                    var newPerson = t_people.Createt_people(i);
//                    newPerson.address = "12;"+ i + i +";main st;anywhere;anystate;usa;12345";
//                    newPerson.email = i + "@something.somewhere";
//                    newPerson.family_name = "Doe";
//                    newPerson.given_name = "Jack/Jane";
//                    newPerson.member = true;
//                    Cache.CacheData.t_people.AddObject(newPerson);
//                } 
//            }
            
//            #region Account info
//            int accountOwner = 2;
//            for (int newAccountIndex = 200; newAccountIndex <= 213; newAccountIndex++)
//            {
//                if (!Cache.CacheData.t_accounts.Any(acc => acc.C_id == newAccountIndex))
//                {
//                    var newAccount = t_accounts.Createt_accounts(newAccountIndex, accountOwner);
//                    newAccount.last_month_paid = DateTime.Today;
//                    newAccount.monthly_total = 0;
//                    Cache.CacheData.t_accounts.AddObject(newAccount);
//                    accountOwner++; 
//                }
//            }
//            for (int newDonationIndex = 1010; newDonationIndex <= 1015; newDonationIndex++)
//            {
//                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
//                {
//                    var newDonation = t_donations.Createt_donations(
//                                newDonationIndex, 200, "reason:" + newDonationIndex, 12.5, DateTime.Today, false);
//                    Cache.CacheData.t_donations.AddObject(newDonation);
//                }
//            }
//            for (int newDonationIndex = 1016; newDonationIndex <= 1020; newDonationIndex++)
//            {
//                if (!Cache.CacheData.t_donations.Any(donation => donation.C_id == newDonationIndex))
//                {
//                    var newDonation = t_donations.Createt_donations(
//                               newDonationIndex, 200, "reason:" + newDonationIndex, 12.5, DateTime.Today, true);
//                    newDonation.date_paid = DateTime.Today;
//                    Cache.CacheData.t_donations.AddObject(newDonation);
//                }
//            } 
//            #endregion

//            #region Number info
//            if (!Cache.CacheData.t_phone_types.Any(numberType => numberType.C_id == 1))
//            {
//                Cache.CacheData.t_phone_types.AddObject(t_phone_types.Createt_phone_types(1, "phonetype:1"));
//            }
//            for (int newPhoneNumberIndex = 100; newPhoneNumberIndex <= 110; newPhoneNumberIndex++)
//            {
//                if (!Cache.CacheData.t_phone_numbers.Any(number => number.C_id == newPhoneNumberIndex))
//                {
//                    var newPhoneNumber = t_phone_numbers.Createt_phone_numbers(
//                                2, "phone number:" + newPhoneNumberIndex.ToString(), 1, newPhoneNumberIndex);
//                    Cache.CacheData.t_phone_numbers.AddObject(newPhoneNumber); 
//                }
//            }
//            #endregion
            
//            #region Yahrtzieht info
//            int yahrPersonId = 2;
//            for (int i = 200; i < 214; i++)
//            {
//                if (!Cache.CacheData.t_yahrtziehts.Any(yahr => yahr.C_id == i))
//                {
//                    var newYahrtzieht = t_yahrtziehts.Createt_yahrtziehts(i, yahrPersonId, DateTime.Today, "ploni ben almoni");
//                    newYahrtzieht.relation = "they where not";
//                    Cache.CacheData.t_yahrtziehts.AddObject(newYahrtzieht);
//                    yahrPersonId++;
//                }
//            }
//            #endregion

//            Cache.CacheData.SaveChanges();
//        }
//        //
//        //Use ClassCleanup to run code after all tests in a class have run
//        [ClassCleanup()]
//        public static void MyClassCleanup()
//        {
//            Cache.CacheData.clear_database();
//        }
//        //
//        //Use TestInitialize to run code before running each test
//        //[TestInitialize()]
//        //public void MyTestInitialize()
//        //{
//        //}
//        //
//        //Use TestCleanup to run code after each test has run
//        //[TestCleanup()]
//        //public void MyTestCleanup()
//        //{
//        //}
//        //
//        #endregion
        
//        /*
//         * In other tests used #1
//         * In Lookup and Get tests used #2 & 3
//         * In Delete tests used #4, 5, 6
//         * In Update tests used #7, 8, 9
//         * In Upsert tests used #10, 20
//         * In Add tests added #16, 17, 18, 19
//         */

//        #region Add Tests

//        /// <summary>
//        ///A test for AddMultipleNewPersons
//        ///</summary>
//        [TestMethod()]
//        public void AddMultipleNewPersonsTest()
//        {
//            List<Person> newPersonList = new List<Person>()
//            {
//                new Person(17, "blah@blah.com", "jack", "doe", true,"12;12;blank;blank;blank;blank;123456", 
//                    new Account(54, 0, DateTime.Today, new List<Donation>()),
//                    new List<PhoneNumber>(), new List<Yahrtzieht>()),
//                new Person(18, "blah@blah.com", "jack", "doe", true, "12;12;blank;blank;blank;blank;123456", 
//                    new Account(55, 0, DateTime.Today, new List<Donation>()),
//                    new List<PhoneNumber>(), new List<Yahrtzieht>()),
//                new Person(19, "blah@blah.com", "jack", "doe", true,"12;12;blank;blank;blank;blank;123456", 
//                    new Account(56, 0, DateTime.Today, new List<Donation>()),
//                    new List<PhoneNumber>(), new List<Yahrtzieht>())
//            };
//            PersonAccess.AddMultipleNewPersons(newPersonList);
//            List<Person> afterAdding = new List<Person>() 
//            {
//                PersonAccess.GetById(17),
//                PersonAccess.GetById(18),
//                PersonAccess.GetById(19)
//            };
//            CollectionAssert.AreEqual(newPersonList, afterAdding);
//        }

//        /// <summary>
//        ///A test for AddNewPerson
//        ///</summary>
//        [TestMethod()]
//        public void AddNewPersonTest()
//        {
//            Person newPerson = new Person(16, "3245@235.com", "good", "luck", true, "12;12;blank;blank;blank;blank;123456",
//                new Account(57, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(), new List<Yahrtzieht>());
//            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
//            Enums.CRUDResults actual;
//            actual = PersonAccess.AddNewPerson(newPerson);
//            Assert.AreEqual(expected, actual);
//            Assert.AreEqual(newPerson, PersonAccess.GetById(16));
//        }

//        #endregion
        
//        #region Convert Tests

//        /// <summary>
//        ///A test for ConvertSingleDbPersonToLocalType
//        ///</summary>
//        [TestMethod()]
//        public void ConvertSingleDbPersonToLocalTypeTest()
//        {
//            t_people dbTypePerson = (from person in Cache.CacheData.t_people
//                                     where person.C_id == 3
//                                     select person).First();
//            Person expected = new Person(3, "3@something.somewhere",
//                            "Jack/Jane", "Doe", true, "12;33;main st;anywhere;anystate;usa;12345",
//                            new Account(201, 0, DateTime.Today, new List<Donation>()),
//                            new List<PhoneNumber>(), new List<Yahrtzieht>() {
//                                new Yahrtzieht(201, DateTime.Today, "ploni ben almoni", "they where not")});
//            Person actual;
//            actual = PersonAccess.ConvertSingleDbPersonToLocalType(dbTypePerson);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for ConvertSingleLocalPersonToDbType
//        ///</summary>
//        [TestMethod()]
//        public void ConvertSingleLocalPersonToDbTypeTest()
//        {
//            Person localTypePerson = new Person(3, "3@something.somewhere",
//                            "Jack/Jane", "Doe", true, "12;33;main st;anywhere;anystate;usa;12345",
//                            new Account(201, 0, DateTime.Today, new List<Donation>()),
//                            new List<PhoneNumber>(), new List<Yahrtzieht>());
//            t_people expected = t_people.Createt_people(3);
//            expected.address = "12;33;main st;anywhere;anystate;usa;12345";
//            expected.email = "3@something.somewhere";
//            expected.family_name = "Doe";
//            expected.given_name = "Jack/Jane";
//            expected.member = true;
//            t_people actual;
//            actual = PersonAccess.ConvertSingleLocalPersonToDbType(localTypePerson);
//            Assert.AreEqual(expected.address, actual.address, true);
//            Assert.AreEqual(expected.C_id, actual.C_id);
//            Assert.AreEqual(expected.email, actual.email);
//            Assert.AreEqual(expected.family_name, actual.family_name);
//            Assert.AreEqual(expected.given_name, actual.given_name);
//            Assert.AreEqual(expected.member, actual.member);
//        }

//        #endregion
        
//        #region Delete Tests

//        /// <summary>
//        ///A test for DeleteMultiplePersons
//        ///</summary>
//        [TestMethod()]
//        public void DeleteMultiplePersonsTest()
//        {
//            List<Person> deletedPersonList = new List<Person>()
//            {
//                PersonAccess.GetById(4),
//                PersonAccess.GetById(5)
//            };
//            PersonAccess.DeleteMultiplePersons(deletedPersonList);
//            List<Person> afterDelete = PersonAccess.GetAllPeople();
//            Assert.IsFalse(afterDelete.Contains(deletedPersonList[0]));
//            Assert.IsFalse(afterDelete.Contains(deletedPersonList[1]));
//        }

//        /// <summary>
//        ///A test for DeleteSinglePerson
//        ///</summary>
//        [TestMethod()]
//        public void DeleteSinglePersonTest()
//        {
//            Person deletedPerson = PersonAccess.GetById(6);
//            Enums.CRUDResults expected = Enums.CRUDResults.DELETE_SUCCESS;
//            Enums.CRUDResults actual;
//            actual = PersonAccess.DeleteSinglePerson(deletedPerson);
//            Assert.AreEqual(expected, actual);
//            List<Person> afterDelete = PersonAccess.GetAllPeople();
//            Assert.IsFalse(afterDelete.Contains(deletedPerson));
//        }

//        /// <summary>
//        ///A test for DeleteSinglePerson
//        ///</summary>
//        [TestMethod()]
//        public void DeleteSingleNonExistintPersonTest()
//        {
//            Person deletedPerson = new Person(32543, "blah@blah.com", "", "", true, ";;;;;;",
//                new Account(1, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(), new List<Yahrtzieht>());
//            Enums.CRUDResults expected = Enums.CRUDResults.DELETE_FAIL;
//            Enums.CRUDResults actual;
//            actual = PersonAccess.DeleteSinglePerson(deletedPerson);
//            Assert.AreEqual(expected, actual);
//        }

//        #endregion
        
//        #region Get Tests

//        /// <summary>
//        ///A test for GetAllPeople
//        ///</summary>
//        [TestMethod()]
//        public void GetAllPeopleTest()
//        {
//            List<Person> expected = PersonAccess.GetAllPeople();
//            List<Person> actual;
//            actual = PersonAccess.GetAllPeople();
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetMembers
//        ///</summary>
//        [TestMethod()]
//        public void GetMembersTest()
//        {
//            bool membershipStatus = true;
//            List<Person> expected = PersonAccess.ConvertMultipleDbPersonsToLocalType(
//                PersonAccess_Accessor.LookupByMembership(membershipStatus));
//            List<Person> actual;
//            actual = PersonAccess.GetPeopleByMembership(membershipStatus);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetNonMembers
//        ///</summary>
//        [TestMethod()]
//        public void GetNonMembersTest()
//        {
//            bool membershipStatus = false;
//            List<Person> expected = PersonAccess.ConvertMultipleDbPersonsToLocalType(
//                PersonAccess_Accessor.LookupByMembership(membershipStatus));
//            List<Person> actual;
//            actual = PersonAccess.GetPeopleByMembership(membershipStatus);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByAccount
//        ///</summary>
//        [TestMethod()]
//        public void GetByAccountTest()
//        {
//            Account accountSearchedBy = new Account(201, 0, DateTime.Today, new List<Donation>());
//            Person expected = new Person(3, "3@something.somewhere", "Jack/Jane", "Doe", true,
//                "12;33;main st;anywhere;anystate;usa;12345", new Account(201, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(),
//                new List<Yahrtzieht>() {new Yahrtzieht(201, DateTime.Today, "ploni ben almoni", "they where not") });
//            Person actual;
//            actual = PersonAccess.GetByAccount(accountSearchedBy);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByAccount
//        ///</summary>
//        [TestMethod()]
//        public void GetByNonExsitentAccountTest()
//        {
//            Account accountSearchedBy = new Account(879654, 1234, DateTime.Today, new List<Donation>());
//            Person expected = null;
//            Person actual;
//            actual = PersonAccess.GetByAccount(accountSearchedBy);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByAddress
//        ///</summary>
//        [TestMethod()]
//        public void GetByAddressTest()
//        {
//            StreetAddress addressSearchedBy = new StreetAddress("12;33;main st;anywhere;anystate;usa;12345");
//            List<Person> expected = new List<Person>()
//            {
//                new Person(3, "3@something.somewhere", "Jack/Jane", "Doe",true,
//                "12;33;main st;anywhere;anystate;usa;12345",
//                new Account(201, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(),
//                new List<Yahrtzieht>() 
//                {
//                    new Yahrtzieht(201, DateTime.Today, "ploni ben almoni", "they where not") 
//                })
//            }; 
//            List<Person> actual;
//            actual = PersonAccess.GetByAddress(addressSearchedBy);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByAddress
//        ///</summary>
//        [TestMethod()]
//        public void GetByNonExsitentAddressTest()
//        {
//            StreetAddress addressSearchedBy = new StreetAddress(";;;;;;");
//            List<Person> expected = new List<Person>();
//            List<Person> actual;
//            actual = PersonAccess.GetByAddress(addressSearchedBy);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByEmail
//        ///</summary>
//        [TestMethod()]
//        public void GetByEmailTest()
//        {
//            MailAddress email = new MailAddress("3@something.somewhere");
//            Person expected = new Person(3, "3@something.somewhere", "Jack/Jane", "Doe", true,
//                "12;33;main st;anywhere;anystate;usa;12345",
//                new Account(201, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(),
//                new List<Yahrtzieht>() 
//                { new Yahrtzieht(201, DateTime.Today, "ploni ben almoni", "they where not") });
//            Person actual;
//            actual = PersonAccess.GetByEmail(email);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByEmail
//        ///</summary>
//        [TestMethod()]
//        public void GetByNonExsistentEmailTest()
//        {
//            MailAddress email = new MailAddress("blah@co.il");
//            Person expected = null;
//            Person actual;
//            actual = PersonAccess.GetByEmail(email);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetById
//        ///</summary>
//        [TestMethod()]
//        public void GetByIdTest()
//        {
//            int id = 3;
//            Person expected = new Person(3, "3@something.somewhere", "Jack/Jane", "Doe", true,
//                "12;33;main st;anywhere;anystate;usa;12345",
//                new Account(201, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(),
//                new List<Yahrtzieht>() { new Yahrtzieht(201, DateTime.Today, "ploni ben almoni", "they where not") });
//            Person actual;
//            actual = PersonAccess.GetById(id);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetById
//        ///</summary>
//        [TestMethod()]
//        public void GetByNonExsitentIdTest()
//        {
//            int id = 0;
//            Person expected = null;
//            Person actual;
//            actual = PersonAccess.GetById(id);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByName
//        ///</summary>
//        [TestMethod()]
//        public void GetByNameTest()
//        {
//            string firstName = "Jack/Jane";
//            string lastName = "Doe";
//            List<Person> expected = PersonAccess.ConvertMultipleDbPersonsToLocalType(
//                PersonAccess_Accessor.LookupByName(firstName, lastName));
//            List<Person> actual;
//            actual = PersonAccess.GetByName(firstName, lastName);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByName
//        ///</summary>
//        [TestMethod()]
//        public void GetByNonExsistentNameTest()
//        {
//            string fullName = string.Empty;
//            List<Person> expected = new List<Person>();
//            List<Person> actual;
//            actual = PersonAccess.GetByName(fullName, fullName);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByPhoneNumber
//        ///</summary>
//        [TestMethod()]
//        public void GetByPhoneNumberTest()
//        {
//            PhoneNumber numberSearchedBy = new PhoneNumber(100, "phone number:100", new PhoneType(1, "phonetype:1"));
//            Person expected = PersonAccess.GetById(2);
//            Person actual;
//            actual = PersonAccess.GetByPhoneNumber(numberSearchedBy);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByPhoneNumber
//        ///</summary>
//        [TestMethod()]
//        public void GetByNonExsistentPhoneNumberTest()
//        {
//            PhoneNumber numberSearchedBy = new PhoneNumber(76829730, "doesnt exsist", new PhoneType(234325, "blahblah"));
//            Person expected = null;
//            Person actual;
//            actual = PersonAccess.GetByPhoneNumber(numberSearchedBy);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByYahrtzieht
//        ///</summary>
//        [TestMethod()]
//        public void GetByYahrtziehtTest()
//        {
//            Yahrtzieht yahrtziehtSearchedBy = new Yahrtzieht(201, DateTime.Today, "ploni ben almoni", "they where not");
//            List<Person> expected = PersonAccess.ConvertMultipleDbPersonsToLocalType(
//                PersonAccess_Accessor.LookupByYahrtzieht(
//                                    yahrtziehtSearchedBy.Name, yahrtziehtSearchedBy.Relation));
//            List<Person> actual;
//            actual = PersonAccess.GetByYahrtzieht(yahrtziehtSearchedBy);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for GetByYahrtzieht
//        ///</summary>
//        [TestMethod()]
//        public void GetByNonExsistentYahrtziehtTest()
//        {
//            Yahrtzieht yahrtziehtSearchedBy = new Yahrtzieht(23124, DateTime.Today, "35235", "35325");
//            List<Person> expected = new List<Person>();
//            List<Person> actual;
//            actual = PersonAccess.GetByYahrtzieht(yahrtziehtSearchedBy);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        #endregion
        
//        #region Lookup Tests

//        /// <summary>
//        ///A test for LookupAllPeople
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupAllPeopleTest()
//        {
//            List<t_people> expected = (from person in Cache.CacheData.t_people
//                                       select person).ToList();
//            List<t_people> actual;
//            actual = PersonAccess_Accessor.LookupAllPeople();
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByAccount
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByAccountTest()
//        {
//            int accountId = 201;
//            t_people expected = t_people.Createt_people(3);
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupByAccount(accountId);
//            Assert.AreEqual(expected.C_id, actual.C_id);
//        }

//        /// <summary>
//        ///A test for LookupByAccount
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByMembersTest()
//        {
//            bool membershipStatus = true;
//            List<t_people> expected = (from person in Cache.CacheData.t_people
//                                       where person.member == membershipStatus
//                                       select person).ToList();
//            List<t_people> actual = PersonAccess_Accessor.LookupByMembership(membershipStatus);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByAccount
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonMembersTest()
//        {
//            bool membershipStatus = false;
//            List<t_people> expected = (from person in Cache.CacheData.t_people
//                                       where person.member == membershipStatus
//                                       select person).ToList();
//            List<t_people> actual = PersonAccess_Accessor.LookupByMembership(membershipStatus);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByNoNExsistentAccount
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonExsistentAccountTest()
//        {
//            int accountId = 1578;
//            t_people expected = null;
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupByAccount(accountId);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByAddress
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByAddressTest()
//        {
//            string address = "12;33;main st;anywhere;anystate;usa;12345";
//            List<t_people> expected = new List<t_people>() 
//            {
//                t_people.Createt_people(3)
//            };
//            List<t_people> actual;
//            actual = PersonAccess_Accessor.LookupByAddress(address);
//            Assert.AreEqual(expected[0].C_id, actual[0].C_id);
//        }

//        /// <summary>
//        ///A test for LookupByAddress
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonExsistentAddressTest()
//        {
//            string address = string.Empty;
//            List<t_people> actual;
//            actual = PersonAccess_Accessor.LookupByAddress(address);
//            Assert.AreEqual(0, actual.Count);
//        }

//        /// <summary>
//        ///A test for LookupByEmail
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByEmailTest()
//        {
//            string email = "3@something.somewhere";
//            t_people expected = t_people.Createt_people(3);
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupByEmail(email);
//            Assert.AreEqual(expected.C_id, actual.C_id);
//        }

//        /// <summary>
//        ///A test for LookupByEmail
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonExsistentEmailTest()
//        {
//            string email = string.Empty;
//            t_people expected = null;
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupByEmail(email);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupById
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByIdTest()
//        {
//            int id = 3;
//            t_people expected = t_people.Createt_people(3);
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupById(id);
//            Assert.AreEqual(expected.C_id, actual.C_id);
//        }

//        /// <summary>
//        ///A test for LookupById
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonExsistentIdTest()
//        {
//            int id = 0;
//            t_people expected = null;
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupById(id);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByName
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNameTest()
//        {
//            string firstName = "Jack/Jane";
//            string lastName = "Doe";
//            List<t_people> expected = (from currPerson in Cache.CacheData.t_people
//                                       where currPerson.given_name == firstName &&
//                                             currPerson.family_name == lastName
//                                       select currPerson).ToList();

//            List<t_people> actual;
//            actual = PersonAccess_Accessor.LookupByName(firstName, lastName);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByName
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonExsistentNameTest()
//        {
//            string fullName = "blah";
//            List<t_people> actual;
//            actual = PersonAccess_Accessor.LookupByName(fullName, fullName);
//            Assert.AreEqual(0, actual.Count);
//        }

//        /// <summary>
//        ///A test for LookupByPhoneNumber
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByPhoneNumberTest()
//        {
//            string numberSearchedBy = "phone number:100";
//            t_people expected = t_people.Createt_people(2);
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupByPhoneNumber(numberSearchedBy);
//            Assert.AreEqual(expected.C_id, actual.C_id);
//        }

//        /// <summary>
//        ///A test for LookupByPhoneNumber
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonExsistentPhoneNumberTest()
//        {
//            string numberSearchedBy = "blah";
//            t_people expected = null;
//            t_people actual;
//            actual = PersonAccess_Accessor.LookupByPhoneNumber(numberSearchedBy);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByYahrtzieht
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByYahrtziehtTest()
//        {
//            string nameOfDeceased = "ploni ben almoni";
//            string relationToDeceased = "they where not";
//            List<t_people> expected = (from currYahrtzieht in Cache.CacheData.t_yahrtziehts
//                                        where currYahrtzieht.deceaseds_name == nameOfDeceased &&
//                                              currYahrtzieht.relation == relationToDeceased
//                                        select currYahrtzieht.t_people).ToList();
//            List<t_people> actual;
//            actual = PersonAccess_Accessor.LookupByYahrtzieht(nameOfDeceased, relationToDeceased);
//            CollectionAssert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for LookupByYahrtzieht
//        ///</summary>
//        [TestMethod()]
        
//        public void LookupByNonExsistentYahrtziehtTest()
//        {
//            string nameOfDeceased = string.Empty;
//            string relationToDeceased = string.Empty;
//            List<t_people> actual;
//            actual = PersonAccess_Accessor.LookupByYahrtzieht(nameOfDeceased, relationToDeceased);
//            Assert.AreEqual(0, actual.Count);
//        }

//        #endregion
        
//        #region Update Tests

//        /// <summary>
//        ///A test for UpdateMultiplePersons
//        ///</summary>
//        [TestMethod()]
//        public void UpdateMultiplePersonsTest()
//        {
//            List<Person> updatedPersonList = new List<Person>()
//            {
//                PersonAccess.GetById(7),
//                PersonAccess.GetById(8)
//            };
//            updatedPersonList[0].Email = new MailAddress("blah@balh.com");
//            updatedPersonList[1].FirstName = "jacob jinglehiemer";
//            PersonAccess.UpdateMultiplePersons(updatedPersonList);
//            List<Person> afterUpdate = PersonAccess.GetAllPeople(); ;
//            Assert.IsTrue(afterUpdate.Contains(updatedPersonList[0]));
//            Assert.IsTrue(afterUpdate.Contains(updatedPersonList[1]));
//        }

//        /// <summary>
//        ///A test for UpdateSinglePerson
//        ///</summary>
//        [TestMethod()]
//        public void UpdateSinglePersonTest()
//        {
//            Person updatedPerson = PersonAccess.GetById(9);
//            updatedPerson.Address = 
//                new StreetAddress("1234;main st;blah town;blah city;;blah country;87452");
//            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
//            Enums.CRUDResults actual;
//            actual = PersonAccess.UpdateSinglePerson(updatedPerson);
//            Assert.AreEqual(expected, actual);
//            Person afterUpdate = PersonAccess.GetById(9);
//            Assert.AreEqual(updatedPerson, afterUpdate);
//        }

//        /// <summary>
//        ///A test for UpdateSinglePerson
//        ///</summary>
//        [TestMethod()]
//        public void UpdateSingleNoNExsistentPersonTest()
//        {
//            Person updatedPerson = new Person(32543, "blah@blah.com", "", "", true, ";;;;;;",
//                new Account(1, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(), new List<Yahrtzieht>());
//            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_FAIL;
//            Enums.CRUDResults actual;
//            actual = PersonAccess.UpdateSinglePerson(updatedPerson);
//            Assert.AreEqual(expected, actual);
//        }

//        #endregion

//        #region Upsert Tests

//        /// <summary>
//        ///A test for UpsertSinglePerson
//        ///</summary>
//        [TestMethod()]
//        public void UpsertAddSinglePersonTest()
//        {
//            Person upsertedPerson = new Person(20, "blah@blah.com", "jane", "jack", true, "12;12;blank;blank;blank;blank;123456",
//                new Account(58, 0, DateTime.Today, new List<Donation>()),
//                new List<PhoneNumber>(), new List<Yahrtzieht>());
//            Enums.CRUDResults expected = Enums.CRUDResults.CREATE_SUCCESS;
//            Enums.CRUDResults actual;
//            actual = PersonAccess.UpsertSinglePerson(upsertedPerson);
//            Assert.AreEqual(expected, actual);
//            Assert.AreEqual(upsertedPerson, PersonAccess.GetById(20));
//        }

//        /// <summary>
//        ///A test for UpsertSinglePerson
//        ///</summary>
//        [TestMethod()]
//        public void UpsertUpdateSinglePersonTest()
//        {
//            Person upsertedPerson = PersonAccess.GetById(10);
//            upsertedPerson.PersonalAccount.PaidDonations.Add(new PaidDonation(456, "", 213, DateTime.Today,"", DateTime.Today));
//            Enums.CRUDResults expected = Enums.CRUDResults.UPDATE_SUCCESS;
//            Enums.CRUDResults actual;
//            actual = PersonAccess.UpsertSinglePerson(upsertedPerson);
//            Assert.AreEqual(expected, actual);
//            Assert.AreEqual(upsertedPerson, PersonAccess.GetById(10));
//        }

//        #endregion
//    }
//}
