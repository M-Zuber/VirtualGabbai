using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.IntegrationTests.Helpers;
using DataCache.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.IntegrationTests
{
    [TestClass]
    public class PrivilegeRepositoryTests
    {
        private readonly VgTestContext _ctx = new VgTestContext();
        private PrivilegeRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            _ctx.Database.Delete();
            _repository = new PrivilegeRepository(_ctx);
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
            var item = GenFu.GenFu.New<Privilege>();
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
            var items = Helper.SetupData(_ctx, 1);

            Assert.IsNull(_repository.GetById(items.Max(d => d.Id) + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, _repository.GetById(expected.Id));
        }

        private static class Helper
        {
            public static Privilege SetupData(ZeraLeviContext ctx) => SetupData(ctx, 1).First();

            public static List<Privilege> SetupData(ZeraLeviContext ctx, int count)
            {
                var privilegeGroup = GenFu.GenFu.New<PrivilegesGroup>();
                var privileges = new List<Privilege>();
                var generatedPrivileges = GenFu.GenFu.ListOf<Privilege>(count);

                foreach (var gP in generatedPrivileges)
                {
                    if (privileges.Find(p => p.Name.Equals(gP.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        privileges.Add(gP);
                    }
                }

                while (privileges.Count < count)
                {
                    generatedPrivileges = GenFu.GenFu.ListOf<Privilege>(count);

                    foreach (var gP in generatedPrivileges)
                    {
                        if (privileges.Find(p => p.Name.Equals(gP.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
                        {
                            privileges.Add(gP);
                        }
                    }
                }

                privilegeGroup.Privileges = privileges.Take(count).ToList();
                ctx.PrivilegesGroups.Add(privilegeGroup);

                ctx.SaveChanges();

                return privilegeGroup.Privileges.ToList();
            }
        }
    }
}
