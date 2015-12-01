using System;

namespace LocalTypes
{
    public class Donation
    {
        #region Properties

        public int _Id { get; set; }

        public string Reason { get; set; }

        public double Amount { get; set; }

        public DateTime DonationDate { get; set; }

        public string Comments { get; set; }

        #endregion

        #region C'tor

        public Donation(int id, string reason, double amount, DateTime donationDate, string comments)
        {
            this._Id = id;
            this.Amount = amount;
            this.Reason = reason;
            this.DonationDate = donationDate;
            this.Comments = comments;
        }

        #endregion

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
            return ((this._Id == donationToCompare._Id) &&
                    (this.Amount == donationToCompare.Amount) &&
                    (this.Comments == donationToCompare.Comments) &&
                    (this.DonationDate == donationToCompare.DonationDate) &&
                    (this.Reason == donationToCompare.Reason));
        }

        public override int GetHashCode() => base.GetHashCode();

        #endregion
    }
}
