using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTypes
{
    public class Person
    {
        #region Properties

        public int _Id { get; set; }

        public Email Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public Account PersonalAccount { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<Yahrtzieht> Yahrtziehts { get; set; }

        #endregion

        #region C'tor
        
        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
