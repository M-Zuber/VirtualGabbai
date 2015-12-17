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
    public class AccountRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        AccountRepository repository;

        [TestInitialize()]
        public void Setup()
        {
            repository = new AccountRepository(_ctx);   
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
            var item = A.New<Account>();
            Assert.IsFalse(repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var account = Helpers.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(account));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var account = Helpers.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(account.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var accounts = Helpers.SetupData(_ctx, 5);

            CollectionAssert.AreEquivalent(accounts, repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var account = Helpers.SetupData(_ctx);

            Assert.IsNull(repository.GetByID(account.ID + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helpers.SetupData(_ctx);

            Assert.AreEqual(expected, repository.GetByID(expected.ID));
        }

        class Helpers
        {
            public static Account SetupData(VGTestContext ctx)
            {
                var person = A.New<Person>();
                person.Account = A.New<Account>();
                person.Account.Donations = A.ListOf<Donation>();
                ctx.People.Add(person);

                ctx.SaveChanges();

                return person.Account;
            }

            public static List<Account> SetupData(VGTestContext ctx, int count)
            {
                var people = A.ListOf<Person>(count);
                List<Account> accounts = new List<Account>();

                foreach (var person in people)
                {
                    var account = A.New<Account>();
                    person.Account = account;
                    accounts.Add(account);
                }

                ctx.People.AddRange(people);
                ctx.SaveChanges();

                return accounts;
            }
        }
    }
}
