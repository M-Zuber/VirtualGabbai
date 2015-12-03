using System.Collections.Generic;
using System.Net.Mail;
using Framework;
using DataCache.Models;
using System.Linq;

namespace DataCache.Models
{
    public partial class Person
    {
        #region Properties

        public StreetAddress FullAddress => new StreetAddress(Address);

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            Person comparedPerson = (Person)obj;

            return ((ID == comparedPerson.ID) &&
                    (Email.Equals(comparedPerson.Email)) &&
                    (GivenName == comparedPerson.GivenName) &&
                    (FamilyName == comparedPerson.FamilyName) &&
                    (Member == comparedPerson.Member) &&
                    (Address.Equals(comparedPerson.Address)) &&
                    (Account.Equals(comparedPerson.Account)) &&
                    (PhoneNumbers.SameAs(comparedPerson.PhoneNumbers)) &&
                    (Yahrtziehts.SameAs(comparedPerson.Yahrtziehts)));
        }

        public override string ToString()
        {
            string phoneNumbersString = "";
            if (PhoneNumbers.Count > 0)
            {
                phoneNumbersString += PhoneNumbers.First().ToString();

                foreach (var number in PhoneNumbers.Skip(1))
                {
                    phoneNumbersString += "\n" + number.ToString();
                }
            }

            string yahrtziehtsString = "";
            if (Yahrtziehts.Count > 0)
            {
                yahrtziehtsString += Yahrtziehts.First().ToString();
                foreach(var y in Yahrtziehts)
                {
                    yahrtziehtsString += "\n" + y.ToString();
                }
            }

            string membership = "";
            if (Member)
            {
                membership = "\nHas membership";
            }

            return GivenName + " " + FamilyName + "\n" + Email + "\n" +
                   "Lives at:\n" + Address.ToString() + membership + "\nAccount information:\n" +
                   Account.ToString() + "\nPhone Numbers:\n\t" + phoneNumbersString +
                   "\nYahrtziehts:\n\t" + yahrtziehtsString;
        }

        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
