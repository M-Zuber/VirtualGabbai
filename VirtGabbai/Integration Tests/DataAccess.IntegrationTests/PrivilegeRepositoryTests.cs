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
    public class PrivilegeRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        PrivilegeRepository repository;

        [TestInitialize()]
        public void Setup()
        {
            _ctx.Database.Delete();
            repository = new PrivilegeRepository(_ctx);
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
            var item = A.New<Privilege>();
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
            var items = Helper.SetupData(_ctx, 1);

            Assert.IsNull(repository.GetByID(items.Max(d => d.ID) + 1));
        }

        [TestMethod]
        public void GetByID_Match_Returns_Match()
        {
            var expected = Helper.SetupData(_ctx);

            Assert.AreEqual(expected, repository.GetByID(expected.ID));
        }

        class Helper
        {
            public static Privilege SetupData(VGTestContext ctx) => SetupData(ctx, 1).First();

            public static List<Privilege> SetupData(VGTestContext ctx, int count)
            {
                var privilegeGroup = A.New<PrivilegesGroup>();
                var privileges = new List<Privilege>();
                var generatedPrivileges = A.ListOf<Privilege>(count);

                foreach (var gP in generatedPrivileges)
                {
                    if (privileges.FirstOrDefault(p => p.Name.Equals(gP.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
                    {
                        privileges.Add(gP);
                    }
                }

                while (privileges.Count < count)
                {
                    generatedPrivileges = A.ListOf<Privilege>(count);

                    foreach (var gP in generatedPrivileges)
                    {
                        if (privileges.FirstOrDefault(p => p.Name.Equals(gP.Name, StringComparison.CurrentCultureIgnoreCase)) == null)
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
