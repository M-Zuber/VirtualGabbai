using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataTypes;

namespace TempUI
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneNumber test = new PhoneNumber(1, "0546137475", new PhoneType(1, "cell phone"));
            Console.WriteLine(test.GetHashCode());
        }
    }
}
