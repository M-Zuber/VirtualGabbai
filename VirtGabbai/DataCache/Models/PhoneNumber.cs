namespace DataCache.Models
{
    public enum PhoneType
    {
        Unknown,
        Cellphone,
        Landline
    }

    // TODO add a purpose/notes field for things like diffrentiating if phone is work/home ect
    // we might want to use an enum for that as well in order to limit the options
    // *or* that might then a use case for a table where we can add support for more options without
    // haveing to redeploy code
    public class PhoneNumber
    {
        public int PersonId { get; set; }
        public string Number { get; set; }
        public int Id { get; set; }
        public virtual Person Person { get; set; }
        public PhoneType Type { get; set; } = PhoneType.Unknown;

        public override bool Equals(object obj)
        {
            if (!(obj is PhoneNumber other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                   || (Id == other.Id
                   && Number == other.Number
                   && Type.Equals(other.Type) != false);
        }

        public override string ToString() => $"Number:\"{Number}\" {Type}";

        public override int GetHashCode() => Id.GetHashCode() * Number.GetHashCode();
    }
}
