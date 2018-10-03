using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class DonationRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private DonationRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new DonationRepository(_ctx);
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
            var item = GenFu.GenFu.New<Donation>();
            Assert.IsFalse(_repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var donation = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(donation));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(_repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var donation = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(donation.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var donations = Helper.SetupData(_ctx, 5);

            CollectionAssert.AreEquivalent(donations, _repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(_repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var donations = Helper.SetupData(_ctx, 1);

            Assert.IsNull(_repository.GetByID(donations.Max(d => d.ID) + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, _repository.GetByID(expected.ID));
        }

        private static class Helper
        {
            public static Donation SetupData(ZeraLeviContext ctx)
            {
                var person = GenFu.GenFu.New<Person>();
                person.Account = GenFu.GenFu.New<Account>();
                person.Account.Donations = GenFu.GenFu.ListOf<Donation>();
                ctx.People.Add(person);

                ctx.SaveChanges();

                return person.Account.Donations.First();
            }

            public static List<Donation> SetupData(ZeraLeviContext ctx, int count)
            {
                var people = GenFu.GenFu.ListOf<Person>(count);
                var allDonations = new List<Donation>();

                foreach (var person in people)
                {
                    person.Account = GenFu.GenFu.New<Account>();

                    var donations = GenFu.GenFu.ListOf<Donation>(count);
                    person.Account.Donations = donations;
                    allDonations.AddRange(donations);
                }

                ctx.People.AddRange(people);
                ctx.SaveChanges();

                return allDonations;
            }
        }
    }
}
