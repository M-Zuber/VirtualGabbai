using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework;

namespace DataTypes
{
    public class StreetAddress
    {
        #region Properties

        public string ApartmentNumber { get; set; }

        public string House { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Zipcode { get; set; }

        #endregion

        #region C'tor

        public StreetAddress(string address)
        {
            string[] addressParts = address.Split(Globals.DELIMITER);
            this.ApartmentNumber = addressParts[0];
            this.House = addressParts[1];
            this.Street = addressParts[2];
            this.City = addressParts[3];
            this.Country = addressParts[4];
            this.Zipcode = addressParts[5];
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}
