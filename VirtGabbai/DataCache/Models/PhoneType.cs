using System.Collections.Generic;

namespace DataCache.Models
{
    public class PhoneType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public override bool Equals(object obj)
        {
            if (!(obj is PhoneType other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || (Id == other.Id && Name == other.Name);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"Type:\"{Name}\"";
    }
}
