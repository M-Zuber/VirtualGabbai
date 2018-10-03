using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private AccountRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new AccountRepository(_ctx);
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
            var item = GenFu.GenFu.New<Account>();
            Assert.IsFalse(_repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var account = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(account));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(_repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var account = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(account.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var accounts = Helper.SetupData(_ctx, 5);

            CollectionAssert.AreEquivalent(accounts, _repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(_repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var account = Helper.SetupData(_ctx);

            Assert.IsNull(_repository.GetByID(account.ID + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, _repository.GetByID(expected.ID));
        }

        private static class Helper
        {
            public static Account SetupData(ZeraLeviContext ctx)
            {
                var person = GenFu.GenFu.New<Person>();
                person.Account = GenFu.GenFu.New<Account>();
                person.Account.Donations = GenFu.GenFu.ListOf<Donation>();
                ctx.People.Add(person);

                ctx.SaveChanges();

                return person.Account;
            }

            public static List<Account> SetupData(ZeraLeviContext ctx, int count)
            {
                var people = GenFu.GenFu.ListOf<Person>(count);
                var accounts = new List<Account>();

                foreach (var person in people)
                {
                    var account = GenFu.GenFu.New<Account>();
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
