using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class PhoneNumber
    {
        public PhoneNumber()
        {

        }
        public PhoneNumber(int id, string number, PhoneType type)
        {
            ID = id;
            Number = number;
            Type = type;
        }

        public int PersonID { get; set; }
        public string Number { get; set; }
        public int NumberTypeID { get; set; }
        public int ID { get; set; }
        public virtual Person Person { get; set; }
        public virtual PhoneType Type { get; set; }

        public static PhoneNumber Createt_phone_numbers(int personId, string number, int _Id1, int _Id2) => new PhoneNumber { PersonID = personId, Number = number, NumberTypeID = _Id1, ID = _Id2 };

        public override bool Equals(object obj)
        {
            PhoneNumber other = obj as PhoneNumber;

            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) ||
                   (ID == other.ID &&
                    Number == other.Number &&
                    Type.Equals(other.Type));
        }

        public override string ToString() => $"Number:\"{Number}\" {Type.ToString()}";

        public override int GetHashCode() => base.GetHashCode();
    }
}
