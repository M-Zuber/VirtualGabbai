using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTypes;
using DataAccess;

namespace TempUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PhoneTypeAccess.AddNewPhoneType(new PhoneType(1, "poper"));
                var test = PhoneTypeAccess.GetPhoneTypeById(1);
                Console.WriteLine(test.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
