using DataAccess.IntegrationTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IntegrationTests
{
    [TestClass()]
    public class AccountRepositoryTests
    {
        VGTestContext _ctx = new VGTestContext();
        AccountRepository repository;

        [ClassInitialize()]
        public void ClassSetup()
        {
            repository = new AccountRepository(_ctx);   
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _ctx.Database.Delete();
        }

        [TestMethod]
        public void Exists_Null_Item_Returns_False()
        {

        }
    }
}
