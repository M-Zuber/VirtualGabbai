using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using GenFu;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class PersonRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private PersonRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new PersonRepository(_ctx);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _ctx.Database.Delete();
        }

        [TestMethod]
        public void Exists_Item_Null_Item_Returns_False()
        {
            Assert.IsFalse(_repository.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var item = GenFu.GenFu.New<Person>();
            Assert.IsFalse(_repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var person = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(person));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(_repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var person = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(person.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var people = Helper.SetupData(_ctx, 2);

            CollectionAssert.AreEquivalent(people, _repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(_repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var people = Helper.SetupData(_ctx, 2);

            Assert.IsNull(_repository.GetByID(people.Max(d => d.ID) + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, _repository.GetByID(expected.ID));
        }

        [TestMethod]
        public void Add_ItemIsNull_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 5);

            _repository.Add(null);

            var after = _repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Add_ValidItem_Added()
        {
            Helper.SetupData(_ctx, 5);

            var before = _repository.Get().ToList();

            var person = Helper.GenFuSetup(1, before.SelectMany(p => p.PhoneNumbers).Select(pn => pn.Type).Select(pt => pt.Name))
                               .First();
            _repository.Add(person);

            var after = _repository.Get();

            Assert.IsFalse(before.Contains(person));
            Assert.IsTrue(after.Contains(person));
        }

        [TestMethod]
        public void Delete_ItemIsNull_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 5);

            _repository.Delete(null);

            var after = _repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Delete_ItemNotInDatabase_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 5);

            var person = Helper.GenFuSetup(1, Enumerable.Empty<string>()).First();
            person.ID = before.Max(p => p.ID) + 1;

            _repository.Delete(person);

            var after = _repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Delete_ItemIsValid_IsRemovedFromDatabase()
        {
            var before = Helper.SetupData(_ctx, 5);

            var person = before.Skip(1).First();
            _repository.Delete(person);

            var after = _repository.Get();

            Assert.IsTrue(before.Contains(person));
            Assert.IsFalse(after.Contains(person));
        }

        [TestMethod]
        public void Save_ItemIsNull_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 3);

            _repository.Save(null);

            var after = _repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Save_ItemIsNew_ItemIsAddedToDatabase()
        {
            var before = Helper.SetupData(_ctx, 3);

            var person = Helper.GenFuSetup(1, before.SelectMany(p => p.PhoneNumbers.Select(pn => pn.Type)).Select(pt => pt.Name))
                               .First();

            Assert.IsFalse(before.Contains(person));

            _repository.Save(person);

            var after = _repository.Get().ToList();

            Assert.IsTrue(after.Contains(person));
        }

        [TestMethod]
        public void Save_ExistingItemDonationsChanged_ValuesAreUpdated()
        {
            var person = Helper.SetupData(_ctx);

            var firstDonation = person.Account.Donations.First();
            firstDonation.Paid = true;
            firstDonation.DatePaid = DateTime.Today;

            var newDonation = GenFu.GenFu.New<Donation>();
            person.Account.Donations.Add(newDonation);

            _repository.Save(person);

            var after = _repository.GetByID(person.ID);

            Assert.AreEqual(person, after);
        }

        private static class Helper
        {
            public static List<Person> GenFuSetup(int count, IEnumerable<string> existingNames)
            {
                var generatedPhoneTypes = GenFu.GenFu.ListOf<PhoneType>();
                var phoneTypes = new List<PhoneType>();
                var existingNamesList = existingNames.ToList();
                foreach (var gPt in generatedPhoneTypes)
                {
                    if (phoneTypes.Find(pt => pt.Name.Equals(gPt.Name, StringComparison.CurrentCultureIgnoreCase)) == null
                        && !existingNamesList.Any(g => string.Equals(g, gPt.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        phoneTypes.Add(gPt);
                    }
                }

                GenFu.GenFu.Configure<PhoneNumber>()
                 .Fill(pn => pn.Type)
                 .WithRandom(phoneTypes)
                 .Fill(pn => pn.Number)
                 .AsPhoneNumber();
                GenFu.GenFu.Configure<Donation>()
                 .Fill(d => d.Paid)
                 .WithRandom(new [] { true, false, false })
                 .Fill(d => d.DonationDate)
                 .AsPastDate();
                GenFu.GenFu.Configure<Account>()
                 .Fill(p => p.Donations, GenFu.GenFu.ListOf<Donation>())
                 .Fill(a => a.ID, 0);
                GenFu.GenFu.Configure<Person>()
                 .Fill(p => p.ID, 0);
                var people = GenFu.GenFu.ListOf<Person>(count);

                foreach (var person in people)
                {
                    var donations = GenFu.GenFu.ListOf<Donation>();
                    var account = GenFu.GenFu.New<Account>();
                    account.Donations = donations;
                    person.Account = account;

                    person.Yahrtziehts = GenFu.GenFu.ListOf<Yahrtzieht>();
                    person.PhoneNumbers = GenFu.GenFu.ListOf<PhoneNumber>();
                }
                return people;
            }

            public static Person SetupData(ZeraLeviContext ctx)
            {
                var person = GenFuSetup(1, Enumerable.Empty<string>()).First();
                ctx.People.Add(person);
                ctx.SaveChanges();

                return person;
            }

            public static List<Person> SetupData(ZeraLeviContext ctx, int count)
            {
                var people = GenFuSetup(count, Enumerable.Empty<string>());
                ctx.People.AddRange(people);
                ctx.SaveChanges();

                return people;
            }
        }
    }
}
