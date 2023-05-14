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
    public class UserRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private UserRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new UserRepository(_ctx);
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
            var item = GenFu.GenFu.New<User>();
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
            var items = Helper.SetupData(_ctx, 2);

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
            var items = Helper.SetupData(_ctx, 2);

            Assert.IsNull(_repository.GetById(items.Max(d => d.Id) + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, _repository.GetById(expected.Id));
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

            var first = before[0];
            var item = new User
            {
                UserName = first.UserName + "~",
                Password = first.Password + "~",
                Email = first.Email + "~",
                PrivilegeGroup = new PrivilegesGroup
                {
                    GroupName = first.PrivilegeGroup.GroupName + "~",
                    Privileges = first.PrivilegeGroup.Privileges.Select(p => new Privilege(0, p.Name + "~")).ToList()
                }
            };

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

            var item = Helper.GenFuSetup(1, before.Select(u => u.PrivilegeGroup.GroupName), before.SelectMany(u => u.PrivilegeGroup.Privileges))
                             .First();
            item.Id = before.Max(p => p.Id) + 1;

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

            var item = Helper.GenFuSetup(1, before.Select(u => u.PrivilegeGroup.GroupName), before.SelectMany(u => u.PrivilegeGroup.Privileges))
                             .First();

            Assert.IsFalse(before.Contains(item));

            _repository.Save(item);

            var after = _repository.Get().ToList();

            Assert.IsTrue(after.Contains(item));
        }

        [TestMethod]
        public void Save_ExistingItemPrivilegesChanged_ValuesAreUpdated()
        {
            var item = Helper.SetupData(_ctx);

            item.PrivilegeGroup.Privileges.Remove(item.PrivilegeGroup.Privileges.First());
            _repository.Save(item);

            var after = _repository.GetById(item.Id);

            Assert.AreEqual(item, after);
        }

        private static class Helper
        {
            public static List<User> GenFuSetup(int count, IEnumerable<string> currentPgNames, IEnumerable<Privilege> currentPrivileges)
            {
                var privileges = GenFu.GenFu.ListOf<Privilege>()
                    .DistinctBy(p => p.Name, StringComparer.CurrentCultureIgnoreCase)
                    .ToList();
                //Try to get at least 10 items
                //if (privileges.Count < 10)
                //{
                //    for (var i = 0; i < 3; i++)
                //    {
                //        privileges = GenFu.GenFu.ListOf<Privilege>()
                //                      .DistinctBy(p => p.Name, StringComparer.CurrentCultureIgnoreCase)
                //                      .ToList();

                //        if (privileges.Count >= 10)
                //        {
                //            break;
                //        }
                //    }
                //}

                privileges.RemoveAll(currentPrivileges.Contains);

                var listOfPrivilegeLists = new List<List<Privilege>>();

                const int slice = 3;

                for (var i = 0; i < privileges.Count / slice; i++)
                {
                    listOfPrivilegeLists.Add(privileges.Skip(i * slice).Take(slice).DistinctBy(p => p.Name, StringComparer.CurrentCultureIgnoreCase).ToList());
                }

                GenFu.GenFu.Configure<PrivilegesGroup>()
                    .Fill(pg => pg.Id, 0)
                    .Fill(pg => pg.Privileges)
                    .Fill(pg => pg.Privileges, listOfPrivilegeLists[0])
                    //.WithRandom(listOfPrivilegeLists)
                    ;

                var users = GenFu.GenFu.ListOf<User>(count);
                var currentPgNamesList = currentPgNames.ToList();

                foreach (var user in users)
                {
                    var pg = GenFu.GenFu.New<PrivilegesGroup>();

                    while (currentPgNamesList.Contains(pg.GroupName, StringComparer.CurrentCultureIgnoreCase))
                    {
                        pg = GenFu.GenFu.New<PrivilegesGroup>();
                    }

                    user.PrivilegeGroup = pg;
                    currentPgNamesList.Add(pg.GroupName);
                }

                return users;
            }

            public static User SetupData(ZeraLeviContext ctx)
            {
                var user = GenFuSetup(1, Enumerable.Empty<string>(), Enumerable.Empty<Privilege>()).First();
                ctx.Users.Add(user);
                ctx.SaveChanges();

                return user;
            }

            public static List<User> SetupData(ZeraLeviContext ctx, int count)
            {
                var users = GenFuSetup(count, Enumerable.Empty<string>(), Enumerable.Empty<Privilege>());
                ctx.Users.AddRange(users);
                ctx.SaveChanges();

                return users;
            }
        }
    }
}
