using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Framework;

namespace LocalTypes
{
    public class Person
    {
        #region Properties

        public int _Id { get; set; }

        public MailAddress Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public StreetAddress Address { get; set; }

        public Account PersonalAccount { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<Yahrtzieht> Yahrtziehts { get; set; }

        #endregion

        #region C'tor

        public Person(int id, string emailAddress, string firstName, string lastName,
                      string streetAddress, Account personalAccount, List<PhoneNumber> phoneNumbers,
                      List<Yahrtzieht> yahrtziehts)
        {
            this._Id = id;
            this.Email = new MailAddress(emailAddress);
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = new StreetAddress(streetAddress);
            this.PersonalAccount = personalAccount;
            this.PhoneNumbers = phoneNumbers;
            this.Yahrtziehts = yahrtziehts;
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            Person comparedPerson = (Person)obj;

            return ((this._Id == comparedPerson._Id) &&
                    (this.Email.Equals(comparedPerson.Email)) &&
                    (this.FirstName == comparedPerson.FirstName) &&
                    (this.LastName == comparedPerson.LastName) &&
                    (this.Address.Equals(comparedPerson.Address)) &&
                    (this.PersonalAccount.Equals(comparedPerson.PersonalAccount)) &&
                    (this.PhoneNumbers.Contains(comparedPerson.PhoneNumbers)) &&
                    (this.Yahrtziehts.Contains(comparedPerson.Yahrtziehts)));
        }

        public override string ToString()
        {
            string phoneNumbersString = "";
            if (this.PhoneNumbers.Count > 0)
            {
                phoneNumbersString += this.PhoneNumbers[0].ToString();
                for (int phoneNumberIndex = 1; phoneNumberIndex < this.PhoneNumbers.Count; phoneNumberIndex++)
                {
                    phoneNumbersString += "\n" + this.PhoneNumbers[phoneNumberIndex].ToString();
                }
            }

            string yahrtziehtsString = "";
            if (this.Yahrtziehts.Count > 0)
            {
                yahrtziehtsString += this.Yahrtziehts[0].ToString();
                for (int yahrtziehtIndex = 1; yahrtziehtIndex < this.Yahrtziehts.Count; yahrtziehtIndex++)
                {
                    yahrtziehtsString += "\n" + this.Yahrtziehts[yahrtziehtIndex].ToString();
                }
            }

            return this.FirstName + " " + this.LastName + "\n" + this.Email.Address + "\n" +
                   "Lives at:\n" + this.Address.ToString() + "\nAccount information:\n" +
                   this.PersonalAccount.ToString() + "\nPhone Numbers:\n\t" + phoneNumbersString +
                   "\nYahrtziehts:\n\t" + yahrtziehtsString;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
