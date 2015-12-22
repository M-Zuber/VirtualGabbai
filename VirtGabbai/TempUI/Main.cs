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

                    var t = ctx.Accounts.Any();
                    ctx.Database.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}