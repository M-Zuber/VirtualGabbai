using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTypes
{
    public class PhoneType
    {
        #region Properties

        public int _Id { get; set; }

        public string PhoneTypeName { get; set; }

        #endregion

        #region C'tor

        public PhoneType(){}

        public PhoneType(int _id, string typeName)
        {
            this._Id = _id;
            this.PhoneTypeName = typeName;
        }

        #endregion

        #region Object Methods

        public override bool Equals(object phoneTypeToCompare)
        {
            PhoneType comparingPhoneType = (PhoneType)phoneTypeToCompare;
            return ((this._Id == comparingPhoneType._Id) &&
                    (this.PhoneTypeName == comparingPhoneType.PhoneTypeName));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return ("Phone Type Name:\"" + this.PhoneTypeName + "\"");
        }

        #endregion
    }
}
