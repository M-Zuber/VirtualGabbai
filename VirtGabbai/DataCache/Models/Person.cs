using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace DataCache.Models
{
    public partial class Person
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public bool Member { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public virtual ICollection<Yahrtzieht> Yahrtziehts { get; set; } = new List<Yahrtzieht>();
        public static Person Createt_people(int _Id) => new Person { ID = _Id };

        public StreetAddress FullAddress => new StreetAddress(Address);

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
                foreach (var y in Yahrtziehts)
                {
                    yahrtziehtsString += "\n" + y.ToString();
                }
            }

            string membership = "";
            if (Member)
            {
                membership = "\nHas membership";
            }

            return $"{GivenName} {FamilyName}\n{Email}\n" +
                   $"Lives at:\n{FullAddress.ToString()} {membership}\nAccount information:\n" +
                   $"{Account.ToString()} \nPhone Numbers:\n\t{phoneNumbersString}" +
                   $"\nYahrtziehts:\n\t{yahrtziehtsString}";
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
