namespace DataCache.Models
{
    public partial class PhoneType
    {

        #region Object Methods

        public override bool Equals(object obj)
        {
            PhoneType comparingPhoneType = (PhoneType)obj;
            return ((ID == comparingPhoneType.ID) &&
                    (Name == comparingPhoneType.Name));
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => ("Type:\"" + Name + "\"");

        #endregion
    }
}
