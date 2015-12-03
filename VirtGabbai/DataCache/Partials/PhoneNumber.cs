namespace DataCache.Models
{
    public partial class PhoneNumber
    {
        #region Properties

        public PhoneType NumberType { get; set; }

        #endregion

        #region Object methods

        public override bool Equals(object obj)
        {
            PhoneNumber numberComparing = (PhoneNumber)obj;

            return ((ID == numberComparing.ID) &&
                    (Number == numberComparing.Number) &&
                    (NumberType.Equals(numberComparing.NumberType)));
        }

        public override string ToString() => "Number:\"" + Number + "\" " + NumberType.ToString();

        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
