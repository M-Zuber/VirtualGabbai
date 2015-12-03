using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocalTypes;
using DataAccess;

namespace TempUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            DataCache.Cache.CacheData.Database.Delete();
        }
    }
}