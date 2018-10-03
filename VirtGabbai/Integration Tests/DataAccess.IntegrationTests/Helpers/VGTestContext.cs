using DataCache.Models;

namespace DataAccess.IntegrationTests.Helpers
{
    public class VgTestContext : ZeraLeviContext
    {
        public VgTestContext():this("name=Database")
        {

        }

        public VgTestContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}
