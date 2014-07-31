namespace LocalTypes
{
    public class PhoneNumber
    {
        #region Properties

        public int _Id { get; set; }

        public string Number { get; set; }

        public PhoneType NumberType { get; set; }

        #endregion

        #region C'tor

        public PhoneNumber(int _id, string phoneNumber, PhoneType numberType)
        {
            this._Id = _id;
            this.Number = phoneNumber;
            this.NumberType = numberType;
        }

        #endregion

        #region Object methods

        public override bool Equals(object obj)
        {
            PhoneNumber numberComparing = (PhoneNumber)obj;

            return ((this._Id == numberComparing._Id) &&
                    (this.Number == numberComparing.Number) &&
                    (this.NumberType.Equals(numberComparing.NumberType)));
        }

        public override string ToString()
        {
            return "Number:\"" + this.Number + "\" " + this.NumberType.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
