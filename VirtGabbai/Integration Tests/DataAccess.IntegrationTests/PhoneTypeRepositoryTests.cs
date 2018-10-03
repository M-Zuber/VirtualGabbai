using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class PhoneTypeRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private PhoneTypeRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new PhoneTypeRepository(_ctx);
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
            var item = GenFu.GenFu.New<PhoneType>();
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

            Assert.IsTrue(_repository.Exists(item.ID));
        }

        [TestMethod]
        public void Get_Returns_All_items()
        {
            var items = Helper.SetupData(_ctx, 2);

            CollectionAssert.AreEquivalent(items, _repository.Get().ToList());
        }

        [TestMethod]
        public void GetByID_No_Data_Returns_Null()
        {
            Assert.IsNull(_repository.GetByID(1));
        }

        [TestMethod]
        public void GetByID_No_Match_Returns_Null()
        {
            var items = Helper.SetupData(_ctx, 1);

            Assert.IsNull(_repository.GetByID(items.Max(d => d.ID) + 1));
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

            var item = Helper.GenFuSetup(1, before.Select(pt => pt.Name))
                             .First();
            _repository.Add(item);

            var after = _repository.Get();

            Assert.IsFalse(before.Contains(item));
            Assert.IsTrue(after.Contains(item));
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

            var item = Helper.GenFuSetup(1, before.Select(pt => pt.Name))
                             .First();
            item.ID = before.Max(p => p.ID) + 1;

            _repository.Delete(item);

            var after = _repository.Get().ToList();

            CollectionAssert.AreEquivalent(before, after);
        }

        [TestMethod]
        public void Delete_ItemIsValid_IsRemovedFromDatabase()
        {
            var before = Helper.SetupData(_ctx, 5);

            var item = before.Skip(1).First();
            _repository.Delete(item);

            var after = _repository.Get();

            Assert.IsTrue(before.Contains(item));
            Assert.IsFalse(after.Contains(item));
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

            var item = Helper.GenFuSetup(1, before.Select(pt => pt.Name))
                             .First();

            Assert.IsFalse(before.Contains(item));

            _repository.Save(item);

            var expected = _repository.GetByID(item.ID);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected, item);

            var after = _repository.Get().ToList();

            Assert.IsTrue(after.Contains(item));
        }

        [TestMethod]
        public void Save_ExistingItemNameChanged_ValuesAreUpdated()
        {
            var item = Helper.SetupData(_ctx);

            item.Name += $" {item.Name}";

            _repository.Save(item);

            var after = _repository.GetByID(item.ID);

            Assert.AreEqual(item, after);
        }

        private static class Helper
        {
            public static List<PhoneType> GenFuSetup(int count, IEnumerable<string> currentTypes)
            {
                var generatedPhoneTypes = GenFu.GenFu.ListOf<PhoneType>(count);
                var phoneTypes = new List<PhoneType>();

                var currentTypeList = currentTypes.ToList();
                foreach (var gPt in generatedPhoneTypes)
                {
                    if (phoneTypes.Find(pt => pt.Name.Equals(gPt.Name, StringComparison.CurrentCultureIgnoreCase)) == null
                        && (currentTypeList.Count == 0 || !currentTypeList.Contains(gPt.Name, StringComparer.CurrentCultureIgnoreCase)))
                    {
                        phoneTypes.Add(gPt);
                    }
                }

                while (phoneTypes.Count < count)
                {
                    generatedPhoneTypes = GenFu.GenFu.ListOf<PhoneType>(count);

                    foreach (var gPt in generatedPhoneTypes)
                    {
                        if (phoneTypes.Find(pt => pt.Name.Equals(gPt.Name, StringComparison.CurrentCultureIgnoreCase)) == null
                            && (currentTypeList.Count == 0 || !currentTypeList.Contains(gPt.Name, StringComparer.CurrentCultureIgnoreCase)))
                        {
                            phoneTypes.Add(gPt);
                        }
                    }
                }

                return phoneTypes.Take(count).ToList();
            }

            public static PhoneType SetupData(ZeraLeviContext ctx)
            {
                var phoneType = GenFuSetup(1, Enumerable.Empty<string>()).First();
                ctx.PhoneTypes.Add(phoneType);
                ctx.SaveChanges();

                return phoneType;
            }

            public static List<PhoneType> SetupData(ZeraLeviContext ctx, int count)
            {
                var items = GenFuSetup(count, Enumerable.Empty<string>());
                ctx.PhoneTypes.AddRange(items);
                ctx.SaveChanges();

                return items;
            }
        }
    }
}
