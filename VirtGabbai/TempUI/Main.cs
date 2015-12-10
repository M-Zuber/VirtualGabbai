using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataCache.Models;

namespace TempUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var ctx = new ZeraLeviContext())
                {
                    var pt = new PhoneType { Name = "thing" };
                    var r = new PhoneTypeRepository(ctx);
                    r.Add(pt);

                    var find = r.GetByName("THING");
                    Console.WriteLine(find);
                    ctx.Database.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //DataCache.Cache.CacheData.Database.Delete();
        }
    }
}