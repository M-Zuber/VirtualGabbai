using Framework;
using System;

namespace DataCache.Models
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
            var addressParts = address.Split(new[] { Globals.Delimiter }, StringSplitOptions.None);

            if (addressParts.Length == 7)
            {
                ApartmentNumber = addressParts[0];
                House = addressParts[1];
                Street = addressParts[2];
                City = addressParts[3];
                State = addressParts[4].ToUpper();
                Country = addressParts[5].ToUpper();
                Zipcode = addressParts[6];
            }
        }

        public StreetAddress(string apartmentNumber, string house, string street, string city, string state, string country, string zipCode)
        {
            ApartmentNumber = apartmentNumber;
            House = house;
            Street = street;
            City = city;
            State = state.ToUpper();
            Country = country.ToUpper();
            Zipcode = zipCode;
        }

        #endregion

        #region Object Methods

        public override bool Equals(object obj)
        {
            if (!(obj is StreetAddress other))
            {
                return false;
            }

            return ReferenceEquals(this, other)
                    || (ApartmentNumber == other.ApartmentNumber
                   && House == other.House
                   && Street == other.Street
                   && City == other.City
                   && State == other.State
                   && Country == other.Country
                   && Zipcode == other.Zipcode);
        }

        public override int GetHashCode() => Zipcode.GetHashCode();

        public override string ToString()
        {
            var addressToString = House + " " + Street;
            if (!string.IsNullOrWhiteSpace(ApartmentNumber))
            {
                addressToString += "\tApartment #" + ApartmentNumber;
            }
            addressToString += "\n" + City + " ";
            if (!string.IsNullOrWhiteSpace(State))
            {
                addressToString += State + " ";
            }
            addressToString += Country + "\n" + Zipcode;

            return addressToString;
        }

        #endregion

        #region Other Methods

        public string ToDbString() => ApartmentNumber + Globals.Delimiter +
                                      House + Globals.Delimiter +
                                      Street + Globals.Delimiter +
                                      City + Globals.Delimiter +
                                      State + Globals.Delimiter +
                                      Country + Globals.Delimiter +
                                      Zipcode;

        #endregion
    }
}
