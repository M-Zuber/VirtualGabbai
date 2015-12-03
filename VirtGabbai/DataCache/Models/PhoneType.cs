using System;
using System.Collections.Generic;

namespace DataCache.Models
{
    public partial class PhoneType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public static PhoneType Createt_phone_types(int _Id, string phoneTypeName) => new PhoneType { ID = _Id, Name = phoneTypeName };

        public override bool Equals(object obj)
        {
            PhoneType comparingPhoneType = (PhoneType)obj;
            return ((ID == comparingPhoneType.ID) &&
                    (Name == comparingPhoneType.Name));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"Type:\"{Name}\"";
    }
}
