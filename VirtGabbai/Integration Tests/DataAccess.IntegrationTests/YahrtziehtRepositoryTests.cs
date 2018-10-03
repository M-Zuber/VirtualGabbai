using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class YahrtziehtRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private YahrtziehtRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new YahrtziehtRepository(_ctx);
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
            var item = GenFu.GenFu.New<Yahrtzieht>();
            Assert.IsFalse(_repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var item = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(item));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(_repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var item = Helper.SetupData(_ctx);

            Assert.IsTrue(_repository.Exists(item.Id));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var items = Helper.SetupData(_ctx, 5);

            CollectionAssert.AreEquivalent(items, _repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(_repository.GetById(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var item = Helper.SetupData(_ctx);

            Assert.IsNull(_repository.GetById(item.Id + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, _repository.GetById(expected.Id));
        }

        private static class Helper
        {
            public static Yahrtzieht SetupData(ZeraLeviContext ctx) => SetupData(ctx, 1).First();

            public static List<Yahrtzieht> SetupData(ZeraLeviContext ctx, int count)
            {
                var people = GenFu.GenFu.ListOf<Person>(count);
                var items = new List<Yahrtzieht>();

                foreach (var person in people)
                {
                    var yahrtziehts = GenFu.GenFu.ListOf<Yahrtzieht>(count);
                    person.Yahrtziehts = yahrtziehts;
                    items.AddRange(yahrtziehts);
                }

                ctx.People.AddRange(people);
                ctx.SaveChanges();

                return items;
            }
        }
    }
}
