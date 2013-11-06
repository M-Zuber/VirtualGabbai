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

        public string State { get; set; }

        public string Country { get; set; }

        public string Zipcode { get; set; }

        #endregion

        #region C'tor

        public StreetAddress(string address)
        {
            char[] delimiters = new char[] { Globals.DELIMITER };
            string[] addressParts = address.Split(delimiters, StringSplitOptions.None);
            this.ApartmentNumber = addressParts[0];
            this.House = addressParts[1];
            this.Street = addressParts[2];
            this.City = addressParts[3];
            this.State = addressParts[4].ToUpper();
            this.Country = addressParts[5].ToUpper();
            this.Zipcode = addressParts[6];
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            StreetAddress comparingAddress = (StreetAddress)obj;
            return ((this.ApartmentNumber == comparingAddress.ApartmentNumber) &&
                    (this.House == comparingAddress.House) &&
                    (this.Street == comparingAddress.Street) &&
                    (this.City == comparingAddress.City) &&
                    (this.State == comparingAddress.State) &&
                    (this.Country == comparingAddress.Country) &&
                    (this.Zipcode == comparingAddress.Zipcode));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string addressToString = this.House + " " + this.Street;
            if (this.ApartmentNumber != "")
            {
                addressToString += "\tApartment #" + this.ApartmentNumber;
            }
            addressToString += "\n" + this.City + " ";
            if (this.State != "")
            {
                addressToString += this.State + " ";
            }
            addressToString += this.Country + "\n" + this.Zipcode;

            return addressToString;
        }

        #endregion

        #region Other Methods

        public string ToDbString() 
        {
            return (this.ApartmentNumber + Globals.DELIMITER +
                    this.House + Globals.DELIMITER +
                    this.Street + Globals.DELIMITER +
                    this.City + Globals.DELIMITER +
                    this.State + Globals.DELIMITER +
                    this.Country + Globals.DELIMITER +
                    this.Zipcode);
        }
        
        #endregion
    }
}
