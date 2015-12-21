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
    public class YahrtziehtRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        YahrtziehtRepository repository;

        [TestInitialize()]
        public void Setup()
        {
            _ctx.Database.Delete();
            repository = new YahrtziehtRepository(_ctx);
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
            var item = A.New<Yahrtzieht>();
            Assert.IsFalse(repository.Exists(item));
        }

        [TestMethod]
        public void Exists_Item_Match_Found_Returns_True()
        {
            var item = Helper.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(item));
        }

        [TestMethod]
        public void Exists_ID_No_Match_Returns_False()
        {
            Assert.IsFalse(repository.Exists(1));
        }

        [TestMethod]
        public void Exists_ID_Match_Returns_True()
        {
            var item = Helper.SetupData(_ctx);

            Assert.IsTrue(repository.Exists(item.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var items = Helper.SetupData(_ctx, 5);

            CollectionAssert.AreEquivalent(items, repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var item = Helper.SetupData(_ctx);

            Assert.IsNull(repository.GetByID(item.ID + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, repository.GetByID(expected.ID));
        }

        class Helper
        {
            public static Yahrtzieht SetupData(VGTestContext ctx) => SetupData(ctx, 1).First();

            public static List<Yahrtzieht> SetupData(VGTestContext ctx, int count)
            {
                var people = A.ListOf<Person>(count);
                List<Yahrtzieht> items = new List<Yahrtzieht>();

                foreach (var person in people)
                {
                    var yahrtziehts = A.ListOf<Yahrtzieht>(count);
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
