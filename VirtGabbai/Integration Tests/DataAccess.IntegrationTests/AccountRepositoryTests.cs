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

        [TestMethod]
        public void Add_Item_Is_Null_Nothing_Happens()
        {
            var before = Helpers.SetupData(_ctx, 10);

            repository.Add(null);

            CollectionAssert.AreEquivalent(before, repository.Get().ToList());
        }

        [TestMethod]
        public void Add_Person_Is_Null_Nothing_Happens()
        {
            var before = Helpers.SetupData(_ctx, 10);

            var account = A.New<Account>();
            account.Person = null;
            account.PersonID = 0;

            repository.Add(account);

            CollectionAssert.AreEquivalent(before, repository.Get().ToList());
        }

        [TestMethod]
        public void Add_Item_Gets_Added()
        {
            var before = Helpers.SetupData(_ctx, 5);

            var newAccount = A.New<Account>();
            var newPerson = A.New<Person>();

            _ctx.People.Add(newPerson);
            _ctx.SaveChanges();

            newAccount.Person = newPerson;
            newAccount.PersonID = newPerson.ID;

            repository.Add(newAccount);

            before.Add(newAccount);
            CollectionAssert.AreEquivalent(before, repository.Get().ToList());

            var person = _ctx.People.FirstOrDefault(p => p.ID == newPerson.ID);
            Assert.AreEqual(newAccount, person.Account);
        }

        [TestMethod]
        public void Delete_Item_Does_Not_Exist_Nothing_Happens()
        {
            var before = Helpers.SetupData(_ctx, 5);
            var item = A.New<Account>();

            repository.Delete(item);

            CollectionAssert.AreEquivalent(before, repository.Get().ToList());
        }

        [TestMethod]
        public void Delete_Item_Exists_Is_Removed()
        {
            var account = Helpers.SetupData(_ctx);

            repository.Delete(account);

            var after = repository.Get();
            Assert.IsFalse(after.Contains(account));
            Assert.IsNull(repository.GetByID(account.ID));
        }

        [TestMethod]
        public void Save_Item_Is_Null_Nothing_Happens()
        {
            var before = Helpers.SetupData(_ctx, 10);

            repository.Save(null);

            CollectionAssert.AreEquivalent(before, repository.Get().ToList());
        }

        [TestMethod]
        public void Save_Item_Has_No_Person_Nothing_Happens()
        {
            var before = Helpers.SetupData(_ctx, 10);

            var account = A.New<Account>();
            account.Person = null;
            account.PersonID = 0;

            repository.Save(account);

            CollectionAssert.AreEquivalent(before, repository.Get().ToList());
        }

        [TestMethod]
        public void Save_Item_Does_Not_Exist_Is_Added()
        {
            Helpers.SetupData(_ctx, 5);
            var before = repository.Get().ToList();

            var newAccount = A.New<Account>();
            var newPerson = A.New<Person>();

            _ctx.People.Add(newPerson);
            _ctx.SaveChanges();

            newAccount.Person = newPerson;
            newAccount.PersonID = newPerson.ID;

            Assert.IsFalse(before.Contains(newAccount));
            Assert.IsFalse(repository.Exists(newAccount));

            repository.Save(newAccount);

            before.Add(newAccount);
            CollectionAssert.AreEquivalent(before, repository.Get().ToList());

            var person = _ctx.People.FirstOrDefault(p => p.ID == newPerson.ID);
            Assert.AreEqual(newAccount, person.Account);
        }

        [TestMethod]
        public void Save_Item_Already_Exists_Changes_Saved()
        {
            var data = Helpers.SetupData(_ctx, 10);

            var accountID = data.Skip(3).First().ID;

            var account = repository.GetByID(accountID);

            Assert.IsNotNull(account);

            account.MonthlyPaymentAmount *= 2;

            repository.Save(account);

            Assert.AreEqual(account, repository.GetByID(accountID));
        }

        [TestMethod]
        public void Save_Person_Change_Is_Not_Saved()
        {
            var data = Helpers.SetupData(_ctx, 10);

            var accountID = data.Skip(3).First().ID;

            var account = repository.GetByID(accountID);

            Assert.IsNotNull(account);

            account.Person = A.New<Person>();

            repository.Save(account);

            Assert.AreNotEqual(account, repository.GetByID(accountID));
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
