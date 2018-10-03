using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class PrivilegesGroupRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private PrivilegeGroupRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new PrivilegeGroupRepository(_ctx);
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
            var item = GenFu.GenFu.New<PrivilegesGroup>();
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

            var item = Helper.GenFuSetup(1).First();

            var currentNames = before.Select(pg => pg.GroupName).ToList();

            while (currentNames.Contains(item.GroupName, StringComparer.CurrentCultureIgnoreCase))
            {
                item = Helper.GenFuSetup(1).First();
            }

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

            var item = Helper.GenFuSetup(1).First();
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

            var item = Helper.GenFuSetup(1).First();

            Assert.IsFalse(before.Contains(item));

            _repository.Save(item);

            var after = _repository.Get().ToList();

            Assert.IsTrue(after.Contains(item));
        }

        [TestMethod]
        public void Save_ExistingItemNameChanged_ValuesAreUpdated()
        {
            var item = Helper.SetupData(_ctx);

            item.GroupName += $" {item.GroupName}";

            _repository.Save(item);

            var after = _repository.GetByID(item.ID);

            Assert.AreEqual(item, after);
        }

        private static class Helper
        {
            public static List<PrivilegesGroup> GenFuSetup(int count)
            {
                var generatedPrivileges = GenFu.GenFu.ListOf<Privilege>().DistinctBy(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
                var privileges = new List<Privilege>(generatedPrivileges);

                GenFu.GenFu.Configure<PrivilegesGroup>()
                    .Fill(pg => pg.Privileges, privileges.Skip(GenFu.GenFu.Random.Next() % privileges.Count).Take(GenFu.GenFu.Random.Next() % privileges.Count));

                var privilegeGroups = GenFu.GenFu.ListOf<PrivilegesGroup>(count).DistinctBy(pg => pg.GroupName, StringComparer.CurrentCultureIgnoreCase).ToList();

                // Try 3 times to make sure that we reached the amount wanted
                if (privilegeGroups.Count < count)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        privilegeGroups = GenFu.GenFu.ListOf<PrivilegesGroup>(count)
                                           .Concat(privilegeGroups)
                                           .DistinctBy(pg => pg.GroupName, StringComparer.CurrentCultureIgnoreCase)
                                           .ToList();

                        if (privilegeGroups.Count >= count)
                        {
                            break;
                        }
                    }
                }

                return privilegeGroups.Take(count).ToList();
            }

            public static PrivilegesGroup SetupData(ZeraLeviContext ctx)
            {
                var item = GenFuSetup(1).First();
                ctx.PrivilegesGroups.Add(item);
                ctx.SaveChanges();

                return item;
            }

            public static List<PrivilegesGroup> SetupData(ZeraLeviContext ctx, int count)
            {
                var items = GenFuSetup(count);
                ctx.PrivilegesGroups.AddRange(items);
                ctx.SaveChanges();

                return items;
            }
        }
    }
}
