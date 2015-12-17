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
            var person = Helpers.SetupData(_ctx);

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
            var person = Helpers.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(person.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var people = Helpers.SetupData(_ctx, 2);

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
            var people = Helpers.SetupData(_ctx, 1);

            Assert.IsNull(repository.GetByID(people.Max(d => d.ID) + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helpers.SetupData(_ctx);

            Assert.AreEqual(expected, repository.GetByID(expected.ID));
        }

        class Helpers
        {
            public static void GenFuSetup()
            {
                A.Configure<PhoneNumber>()
                 .Fill(pn => pn.Type, A.New<PhoneType>());
                A.Configure<Donation>()
                 .Fill(d => d.Paid)
                 .WithRandom(new bool[] { true, false, false })
                 .Fill(d => d.DonationDate)
                 .AsPastDate();
                A.Configure<Account>()
                 .Fill(p => p.Donations, A.ListOf<Donation>());
            }

            public static Person SetupData(VGTestContext ctx)
            {
                GenFuSetup();
                var person = A.New<Person>();
                ctx.People.Add(person);

                ctx.SaveChanges();

                return person;
            }

            public static List<Person> SetupData(VGTestContext ctx, int count)
            {
                GenFuSetup();
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

                ctx.People.AddRange(people);
                ctx.SaveChanges();

                return people;
            }
        }
    }
}
