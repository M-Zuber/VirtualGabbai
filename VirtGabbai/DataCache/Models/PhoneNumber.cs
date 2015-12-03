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

        public override bool Equals(object obj)
        {
            PhoneNumber numberComparing = (PhoneNumber)obj;

            return ((ID == numberComparing.ID) &&
                    (Number == numberComparing.Number) &&
                    (Type.Equals(numberComparing.Type)));
        }

        public override string ToString() => $"Number:\"{Number}\" {Type.ToString()}";

        public override int GetHashCode() => base.GetHashCode();
    }
}
