using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using GenFu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IntegrationTests
{
    [TestClass()]
    public class PersonRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        PersonRepository repository;

        [TestInitialize()]
        public void Setup()
        {
            repository = new PersonRepository(_ctx);
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _ctx.Database.Delete();
        }

        [TestMethod]
        public void Exists_Item_Null_Item_Returns_False()
        {
            Assert.IsFalse(repository.Exists(null));
        }

        [TestMethod]
        public void Exists_Item_No_Match_Returns_False()
        {
            var item = A.New<Person>();
            Assert.IsFalse(repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var person = Helper.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(person));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var person = Helper.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(person.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var people = Helper.SetupData(_ctx, 2);

            CollectionAssert.AreEquivalent(people, repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var people = Helper.SetupData(_ctx, 2);

            Assert.IsNull(repository.GetByID(people.Max(d => d.ID) + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, repository.GetByID(expected.ID));
        }

        [TestMethod]
        public void Add_ItemIsNull_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 5);

            repository.Add(null);

            var after = repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Add_ValidItem_Added()
        {
            Helper.SetupData(_ctx, 5);

            var before = repository.Get().ToList();

            var person = Helper.GenFuSetup(1, before.SelectMany(p => p.PhoneNumbers).Select(pn => pn.Type).Select(pt => pt.Name))
                               .First();
            repository.Add(person);

            var after = repository.Get();

            Assert.IsFalse(before.Contains(person));
            Assert.IsTrue(after.Contains(person));
        }

        [TestMethod]
        public void Delete_ItemIsNull_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 5);

            repository.Delete(null);

            var after = repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Delete_ItemNotInDatabase_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 5);

            var person = Helper.GenFuSetup(1, Enumerable.Empty<string>()).First();
            person.ID = before.Max(p => p.ID) + 1;

            repository.Delete(person);

            var after = repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Delete_ItemIsValid_IsRemovedFromDatabase()
        {
            var before = Helper.SetupData(_ctx, 5);

            var person = before.Skip(1).First();
            repository.Delete(person);

            var after = repository.Get();

            Assert.IsTrue(before.Contains(person));
            Assert.IsFalse(after.Contains(person));
        }

        [TestMethod]
        public void Save_ItemIsNull_NothingHappens()
        {
            var before = Helper.SetupData(_ctx, 3);

            repository.Save(null);

            var after = repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Save_ItemIsNew_ItemIsAddedToDatabase()
        {
            var before = Helper.SetupData(_ctx, 3);

            var person = Helper.GenFuSetup(1, before.SelectMany(p => p.PhoneNumbers.Select(pn => pn.Type)).Select(pt => pt.Name))
                               .First();

            Assert.IsFalse(before.Contains(person));

            repository.Save(person);

            var after = repository.Get().ToList();

            Assert.IsTrue(after.Contains(person));
        }

        [TestMethod]
        public void Save_ExistingItemDonationsChanged_ValuesAreUpdated()
        {
            var person = Helper.SetupData(_ctx);

            var firstDonation = person.Account.Donations.First();
            firstDonation.Paid = true;
            firstDonation.DatePaid = DateTime.Today;

            var newDonation = A.New<Donation>();
            person.Account.Donations.Add(newDonation);

            repository.Save(person);

            var after = repository.GetByID(person.ID);

            Assert.AreEqual(person, after);
        }

        class Helper
        {
            public static List<Person> GenFuSetup(int count, IEnumerable<string> existingNames)
            {
                var generatedPhoneTypes = A.ListOf<PhoneType>();
                var phoneTypes = new List<PhoneType>();

                foreach (var gPT in generatedPhoneTypes)
                {
                    if (phoneTypes.FirstOrDefault(pt => pt.Name.Equals(gPT.Name, StringComparison.CurrentCultureIgnoreCase)) == null &&
                        (!existingNames.Any() || !existingNames.Contains(gPT.Name, StringComparer.CurrentCultureIgnoreCase)))
                    {
                        phoneTypes.Add(gPT);
                    }
                }

                A.Configure<PhoneNumber>()
                 .Fill(pn => pn.Type)
                 .WithRandom(phoneTypes)
                 .Fill(pn => pn.Number)
                 .AsPhoneNumber();
                A.Configure<Donation>()
                 .Fill(d => d.Paid)
                 .WithRandom(new bool[] { true, false, false })
                 .Fill(d => d.DonationDate)
                 .AsPastDate();
                A.Configure<Account>()
                 .Fill(p => p.Donations, A.ListOf<Donation>());
                var people = A.ListOf<Person>(count);

                foreach (var person in people)
                {
                    var donations = A.ListOf<Donation>();
                    var account = A.New<Account>();
                    account.Donations = donations;
                    person.Account = account;

                    person.Yahrtziehts = A.ListOf<Yahrtzieht>();
                    person.PhoneNumbers = A.ListOf<PhoneNumber>();
                }
                return people;
            }

            public static Person SetupData(VGTestContext ctx)
            {
                var person = GenFuSetup(1, Enumerable.Empty<string>()).First();
                ctx.People.Add(person);
                ctx.SaveChanges();

                return person;
            }

            public static List<Person> SetupData(VGTestContext ctx, int count)
            {
                var people = GenFuSetup(count, Enumerable.Empty<string>());
                ctx.People.AddRange(people);
                ctx.SaveChanges();

                return people;
            }
        }
    }
}
