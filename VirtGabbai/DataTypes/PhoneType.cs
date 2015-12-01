namespace LocalTypes
{
    public class PhoneType
    {
        #region Properties

        public int _Id { get; set; }

        public string PhoneTypeName { get; set; }

        #endregion

        #region C'tor

        public PhoneType(int _id, string typeName)
        {
            this._Id = _id;
            this.PhoneTypeName = typeName;
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            PhoneType comparingPhoneType = (PhoneType)obj;
            return ((this._Id == comparingPhoneType._Id) &&
                    (this.PhoneTypeName == comparingPhoneType.PhoneTypeName));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => ("Type:\"" + this.PhoneTypeName + "\"");

        #endregion
    }
}
