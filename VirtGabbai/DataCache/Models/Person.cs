using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace DataCache.Models
{
    public partial class Person
    {
        public Person()
        {

        }

        public Person(int id, string email, string givenName, string familyName, bool member, string address, Account account, IEnumerable<PhoneNumber> phoneNumbers, IEnumerable<Yahrtzieht> yahrtziehts)
        {
            ID = id;
            Email = email;
            GivenName = givenName;
            FamilyName = familyName;
            Member = member;
            Address = address;
            Account = account;
            PhoneNumbers = phoneNumbers.ToList();
            Yahrtziehts = yahrtziehts.ToList();
        }

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
            Person other = obj as Person;

            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) ||
                    (ID == other.ID &&
                    Email == other.Email &&
                    GivenName == other.GivenName &&
                    FamilyName == other.FamilyName &&
                    Member == other.Member &&
                    Address == other.Address &&
                    Account.Equals(other.Account) &&
                    PhoneNumbers.SameAs(other.PhoneNumbers) &&
                    Yahrtziehts.SameAs(other.Yahrtziehts));
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
                foreach (var y in Yahrtziehts.Skip(1))
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
                   $"Lives at:\n{FullAddress.ToString()}{membership}\nAccount information:\n" +
                   $"{Account.ToString()}\nPhone Numbers:\n\t{phoneNumbersString}" +
                   $"\nYahrtziehts:\n\t{yahrtziehtsString}";
        }

        public override int GetHashCode() => ID.GetHashCode();
    }
}
