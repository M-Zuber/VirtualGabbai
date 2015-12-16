using DataCache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IntegrationTests.Helpers
{
    public class VGTestContext : ZeraLeviContext
    {
        public VGTestContext():this("name=Database")
        {

        }

        public VGTestContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}
