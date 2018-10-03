namespace DataCache.Models
{
    public class PhoneNumber
    {
        public int PersonId { get; set; }
        public string Number { get; set; }
        public int NumberTypeId { get; set; }
        public int Id { get; set; }
        public virtual Person Person { get; set; }
        public virtual PhoneType Type { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is PhoneNumber other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                   || (Id == other.Id
                   && Number == other.Number
                   && Type.Equals(other.Type));
        }

        public override string ToString() => $"Number:\"{Number}\" {Type}";

        public override int GetHashCode() => Id.GetHashCode() * NumberTypeId.GetHashCode();
    }
}
