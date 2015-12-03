using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class PhoneNumber
    {
        public int PersonID { get; set; }
        public string Number { get; set; }
        public int NumberTypeID { get; set; }
        public int ID { get; set; }
        public virtual Person Person { get; set; }
        public virtual PhoneType Type { get; set; }

        public static PhoneNumber Createt_phone_numbers(int personId, string number, int _Id1, int _Id2) => new PhoneNumber { PersonID = personId, Number = number, NumberTypeID = _Id1, ID = _Id2 };
    }
}
