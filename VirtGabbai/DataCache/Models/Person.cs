using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace DataCache.Models
{
    public partial class Person
    {
        public Person()
        {
            PhoneNumbers = new List<PhoneNumber>();
            Yahrtziehts = new List<t_yahrtziehts>();
        }

        public int ID { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public bool Member { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<t_yahrtziehts> Yahrtziehts { get; set; }

        public static Person Createt_people(int _Id) => new Person { ID = _Id };
    }
}
