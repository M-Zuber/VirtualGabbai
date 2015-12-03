using System;

namespace DataCache.Models
{
    public partial class Donation
    {
        #region Object Methods

        public override string ToString()
        {
            string returnString = "Donated for: \"" + this.Reason +
                                  "\" Amount donated: \"" + this.Amount +
                                  "\" Date donated: \"" + this.DonationDate.ToString("dd/MM/yyyy") +"\"";
            if (this.Comments != string.Empty)
            {
                returnString += " Comments: \"" + this.Comments + "\"";
                
            }
            return returnString;
        }

        public override bool Equals(object obj)
        {
            Donation donationToCompare = (Donation)obj;

            if ((donationToCompare.Comments == null) && (this.Comments != null))
            {
                return false;
            }
            return ((this.ID == donationToCompare.ID) &&
                    (this.Amount == donationToCompare.Amount) &&
                    (this.Comments == donationToCompare.Comments) &&
                    (this.DonationDate == donationToCompare.DonationDate) &&
                    (this.Reason == donationToCompare.Reason));
        }

        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
