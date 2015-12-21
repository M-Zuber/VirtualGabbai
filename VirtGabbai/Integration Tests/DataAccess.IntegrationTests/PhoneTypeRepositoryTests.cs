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
    public class PhoneTypeRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        PhoneTypeRepository repository;

        [TestInitialize()]
        public void Setup()
        {
            repository = new PhoneTypeRepository(_ctx);
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
            var item = A.New<PhoneType>();
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
            var items = Helper.SetupData(_ctx, 2);

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
            var items = Helper.SetupData(_ctx, 1);

            Assert.IsNull(repository.GetByID(items.Max(d => d.ID) + 1));
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

            var item = Helper.GenFuSetup(1, before.Select(pt => pt.Name))
                             .First();
            repository.Add(item);

            var after = repository.Get();

            Assert.IsFalse(before.Contains(item));
            Assert.IsTrue(after.Contains(item));
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

            var iten = Helper.GenFuSetup(1, before.Select(pt => pt.Name))
                             .First();
            iten.ID = before.Max(p => p.ID) + 1;

            repository.Delete(iten);

            var after = repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Delete_ItemIsValid_IsRemovedFromDatabase()
        {
            var before = Helper.SetupData(_ctx, 5);

            var item = before.Skip(1).First();
            repository.Delete(item);

            var after = repository.Get();

            Assert.IsTrue(before.Contains(item));
            Assert.IsFalse(after.Contains(item));
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

            var item = Helper.GenFuSetup(1, before.Select(pt =>pt.Name))
                             .First();

            Assert.IsFalse(before.Contains(item));

            repository.Save(item);

            var expected = repository.GetByID(item.ID);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected, item);

            var after = repository.Get().ToList();

            Assert.IsTrue(after.Contains(item));
        }

        [TestMethod]
        public void Save_ExistingItemNameChanged_ValuesAreUpdated()
        {
            var item = Helper.SetupData(_ctx);

            item.Name += $" {item.Name}";

            repository.Save(item);

            var after = repository.GetByID(item.ID);

            Assert.AreEqual(item, after);
        }

        class Helper
        {
            public static List<PhoneType> GenFuSetup(int count, IEnumerable<string> currentTypes)
            {
                var generatedPhoneTypes = A.ListOf<PhoneType>(count);
                var phoneTypes = new List<PhoneType>();

                foreach (var gPT in generatedPhoneTypes)
                {
                    if ((phoneTypes.FirstOrDefault(pt => pt.Name.Equals(gPT.Name, StringComparison.CurrentCultureIgnoreCase)) == null) &&
                        (!currentTypes.Any() || !currentTypes.Contains(gPT.Name, StringComparer.CurrentCultureIgnoreCase)))
                    {
                        phoneTypes.Add(gPT);
                    }
                }

                while (phoneTypes.Count < count)
                {
                    generatedPhoneTypes = A.ListOf<PhoneType>(count);

                    foreach (var gPT in generatedPhoneTypes)
                    {
                        if ((phoneTypes.FirstOrDefault(pt => pt.Name.Equals(gPT.Name, StringComparison.CurrentCultureIgnoreCase)) == null) &&
                            (!currentTypes.Any() || !currentTypes.Contains(gPT.Name, StringComparer.CurrentCultureIgnoreCase)))
                        {
                            phoneTypes.Add(gPT);
                        }
                    }
                }

                return phoneTypes.Take(count).ToList();
            }

            public static PhoneType SetupData(VGTestContext ctx)
            {
                var phoneType = GenFuSetup(1, Enumerable.Empty<string>()).First();
                ctx.PhoneTypes.Add(phoneType);
                ctx.SaveChanges();

                return phoneType;
            }

            public static List<PhoneType> SetupData(VGTestContext ctx, int count)
            {
                var items = GenFuSetup(count, Enumerable.Empty<string>());
                ctx.PhoneTypes.AddRange(items);
                ctx.SaveChanges();

                return items;
            }
        }
    }
}
