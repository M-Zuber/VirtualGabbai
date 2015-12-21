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
    public class PrivilegesGroupRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        PrivilegeGroupRepository repository;

        [TestInitialize()]
        public void Setup()
        {
            repository = new PrivilegeGroupRepository(_ctx);
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
            var item = A.New<PrivilegesGroup>();
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

            var item = Helper.GenFuSetup(1).First();
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

            var iten = Helper.GenFuSetup(1).First();
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

            var item = Helper.GenFuSetup(1).First();

            Assert.IsFalse(before.Contains(item));

            repository.Save(item);

            var after = repository.Get().ToList();

            Assert.IsTrue(after.Contains(item));
        }

        [TestMethod]
        public void Save_ExistingItemNameChanged_ValuesAreUpdated()
        {
            var item = Helper.SetupData(_ctx);

            item.GroupName += $" {item.GroupName}";

            repository.Save(item);

            var after = repository.GetByID(item.ID);

            Assert.AreEqual(item, after);
        }

        class Helper
        {
            public static List<PrivilegesGroup> GenFuSetup(int count)
            {
                var generatedPrivileges = A.ListOf<Privilege>();
                var privileges = new List<Privilege>();
                foreach (var gP in generatedPrivileges)
                {
                    if (privileges.FirstOrDefault(pt => pt.Name == gP.Name) == null)
                    {
                        privileges.Add(gP);
                    }
                }

                A.Configure<PrivilegesGroup>()
                    .Fill(pg => pg.Privileges, privileges.Skip(A.Random.Next() % privileges.Count).Take(A.Random.Next() % privileges.Count));

                var generatedPrivilegeGroups = A.ListOf<PrivilegesGroup>(count);
                var privilegeGroups = new List<PrivilegesGroup>();

                foreach (var gPG in generatedPrivilegeGroups)
                {
                    if (privilegeGroups.FirstOrDefault(pg => pg.GroupName == gPG.GroupName) == null)
                    {
                        privilegeGroups.Add(gPG);
                    }
                }

                while (privilegeGroups.Count < count)
                {
                    generatedPrivilegeGroups = A.ListOf<PrivilegesGroup>(count);

                    foreach (var gPG in generatedPrivilegeGroups)
                    {
                        if (privilegeGroups.FirstOrDefault(pg => pg.GroupName == gPG.GroupName) == null)
                        {
                            privilegeGroups.Add(gPG);
                        }
                    }
                }

                return privilegeGroups.Take(count).ToList();
            }

            public static PrivilegesGroup SetupData(VGTestContext ctx)
            {
                var item = GenFuSetup(1).First();
                ctx.PrivilegesGroups.Add(item);
                ctx.SaveChanges();

                return item;
            }

            public static List<PrivilegesGroup> SetupData(VGTestContext ctx, int count)
            {
                var items = GenFuSetup(count);
                ctx.PrivilegesGroups.AddRange(items);
                ctx.SaveChanges();

                return items;
            }
        }
    }
}
