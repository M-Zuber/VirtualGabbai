using Framework;
using System.Collections.Generic;
using System.Linq;

namespace DataCache.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public bool Member { get; set; }
        public virtual Account Account { get; set; } = new Account();
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public virtual ICollection<Yahrtzieht> Yahrtziehts { get; set; } = new List<Yahrtzieht>();

        public StreetAddress FullAddress => new StreetAddress(Address);

        public override bool Equals(object obj)
        {
            if (!(obj is Person other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                    || (Id == other.Id
                   && Email == other.Email
                   && GivenName == other.GivenName
                   && FamilyName == other.FamilyName
                   && Member == other.Member
                   && Address == other.Address
                   && Account.Equals(other.Account)
                   && PhoneNumbers.SameAs(other.PhoneNumbers)
                   && Yahrtziehts.SameAs(other.Yahrtziehts));
        }

        public override string ToString()
        {
            var phoneNumbersString = "";
            if (PhoneNumbers.Count > 0)
            {
                phoneNumbersString += PhoneNumbers.First().ToString();

                foreach (var number in PhoneNumbers.Skip(1))
                {
                    phoneNumbersString = $"{phoneNumbersString}\n{number}";
                }
            }

            var yahrtziehtsString = "";
            if (Yahrtziehts.Count > 0)
            {
                yahrtziehtsString += Yahrtziehts.First().ToString();
                foreach (var y in Yahrtziehts.Skip(1))
                {
                    yahrtziehtsString = $"{yahrtziehtsString}\n{y}";
                }
            }

            var membership = "";
            if (Member)
            {
                membership = "\nHas membership";
            }

            return $"{GivenName} {FamilyName}\n{Email}\n" +
                   $"Lives at:\n{FullAddress}{membership}\nAccount information:\n" +
                   $"{Account}\nPhone Numbers:\n\t{phoneNumbersString}" +
                   $"\nYahrtziehts:\n\t{yahrtziehtsString}";
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
