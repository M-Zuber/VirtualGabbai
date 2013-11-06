using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace LocalTypes
{
    public class Person
    {
        #region Properties

        public int _Id { get; set; }

        public MailAddress Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

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
            this.Address = streetAddress;
            this.PersonalAccount = personalAccount;
            this.PhoneNumbers = phoneNumbers;
            this.Yahrtziehts = yahrtziehts;
        }

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
